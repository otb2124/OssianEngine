using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using SharpDX.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Utils;
using static Entities.WeaponComboMovesetFactory;
using Color = Microsoft.Xna.Framework.Color;
using Model = Resources.Model;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Entities
{
    public class WeaponBody
    {
        public WeaponHitbox Hitbox;
        public StaticSprites Sprite;
        public AnimationManager aManager;
        public List<AttackTypes> AttackHistory;
        public WeaponMovesets MoveSet;
        private bool ComboHistoryUpdated = false;
        private bool ModelAnimationTimeUpdated = false;

        public readonly float GlobalWeaponSwingSpeedMultiplier = 0.6f;

        public float WeaponSwingSpeedMultiplier;
        public float currentSwingTime = 0f;
        public bool isSwinging = false;

        public WeaponComboHitSet Combo; 

        public AnimationData WeaponOutAnimationData;

        public LightSource.LightSourceData LightSourceData;
        public LightSource LightSource;

        public WeaponBody()
        {
            Hitbox = new WeaponHitbox();
            AttackHistory = new List<AttackTypes>();
            Combo = new WeaponComboHitSet();
        }

        public void Init()
        {
            var hits = GetWeaponComboHits(MoveSet);
            int totalHits = GetTotalComboHits(MoveSet);

            aManager = new AnimationManager();

            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].SetAnimation(MoveSet, GlobalWeaponSwingSpeedMultiplier * WeaponSwingSpeedMultiplier);

                aManager.AddAnimationForBothDirections(
                    StaticSpriteFactory.spriteMappings[Sprite],
                    hits[i].AnimationState,
                    hits[i].AnimationData
                );
            }

            aManager.AddAnimationForBothDirections(
                    StaticSpriteFactory.spriteMappings[Sprite],
                    AnimationStates.WEAPON_OUT_IDLE,
                    WeaponOutAnimationData
                );

            Combo.UpdateHits(AttackHistory, MoveSet);


            if (LightSourceData != null)
            {
                LightSource = new LightSource();
            }
        }

        public void Update(Model model)
        {
            float deltaTime = (float)Graphics.Graphics.CurrentLogicTime/(float)Graphics.Graphics.TimeScale;

            if (model.ModelState == ModelStates.ATTACKING_LIGHT || model.ModelState == ModelStates.ATTACKING_HEAVY)
            {
                if (LightSource != null)
                {
                    Console.WriteLine(LightSource.Id);
                    if(Graphics.Graphics.lightManager.GetEntityById(LightSource.Id) == null)
                    {
                        Graphics.Graphics.lightManager.AddLightSource(LightSource);
                    }
                }

                AttackTypes currentAttack = model.ModelState == ModelStates.ATTACKING_LIGHT ? AttackTypes.LIGHT : AttackTypes.HEAVY;
                UpdateComboSelection(currentAttack);
                UpdateHitbox(model);
                UpdateSwingAndCombo(model, currentAttack, deltaTime);
                UpdateAnimation(model);
            }
            else if (model.ModelState == ModelStates.BLOCKING)
            {
                if (LightSource != null)
                {
                    Console.WriteLine(LightSource.Id);
                    if (Graphics.Graphics.lightManager.GetEntityById(LightSource.Id) == null)
                    {
                        Graphics.Graphics.lightManager.AddLightSource(LightSource);
                    }
                }


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

                if (LightSource != null)
                {
                    if(Graphics.Graphics.lightManager.GetEntityById(LightSource.Id) != null)
                    {
                        Graphics.Graphics.lightManager.GetEntityById(LightSource.Id).Update(Vector2.Zero, null);
                    }
                }
                    
            }
        }

        private void UpdateComboSelection(AttackTypes currentAttack)
        {
            if (ComboHistoryUpdated)
                return;

            ComboHistoryUpdated = true;
            AttackHistory.Add(currentAttack);
            int maxComboLength = GetWeaponComboHits(MoveSet).Max(h => h.AttackSequence.Length);
            if (AttackHistory.Count > maxComboLength)
                AttackHistory.RemoveAt(0);

            Combo.UpdateHits(AttackHistory, MoveSet);
        }

        private void UpdateHitbox(Model model)
        {
            var currentHit = Combo.GetCurrentHit();
            if (currentHit == null)
            {
                model.ModelState = ModelStates.WEAPON_OUT_IDLE;
                return;
            }

            int horizontalXFactor = model.Direction == Directions.RIGHT ? 1 : -1;
            Vector2 weaponPosition = model.Body.Position.ToVector2() + currentHit.HitboxOffset.Position * new Vector2(horizontalXFactor, 1f);

            if(currentSwingTime > WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier * currentHit.HitboxAppearanceTimePeriod.X && currentSwingTime < WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier * currentHit.HitboxAppearanceTimePeriod.Y)
            {
                Hitbox.Update(
                    weaponPosition,
                    currentHit.HitboxOffset.Size(),
                    currentHit.HitboxOffset.Rotation * horizontalXFactor
                );

                if (LightSource != null)
                {
                    Vector2 weaponRotOffset = new Vector2(0, currentHit.HitboxOffset.Height);
                    weaponRotOffset = Vector2.Transform(weaponRotOffset, Matrix.CreateRotationZ(currentHit.HitboxOffset.Rotation));
                    Vector2 lightPos = weaponPosition - (weaponRotOffset * horizontalXFactor);

                    Graphics.Graphics.lightManager.GetEntityById(LightSource.Id).Update(lightPos, LightSourceData);
                }
            }
            else
            {
                Hitbox.Update(
                    Vector2.Zero,
                    Vector2.Zero,
                    0f
                );

                if (LightSource != null)
                    Graphics.Graphics.lightManager.GetEntityById(LightSource.Id).Update(Vector2.Zero, null);
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
                    if (Combo.CanContinueWith(currentAttack, AttackHistory, MoveSet))
                    {
                        Combo.UpdateSet(AttackHistory, MoveSet);
                    }
                    else
                    {
                        Combo.ResetCombo(AttackHistory);
                        Combo.UpdateHits(AttackHistory, MoveSet);
                    }
                    Combo.StartContinuationWindow();
                }
                

                var currentHit = Combo.GetCurrentHit();
                if (currentHit == null)
                {
                    return;
                }

                int hitIndex = Array.IndexOf(GetWeaponComboHits(MoveSet), currentHit);

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
                    currentHit.SwingTimeSec * WeaponSwingSpeedMultiplier
                ));
            }

            currentSwingTime += deltaTime;

            var hit = Combo.GetCurrentHit();
            if (hit != null && currentSwingTime >= CalculateFinalSwingTime())
            {
                isSwinging = false;
                model.ModelState = ModelStates.WEAPON_OUT_IDLE;
                var hitTemplates = GetWeaponComboHits(MoveSet);
                var nextHits = hitTemplates.Where(h => h.AttackSequence.Length == hit.AttackSequence.Length + 1 &&
                                                      h.AttackSequence.Take(hit.AttackSequence.Length).SequenceEqual(hit.AttackSequence)).ToList();
                if (!nextHits.Any())
                {
                    AttackHistory.Clear();
                    Combo.UpdateHits(AttackHistory, MoveSet);
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
            if (Sprite == StaticSprites.NONE 
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
                return WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier * Combo.GetCurrentHit().SwingTimeSec;

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
            float multipliers = WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier;
            if(GetComboHit(set, sequence) != null)
            {
                return multipliers * GetComboHit(set, sequence).SwingTimeSec;
            }
            return multipliers;
        }
    }
}