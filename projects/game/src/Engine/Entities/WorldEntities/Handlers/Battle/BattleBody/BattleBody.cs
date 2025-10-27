using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Utils;
using static Entities.BattleMovesetFactory;
using Color = Microsoft.Xna.Framework.Color;
using Model = Resources.Model;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Entities
{

    public class BattleBodyData
    {
        public float WeaponSwingSpeedMultiplier;
        public StaticSprites Sprite;
        public BattleMovesets MoveSet;
        public AnimationFramesData WeaponOutAnimationData;
        public LightSource.LightSourceData LightSourceData;
        public ModelStates ModelStateBetweenHits;
        public Projectiles ProjectileToCast;

        public bool DisableHitBoxDamage;

        public BattleBodyData()
        {
        }

        public BattleBodyData(float weaponSwingSpeedMultiplier, StaticSprites sprite, BattleMovesets moveSet, AnimationFramesData weaponOutAnimationData, LightSource.LightSourceData lightSourceData, ModelStates stateBetweenHits = ModelStates.WEAPON_OUT_IDLE, Projectiles projectileToCast = Projectiles.NONE)
        {
            WeaponSwingSpeedMultiplier = weaponSwingSpeedMultiplier;
            Sprite = sprite;
            MoveSet = moveSet;
            LightSourceData = lightSourceData;
            ModelStateBetweenHits = stateBetweenHits;
            ProjectileToCast = projectileToCast;
            DisableHitBoxDamage = false;
        }
    }

    public class BattleBody
    {
        public WeaponHitbox Hitbox;
        public AnimationSet AManager;
        public List<AttackTypes> AttackHistory;
        private bool ComboHistoryUpdated = false;
        private bool ModelAnimationTimeUpdated = false;

        public readonly float GlobalWeaponSwingSpeedMultiplier = 0.6f;

        public float CurrentSwingTime = 0f;
        public bool IsSwinging = false;
        public BattleCombo Combo; 
        public WeaponLightSource LightSource;

        public RotatedRectangle NoAttackHitbox;

        public BattleBodyData BattleBodyData;

        //Lerp state
        private Vector2 InitialHitboxSize;
        private Vector2 TargetHitboxSize;
        private FlatVector InitialBodyPosition;
        private FlatVector TargetBodyPosition;

        public BattleComboHit[] MoveSetComboHits;

        public ProjectileEntity Projectile;

        public BattleBody()
        {
            Hitbox = new WeaponHitbox();
            AttackHistory = new List<AttackTypes>();
            Combo = new BattleCombo();
        }

        public void Init(BattleBodyData data)
        {
            BattleBodyData = data;
            MoveSetComboHits = GetWeaponComboHits(BattleBodyData.MoveSet);

            for (int i = 0; i < MoveSetComboHits.Length; i++)
            {
                MoveSetComboHits[i].SetAnimation(BattleBodyData.MoveSet, GlobalWeaponSwingSpeedMultiplier * BattleBodyData.WeaponSwingSpeedMultiplier);

                AManager = new AnimationSet(StaticSpriteFactory.spriteMappings[BattleBodyData.Sprite].SpriteSheet,
                    new List<Animation>()
                    {
                        new Animation(new AnimationKey(MoveSetComboHits[i].AnimationState, Directions.LEFT), MoveSetComboHits[i].AnimationData),
                        new Animation(new AnimationKey(MoveSetComboHits[i].AnimationState, Directions.RIGHT), MoveSetComboHits[i].AnimationData),
                    }
                );
            }

            if(BattleBodyData.ModelStateBetweenHits == ModelStates.WEAPON_OUT_IDLE)
            {
                AManager = new AnimationSet(StaticSpriteFactory.spriteMappings[BattleBodyData.Sprite].SpriteSheet,
                    new List<Animation>()
                    {
                        new Animation(new AnimationKey(AnimationStates.WEAPON_OUT_IDLE, Directions.LEFT), BattleBodyData.WeaponOutAnimationData),
                        new Animation(new AnimationKey(AnimationStates.WEAPON_OUT_IDLE, Directions.RIGHT), BattleBodyData.WeaponOutAnimationData),
                    }
                );
            }
            

            Combo.UpdateHits(AttackHistory, BattleBodyData.MoveSet);


            if (BattleBodyData.LightSourceData != null)
            {
                LightSource = new WeaponLightSource(BattleBodyData.LightSourceData);
            }

            NoAttackHitbox = new Utils.RotatedRectangle(new Vector2(15, 20), new Vector2(10, 30), 0f);
        }

        public void Update(Model model, EquipmentManager equipmentManager = null)
        {
            float deltaTime = (float)Graphics.Graphics.CurrentLogicTime/(float)Graphics.Graphics.TimeScale;

            if (LightSource != null)
            {
                if (model.ModelState == ModelStates.ATTACKING_LIGHT || model.ModelState == ModelStates.ATTACKING_HEAVY || model.ModelState == ModelStates.BLOCKING
                    || model.ModelState == ModelStates.WEAPON_OUT_IDLE || model.ModelState == ModelStates.WEAPON_OUT_MOVING 
                    || model.ModelState == ModelStates.FLYING || model.ModelState == ModelStates.FLYING_AND_MOVING)
                {
                    if (Graphics.Graphics.lightManager.GetEntityById(LightSource.Id) == null)
                    {
                        LightSource.Init(Combo, NoAttackHitbox, model, BattleBodyData.LightSourceData);
                        Graphics.Graphics.lightManager.AddLightSource(LightSource);
                    }
                }
                else
                {
                    Graphics.Graphics.lightManager.lightSourcesToRemove.Add(LightSource);
                }
            }

            if (model.ModelState == ModelStates.ATTACKING_LIGHT || model.ModelState == ModelStates.ATTACKING_HEAVY || model.ModelState == ModelStates.BLOCKING)
            {
                AttackTypes currentAttack = SwitchModelStateToAttackType(model.ModelState);

                UpdateComboSelection(currentAttack);
                UpdateHitbox(model);
                UpdateSwingAndCombo(model, currentAttack, equipmentManager, deltaTime);
                UpdateAnimation(model);
            }
            else
            {
                Combo.UpdateCounter(deltaTime, AttackHistory);
                Hitbox.Update(Vector2.Zero, Vector2.Zero);
                IsSwinging = false;
                ComboHistoryUpdated = false;
                ModelAnimationTimeUpdated = false;
                InitialBodyPosition = model.Body.Position;
                TargetBodyPosition = model.Body.Position;
            }
        }

        private void UpdateComboSelection(AttackTypes currentAttack)
        {
            if (ComboHistoryUpdated)
                return;

            ComboHistoryUpdated = true;
            AttackHistory.Add(currentAttack);
            int maxComboLength = MoveSetComboHits.Max(h => h.AttackSequence.Length);
            if (AttackHistory.Count > maxComboLength)
                AttackHistory.RemoveAt(0);

            Combo.UpdateHits(AttackHistory, BattleBodyData.MoveSet);
        }

        private void UpdateHitbox(Model model)
        {
            var currentHit = Combo.GetCurrentHit();
            int horizontalXFactor = model.Direction == Directions.RIGHT ? 1 : -1;

            if (currentHit == null)
            {
                model.ModelState = BattleBodyData.ModelStateBetweenHits;
                return;
            }


            Vector2 weaponPosition = model.Body.Position.ToVector2() + currentHit.HitboxOffset.Position * new Vector2(horizontalXFactor, 1f);
            float swingDuration = CalculateFinalSwingTime();
            float appearanceStart = BattleBodyData.WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier * currentHit.HitboxAppearanceTimePeriod.X;
            float appearanceEnd = BattleBodyData.WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier * currentHit.HitboxAppearanceTimePeriod.Y;

            if (CurrentSwingTime > BattleBodyData.WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier * currentHit.HitboxAppearanceTimePeriod.X && CurrentSwingTime < BattleBodyData.WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier * currentHit.HitboxAppearanceTimePeriod.Y)
            {
                if (CurrentSwingTime == 0f || InitialHitboxSize == Vector2.Zero)
                {
                    InitialHitboxSize = Hitbox.extends.Size();
                    TargetHitboxSize = currentHit.HitboxOffset.Size();
                }

                float t = Microsoft.Xna.Framework.MathHelper.Clamp((CurrentSwingTime - appearanceStart) / (appearanceEnd - appearanceStart), 0f, 1f);
                Vector2 lerpedSize = Vector2.Lerp(InitialHitboxSize, TargetHitboxSize, t);

                Hitbox.Update(
                    weaponPosition,
                    lerpedSize,
                    currentHit.HitboxOffset.Rotation * horizontalXFactor
                );

                TargetBodyPosition = InitialBodyPosition + new FlatVector(
                    currentHit.EntityPositionOffset.X * horizontalXFactor,
                    currentHit.EntityPositionOffset.Y
                );
                //t = Microsoft.Xna.Framework.MathHelper.Clamp(CurrentSwingTime / swingDuration, 0f, 1f);
                FlatVector lerpedPosition = FlatVector.Lerp(InitialBodyPosition, TargetBodyPosition, t);
                model.Body.MoveTo(lerpedPosition);
            }
            else
            {
                Hitbox.Update(Vector2.Zero, Vector2.Zero);
                InitialHitboxSize = Vector2.Zero;
                TargetHitboxSize = Vector2.Zero;
                InitialBodyPosition = model.Body.Position;
                TargetBodyPosition = model.Body.Position;
            }
            
        }

        private void UpdateSwingAndCombo(Model model, AttackTypes currentAttack, EquipmentManager equipmentManager, float deltaTime)
        {
            if (!IsSwinging)
            {
                IsSwinging = true;
                CurrentSwingTime = 0f;

                if(currentAttack != AttackTypes.BLOCK)
                {
                    if (Combo.CanContinueWith(currentAttack, AttackHistory, BattleBodyData.MoveSet))
                    {
                        Combo.UpdateSet(AttackHistory, BattleBodyData.MoveSet);
                    }
                    else
                    {
                        Combo.ResetCombo(AttackHistory);
                        Combo.UpdateHits(AttackHistory, BattleBodyData.MoveSet);
                    }
                    Combo.StartContinuationWindow();
                }
                

                var currentHit = Combo.GetCurrentHit();
                if (currentHit == null)
                {
                    return;
                }

                int hitIndex = Array.IndexOf(MoveSetComboHits, currentHit);

                AManager.GetCurrent().Reset();
                AManager.GetCurrent().Start();

                Sounds.Sounds.SoundManager.AddSoundSource(new Sounds.SoundSource(
                    Resources.Sounds.SWING_SWORD,
                    model.Body.Position.ToVector2(),
                    currentHit.SwingTimeSec * BattleBodyData.WeaponSwingSpeedMultiplier
                ));
            }

            CurrentSwingTime += deltaTime;

            var hit = Combo.GetCurrentHit();
            if (hit != null && CurrentSwingTime >= CalculateFinalSwingTime())
            {
                IsSwinging = false;
                model.ModelState = BattleBodyData.ModelStateBetweenHits;
                
                var hitTemplates = MoveSetComboHits;
                var nextHits = hitTemplates.Where(h => h.AttackSequence.Length == hit.AttackSequence.Length + 1 &&
                                                      h.AttackSequence.Take(hit.AttackSequence.Length).SequenceEqual(hit.AttackSequence)).ToList();
                if (!nextHits.Any())
                {
                    AttackHistory.Clear();
                    Combo.UpdateHits(AttackHistory, BattleBodyData.MoveSet);
                }

                if (BattleBodyData.ProjectileToCast != Projectiles.NONE)
                {
                    Vector2 projectileDirection = hit.HitboxOffset.Position;
                    if (projectileDirection != Vector2.Zero)
                    {
                        float rotation = hit.HitboxOffset.Rotation;
                        projectileDirection = Vector2.Transform(projectileDirection, Matrix.CreateRotationZ(rotation));
                        projectileDirection.Normalize();
                    }

                    projectileDirection = new Vector2(projectileDirection.X * (model.Direction == Directions.RIGHT ? -1 : 1), -projectileDirection.Y);

                    Projectile = new ProjectileEntity(model.Body.Position.ToVector2(), BattleBodyData.ProjectileToCast, projectileDirection);

                    if (equipmentManager != null)
                    {
                        Projectile.UpdateProjectileStats(equipmentManager.GetCurrentWeapon().BattleItemStatsData, model.OwnerId);
                    }

                    Entities.EntityManager.AddEntity(Projectile);
                }
            }
        }

        private void UpdateAnimation(Model model)
        {
            var currentHit = Combo.GetCurrentHit();
            if (currentHit == null)
            {
                return;
            }
            AManager.Update(new AnimationKey(Combo.GetCurrentHit().AnimationState, model.Direction));

            model.AnimationState = Combo.GetCurrentHit().AnimationState;

            if (!ModelAnimationTimeUpdated)
            {
                model.AManagers[0].GetAnimation(new AnimationKey(model.AnimationState, model.Direction)).AnimationFramesData.FrameTime = currentHit.AnimationData.FrameTime;
                model.AManagers[0].GetAnimation(new AnimationKey(model.AnimationState, model.Direction)).FrameTimeLeft = currentHit.AnimationData.FrameTime;
                ModelAnimationTimeUpdated = true;
            }
        }

        public void Draw(Model model)
        {
            if(BattleBodyData == null)
                return;

            if (BattleBodyData.Sprite == StaticSprites.NONE 
                || (model.ModelState != ModelStates.WEAPON_OUT_IDLE && model.ModelState != ModelStates.WEAPON_OUT_MOVING 
                && model.ModelState != ModelStates.ATTACKING_LIGHT && model.ModelState != ModelStates.ATTACKING_HEAVY
                && model.ModelState != ModelStates.BLOCKING))
                return;

            var currentHit = Combo.GetCurrentHit();
            if (currentHit == null)
            {
                AManager.Update(new AnimationKey(Model.ModelStateToAnimationState(BattleBodyData.ModelStateBetweenHits, model.AnimationState), model.Direction));
            }

            Rectangle spriteSize = model.AManagers[0].GetCurrent().GetCurrentFrame();
            float scaleX = 1f;
            float scaleY = 1f;
            float bodyWidth = model.Body.Width + model.BodyOffset.X;
            float bodyHeight = model.Body.Height + model.BodyOffset.Y;
            scaleX = bodyWidth / spriteSize.Width;
            scaleY = bodyHeight / spriteSize.Height;

            Vector2 entityBodyPos = model.Body.Position.ToVector2();
            float directionXOffset = model.Direction == Directions.RIGHT ? -10 : model.Body.Width * 3f + 10;
            Vector2 entityBodyPosWithOffset = new Vector2(entityBodyPos.X - model.Body.Width / 2f - directionXOffset, entityBodyPos.Y - model.Body.Height / 2f);

            AManager.GetCurrent().Draw(AManager.SpriteSheet, entityBodyPosWithOffset, Color.White, 0f, Vector2.Zero, new Vector2(scaleX, scaleY), 0f);
        }

        public void DrawHitbox()
        {
            var currentHit = Combo.GetCurrentHit();
            if (currentHit == null)
            {
                return;
            }
            Hitbox.Draw(Color.Red);
        }


        public float CalculateFinalSwingTime()
        {
            if(Combo.GetCurrentHit() != null)
                return BattleBodyData.WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier * Combo.GetCurrentHit().SwingTimeSec;

            return 0f;
        }

        public AttackTypes[] GetCurrentAttack(AttackTypes attackToAdd)
        {
            AttackTypes[] history = AttackHistory.ToArray();
            AttackTypes[] currentAttack = new AttackTypes[history.Length + 1];
            for (global::System.Int32 i = 0; i < history.Length; i++)
            {
                currentAttack[i] = history[i];
            }
            currentAttack[currentAttack.Length - 1] = attackToAdd;

            return currentAttack;
        }

        public float CalculatePredictedFinalSwingTime(BattleMovesets set, AttackTypes[] sequence)
        {
            float multipliers = BattleBodyData.WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier;
            if(GetComboHit(set, sequence) != null)
            {
                return multipliers * GetComboHit(set, sequence).SwingTimeSec;
            }
            return multipliers;
        }
    }
}