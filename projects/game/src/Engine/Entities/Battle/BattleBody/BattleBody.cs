using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using static Entities.WeaponComboMovesetFactory;
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
        public AnimationData WeaponOutAnimationData;
        public LightSource.LightSourceData LightSourceData;
        public ModelStates ModelStateBetweenHits;

        public BattleBodyData()
        {
        }

        public BattleBodyData(float weaponSwingSpeedMultiplier, StaticSprites sprite, BattleMovesets moveSet, AnimationData weaponOutAnimationData, LightSource.LightSourceData lightSourceData, ModelStates stateBetweenHits = ModelStates.WEAPON_OUT_IDLE)
        {
            WeaponSwingSpeedMultiplier = weaponSwingSpeedMultiplier;
            Sprite = sprite;
            MoveSet = moveSet;
            LightSourceData = lightSourceData;
            ModelStateBetweenHits = stateBetweenHits;
        }
    }

    public class BattleBody
    {
        public WeaponHitbox Hitbox;
        public AnimationManager aManager;
        public List<AttackTypes> AttackHistory;
        private bool ComboHistoryUpdated = false;
        private bool ModelAnimationTimeUpdated = false;

        public readonly float GlobalWeaponSwingSpeedMultiplier = 0.6f;

        public float currentSwingTime = 0f;
        public bool isSwinging = false;
        public WeaponComboHitSet Combo; 
        public WeaponLightSource LightSource;

        public RotatedRectangle NoAttackHitbox;

        public BattleBodyData BattleBodyData;

        // Lerp state
        private Vector2 initialHitboxSize;
        private Vector2 targetHitboxSize;
        private FlatVector initialBodyPosition;
        private FlatVector targetBodyPosition;

        public BattleBody()
        {
            Hitbox = new WeaponHitbox();
            AttackHistory = new List<AttackTypes>();
            Combo = new WeaponComboHitSet();
        }

        public void Init(BattleBodyData data)
        {
            BattleBodyData = data;

            var hits = GetWeaponComboHits(BattleBodyData.MoveSet);
            int totalHits = GetTotalComboHits(BattleBodyData.MoveSet);

            aManager = new AnimationManager();

            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].SetAnimation(BattleBodyData.MoveSet, GlobalWeaponSwingSpeedMultiplier * BattleBodyData.WeaponSwingSpeedMultiplier);

                aManager.AddAnimationForBothDirections(
                    StaticSpriteFactory.spriteMappings[BattleBodyData.Sprite],
                    hits[i].AnimationState,
                    hits[i].AnimationData
                );
            }

            if(BattleBodyData.ModelStateBetweenHits == ModelStates.WEAPON_OUT_IDLE)
            {
                aManager.AddAnimationForBothDirections(
                    StaticSpriteFactory.spriteMappings[BattleBodyData.Sprite],
                    AnimationStates.WEAPON_OUT_IDLE,
                    BattleBodyData.WeaponOutAnimationData
                );
            }
            

            Combo.UpdateHits(AttackHistory, BattleBodyData.MoveSet);


            if (BattleBodyData.LightSourceData != null)
            {
                LightSource = new WeaponLightSource(BattleBodyData.LightSourceData);
            }

            NoAttackHitbox = new Utils.RotatedRectangle(new Vector2(15, 20), new Vector2(10, 30), 0f);
        }

        public void Update(Model model)
        {
            float deltaTime = (float)Graphics.Graphics.CurrentLogicTime/(float)Graphics.Graphics.TimeScale;

            if (LightSource != null)
            {
                if (model.ModelState == ModelStates.ATTACKING_LIGHT || model.ModelState == ModelStates.ATTACKING_HEAVY || model.ModelState == ModelStates.BLOCKING
                    || model.ModelState == ModelStates.WEAPON_OUT_IDLE || model.ModelState == ModelStates.WEAPON_OUT_MOVING)
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

            if (model.ModelState == ModelStates.ATTACKING_LIGHT || model.ModelState == ModelStates.ATTACKING_HEAVY)
            {
                AttackTypes currentAttack = model.ModelState == ModelStates.ATTACKING_LIGHT ? AttackTypes.LIGHT : AttackTypes.HEAVY;
                UpdateComboSelection(currentAttack);
                UpdateHitbox(model);
                UpdateSwingAndCombo(model, currentAttack, deltaTime);
                UpdateAnimation(model);
            }
            else if (model.ModelState == ModelStates.BLOCKING)
            {
                AttackTypes currentAttack = AttackTypes.BLOCK;
                UpdateComboSelection(currentAttack);
                UpdateHitbox(model);
                UpdateSwingAndCombo(model, currentAttack, deltaTime);
                UpdateAnimation(model);
            }
            else
            {
                Combo.UpdateCounter(deltaTime, AttackHistory);
                Hitbox.Update(Vector2.Zero, Vector2.Zero);
                isSwinging = false;
                ComboHistoryUpdated = false;
                ModelAnimationTimeUpdated = false;
            }
        }

        private void UpdateComboSelection(AttackTypes currentAttack)
        {
            if (ComboHistoryUpdated)
                return;

            ComboHistoryUpdated = true;
            AttackHistory.Add(currentAttack);
            int maxComboLength = GetWeaponComboHits(BattleBodyData.MoveSet).Max(h => h.AttackSequence.Length);
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

            if (currentSwingTime > BattleBodyData.WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier * currentHit.HitboxAppearanceTimePeriod.X && currentSwingTime < BattleBodyData.WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier * currentHit.HitboxAppearanceTimePeriod.Y)
            {
                if (currentSwingTime == 0f || initialHitboxSize == Vector2.Zero)
                {
                    initialHitboxSize = Hitbox.extends.Size();
                    targetHitboxSize = currentHit.HitboxOffset.Size();
                }

                float t = Microsoft.Xna.Framework.MathHelper.Clamp((currentSwingTime - appearanceStart) / (appearanceEnd - appearanceStart), 0f, 1f);
                Vector2 lerpedSize = Vector2.Lerp(initialHitboxSize, targetHitboxSize, t);

                Hitbox.Update(
                    weaponPosition,
                    lerpedSize,
                    currentHit.HitboxOffset.Rotation * horizontalXFactor
                );

                targetBodyPosition = initialBodyPosition + new FlatVector(
                    currentHit.EntityPositionOffset.X * horizontalXFactor,
                    currentHit.EntityPositionOffset.Y
                );
                //t = Microsoft.Xna.Framework.MathHelper.Clamp(currentSwingTime / swingDuration, 0f, 1f);
                FlatVector lerpedPosition = FlatVector.Lerp(initialBodyPosition, targetBodyPosition, t);
                model.Body.MoveTo(lerpedPosition);
            }
            else
            {
                Hitbox.Update(Vector2.Zero, Vector2.Zero);
                initialHitboxSize = Vector2.Zero;
                targetHitboxSize = Vector2.Zero;
                initialBodyPosition = model.Body.Position;
                targetBodyPosition = model.Body.Position;
            }
            
        }

        private void UpdateSwingAndCombo(Model model, AttackTypes currentAttack, float deltaTime)
        {
            if (!isSwinging)
            {
                isSwinging = true;
                currentSwingTime = 0f;

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

                int hitIndex = Array.IndexOf(GetWeaponComboHits(BattleBodyData.MoveSet), currentHit);

                aManager.GetCurrent().Reset();
                aManager.GetCurrent().Start();

                Sounds.Sounds.SoundManager.AddSoundSource(new Sounds.SoundSource(
                    Resources.Sounds.SWING_SWORD,
                    model.Body.Position.ToVector2(),
                    currentHit.SwingTimeSec * BattleBodyData.WeaponSwingSpeedMultiplier
                ));
            }

            currentSwingTime += deltaTime;

            var hit = Combo.GetCurrentHit();
            if (hit != null && currentSwingTime >= CalculateFinalSwingTime())
            {
                isSwinging = false;
                model.ModelState = BattleBodyData.ModelStateBetweenHits;
                
                var hitTemplates = GetWeaponComboHits(BattleBodyData.MoveSet);
                var nextHits = hitTemplates.Where(h => h.AttackSequence.Length == hit.AttackSequence.Length + 1 &&
                                                      h.AttackSequence.Take(hit.AttackSequence.Length).SequenceEqual(hit.AttackSequence)).ToList();
                if (!nextHits.Any())
                {
                    AttackHistory.Clear();
                    Combo.UpdateHits(AttackHistory, BattleBodyData.MoveSet);
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
            aManager.Update(new Tuple<Directions, AnimationStates>(model.Direction, Combo.GetCurrentHit().AnimationState));

            model.AnimationState = Combo.GetCurrentHit().AnimationState;

            if (!ModelAnimationTimeUpdated)
            {
                model.aManager.GetAnimation(model.Direction, model.AnimationState).frameTime = currentHit.AnimationData.FrameTime;
                model.aManager.GetAnimation(model.Direction, model.AnimationState).frameTimeLeft = currentHit.AnimationData.FrameTime;
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
                aManager.Update(new Tuple<Directions, AnimationStates>(model.Direction, Model.ModelStateToAnimationState(BattleBodyData.ModelStateBetweenHits, model.AnimationState)));
            }

            Rectangle spriteSize = model.aManager.GetCurrent().GetCurrentFrame();
            float scaleX = 1f;
            float scaleY = 1f;
            float bodyWidth = model.Body.Width + model.bodyOffset.X;
            float bodyHeight = model.Body.Height + model.bodyOffset.Y;
            scaleX = bodyWidth / spriteSize.Width;
            scaleY = bodyHeight / spriteSize.Height;

            Vector2 entityBodyPos = model.Body.Position.ToVector2();
            float directionXOffset = model.Direction == Directions.RIGHT ? -10 : model.Body.Width * 3f + 10;
            Vector2 entityBodyPosWithOffset = new Vector2(entityBodyPos.X - model.Body.Width / 2f - directionXOffset, entityBodyPos.Y - model.Body.Height / 2f);

            aManager.GetCurrent().Draw(entityBodyPosWithOffset, Color.White, 0f, Vector2.Zero, new Vector2(scaleX, scaleY), 0f);
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