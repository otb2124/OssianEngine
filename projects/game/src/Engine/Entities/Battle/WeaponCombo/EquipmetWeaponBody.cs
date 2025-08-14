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
    public class WeaponBodyData
    {
        public float WeaponSwingSpeedMultiplier;
        public StaticSprites Sprite;
        public WeaponMovesets MoveSet;
        public AnimationData WeaponOutAnimationData;
        public LightSource.LightSourceData LightSourceData;
        public WeaponBodyData() { }
    }
    public class EquipmetWeaponBody
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

        public WeaponBodyData WeaponBodyData;

        public EquipmetWeaponBody()
        {
            Hitbox = new WeaponHitbox();
            AttackHistory = new List<AttackTypes>();
            Combo = new WeaponComboHitSet();
        }

        public void Init(WeaponBodyData data)
        {
            WeaponBodyData = data;

            var hits = GetWeaponComboHits(WeaponBodyData.MoveSet);
            int totalHits = GetTotalComboHits(WeaponBodyData.MoveSet);

            aManager = new AnimationManager();

            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].SetAnimation(WeaponBodyData.MoveSet, GlobalWeaponSwingSpeedMultiplier * WeaponBodyData.WeaponSwingSpeedMultiplier);

                aManager.AddAnimationForBothDirections(
                    StaticSpriteFactory.spriteMappings[WeaponBodyData.Sprite],
                    hits[i].AnimationState,
                    hits[i].AnimationData
                );
            }

            aManager.AddAnimationForBothDirections(
                    StaticSpriteFactory.spriteMappings[WeaponBodyData.Sprite],
                    AnimationStates.WEAPON_OUT_IDLE,
                    WeaponBodyData.WeaponOutAnimationData
                );

            Combo.UpdateHits(AttackHistory, WeaponBodyData.MoveSet);


            if (WeaponBodyData.LightSourceData != null)
            {
                LightSource = new WeaponLightSource(WeaponBodyData.LightSourceData);
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
                        LightSource.Init(Combo, NoAttackHitbox, model, WeaponBodyData.LightSourceData);
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
            int maxComboLength = GetWeaponComboHits(WeaponBodyData.MoveSet).Max(h => h.AttackSequence.Length);
            if (AttackHistory.Count > maxComboLength)
                AttackHistory.RemoveAt(0);

            Combo.UpdateHits(AttackHistory, WeaponBodyData.MoveSet);
        }

        private void UpdateHitbox(Model model)
        {
            var currentHit = Combo.GetCurrentHit();
            int horizontalXFactor = model.Direction == Directions.RIGHT ? 1 : -1;

            if (currentHit == null)
            {
                model.ModelState = ModelStates.WEAPON_OUT_IDLE;
                return;
            }

            
            Vector2 weaponPosition = model.Body.Position.ToVector2() + currentHit.HitboxOffset.Position * new Vector2(horizontalXFactor, 1f);

            if (currentSwingTime > WeaponBodyData.WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier * currentHit.HitboxAppearanceTimePeriod.X && currentSwingTime < WeaponBodyData.WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier * currentHit.HitboxAppearanceTimePeriod.Y)
            {
                Hitbox.Update(
                    weaponPosition,
                    currentHit.HitboxOffset.Size(),
                    currentHit.HitboxOffset.Rotation * horizontalXFactor
                );
            }
            else
            {
                Hitbox.Update(
                    Vector2.Zero,
                    Vector2.Zero,
                    0f
                );
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
                    if (Combo.CanContinueWith(currentAttack, AttackHistory, WeaponBodyData.MoveSet))
                    {
                        Combo.UpdateSet(AttackHistory, WeaponBodyData.MoveSet);
                    }
                    else
                    {
                        Combo.ResetCombo(AttackHistory);
                        Combo.UpdateHits(AttackHistory, WeaponBodyData.MoveSet);
                    }
                    Combo.StartContinuationWindow();
                }
                

                var currentHit = Combo.GetCurrentHit();
                if (currentHit == null)
                {
                    return;
                }

                int hitIndex = Array.IndexOf(GetWeaponComboHits(WeaponBodyData.MoveSet), currentHit);

                aManager.GetCurrent().Reset();
                aManager.GetCurrent().Start();

                int horizontalXFactor = model.Direction == Directions.RIGHT ? 1 : -1;
                model.Body.Move(new FlatVector(
                    currentHit.EntityPositionOffset.X * horizontalXFactor,
                    currentHit.EntityPositionOffset.Y
                ));
                Sounds.Sounds.SoundManager.AddSoundSource(new Sounds.SoundSource(
                    Resources.Sounds.SWING_SWORD,
                    model.Body.Position.ToVector2(),
                    currentHit.SwingTimeSec * WeaponBodyData.WeaponSwingSpeedMultiplier
                ));
            }

            currentSwingTime += deltaTime;

            var hit = Combo.GetCurrentHit();
            if (hit != null && currentSwingTime >= CalculateFinalSwingTime())
            {
                isSwinging = false;
                model.ModelState = ModelStates.WEAPON_OUT_IDLE;
                var hitTemplates = GetWeaponComboHits(WeaponBodyData.MoveSet);
                var nextHits = hitTemplates.Where(h => h.AttackSequence.Length == hit.AttackSequence.Length + 1 &&
                                                      h.AttackSequence.Take(hit.AttackSequence.Length).SequenceEqual(hit.AttackSequence)).ToList();
                if (!nextHits.Any())
                {
                    AttackHistory.Clear();
                    Combo.UpdateHits(AttackHistory, WeaponBodyData.MoveSet);
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

            model.animationState = Combo.GetCurrentHit().AnimationState;

            if(!ModelAnimationTimeUpdated)
            {
                model.aManager.GetAnimation(model.Direction, model.animationState).frameTime = currentHit.AnimationData.FrameTime;
                model.aManager.GetAnimation(model.Direction, model.animationState).frameTimeLeft = currentHit.AnimationData.FrameTime;
                ModelAnimationTimeUpdated = true;
            }
        }

        public void Draw(Model model)
        {
            if (WeaponBodyData.Sprite == StaticSprites.NONE 
                || (model.ModelState != ModelStates.WEAPON_OUT_IDLE && model.ModelState != ModelStates.WEAPON_OUT_MOVING 
                && model.ModelState != ModelStates.ATTACKING_LIGHT && model.ModelState != ModelStates.ATTACKING_HEAVY
                && model.ModelState != ModelStates.BLOCKING))
                return;

            var currentHit = Combo.GetCurrentHit();
            if (currentHit == null)
            {
                aManager.Update(new Tuple<Directions, AnimationStates>(model.Direction, AnimationStates.WEAPON_OUT_IDLE));
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
                return WeaponBodyData.WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier * Combo.GetCurrentHit().SwingTimeSec;

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

        public float CalculatePredictedFinalSwingTime(WeaponMovesets set, AttackTypes[] sequence)
        {
            float multipliers = WeaponBodyData.WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier;
            if(GetComboHit(set, sequence) != null)
            {
                return multipliers * GetComboHit(set, sequence).SwingTimeSec;
            }
            return multipliers;
        }
    }
}