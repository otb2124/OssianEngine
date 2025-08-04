using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using Utils;
using Model = Resources.Model;

namespace Entities
{
    public class WeaponEntity
    {

        public WeaponHitbox hitbox;
        public StaticSprites sprite;
        public AnimationManager[] aManagers;

        public float WeaponSwingSpeedMultiplier;
        public float currentSwingTime = 0f;
        public bool isSwinging = false;

        public Vector2 Size;

        public WeaponComboHitSet[] Combos;
        public int CurrentComboId = 0;
        public bool MovedPlayer = false;

        private Dictionary<(int comboId, int hitId), int> animationIndexMap;

        public WeaponEntity()
        {
            hitbox = new WeaponHitbox();
            animationIndexMap = new Dictionary<(int comboId, int hitId), int>();
        }

        public void Init()
        {
            Combos = WeaponComboHitSetFactory.GetWeaponComboHitSets(WeaponComboHitSetFactory.WeaponComboHitSets.SWORD);
            sprite = StaticSprites.ENTITIES_WEAPONS_TERRABLADE;
            int totalHits = WeaponComboHitSetFactory.GetTotalComboHits(WeaponComboHitSetFactory.WeaponComboHitSets.SWORD);
            aManagers = new AnimationManager[totalHits];
            int animationIndex = 0;

            for (int i = 0; i < Combos.Length; i++)
            {
                for (int j = 0; j < Combos[i].Combohits.Length; j++)
                {
                    aManagers[animationIndex] = new AnimationManager();
                    float eachFrameDuration = Combos[i].Combohits[j].SwingTimeSec * WeaponSwingSpeedMultiplier / 4;
                    aManagers[animationIndex].AddAnimationForBothDirections(
                        StaticSpriteFactory.spriteMappings[sprite],
                        AnimationStates.IDLE,
                        4,
                        new Vector2(0, 0),
                        new Vector2(128, 128),
                        eachFrameDuration
                    );
                    animationIndexMap[(i, j)] = animationIndex;
                    animationIndex++;
                }
            }
        }


        public void Update(Model model)
        {
            float deltaTime = (float)Graphics.Graphics.gameTime.ElapsedGameTime.TotalSeconds;

            if (model.modelState == ModelStates.ATTACKING_LIGHT || model.modelState == ModelStates.ATTACKING_HEAVY)
            {
                CurrentComboId = model.modelState == ModelStates.ATTACKING_LIGHT ? 0 : 1;
                int horizontalXFactor = model.direction == Directions.RIGHT ? 1 : -1;
                Vector2 weaponPosition = model.body.Position.ToVector2() + Combos[CurrentComboId].GetCurrentHit().HitboxPositionOffset * new Vector2(horizontalXFactor, 1f);

                hitbox.Update(
                    weaponPosition,
                    Size,
                    Combos[CurrentComboId].GetCurrentHit().HitboxRotationOffset * horizontalXFactor
                );


                if (!isSwinging)
                {
                    isSwinging = true;
                    currentSwingTime = 0f;

                    if (Combos[CurrentComboId].AllowContinuation && Combos[CurrentComboId].ContinuationAllowCounter < Combos[CurrentComboId].ContinuationAllowTimeSec)
                    {
                        Combos[CurrentComboId].UpdateSet();
                    }
                    else
                    {
                        Combos[CurrentComboId].ResetCombo();
                    }
                    Combos[CurrentComboId].StartContinuationWindow();

                    int animationIndex = animationIndexMap[(CurrentComboId, Combos[CurrentComboId].CurrentComboHitId)];
                    aManagers[animationIndex].GetCurrent().Reset();
                    aManagers[animationIndex].GetCurrent().Start();

                    model.body.Move(new FlatVector(Combos[CurrentComboId].GetCurrentHit().EntityPositionOffset.X * horizontalXFactor, Combos[CurrentComboId].GetCurrentHit().EntityPositionOffset.Y));
                    Sounds.Sounds.SoundManager.AddSoundSource(new Sounds.SoundSource(Resources.Sounds.SWING_SWORD, model.body.Position.ToVector2(), Combos[CurrentComboId].GetCurrentHit().SwingTimeSec * WeaponSwingSpeedMultiplier));
                }

                currentSwingTime += deltaTime;

                if (currentSwingTime >= WeaponSwingSpeedMultiplier * Combos[CurrentComboId].GetCurrentHit().SwingTimeSec)
                {
                    isSwinging = false;
                    model.modelState = ModelStates.WEAPON_OUT_IDLE;
                }

                int currentAnimationIndex = animationIndexMap[(CurrentComboId, Combos[CurrentComboId].CurrentComboHitId)];
                aManagers[currentAnimationIndex].Update(new Tuple<Directions, AnimationStates>(model.direction, AnimationStates.IDLE));
            }
            else
            {
                Combos[CurrentComboId].UpdateCounter(deltaTime);
                hitbox.Update(
                    new Vector2(0, 0),
                    new Vector2(0, 0)
                );
                isSwinging = false;
                MovedPlayer = false;
            }
        }

        public void Draw(Model model)
        {
            if (model.modelState != ModelStates.ATTACKING_LIGHT && model.modelState != ModelStates.ATTACKING_HEAVY)
                return;

            Rectangle spriteSize = model.aManager.GetCurrent().GetCurrentFrame();
            float scaleX = 1f;
            float scaleY = 1f;
            float bodyWidth = model.body.Width + model.bodyOffset.X;
            float bodyHeight = model.body.Height + model.bodyOffset.Y;
            scaleX = bodyWidth / spriteSize.Width;
            scaleY = bodyHeight / spriteSize.Height;

            Vector2 entityBodyPos = model.body.Position.ToVector2();

            float directionXOffset = -10;
            if (model.direction == Directions.LEFT)
            {
                directionXOffset = model.body.Width * 3f + 10;
            }

            Vector2 entityBodyPosWithOffset = new Vector2(entityBodyPos.X - model.body.Width / 2f - directionXOffset, entityBodyPos.Y - model.body.Height / 2f);

            int animationIndex = animationIndexMap[(CurrentComboId, Combos[CurrentComboId].CurrentComboHitId)];
            aManagers[animationIndex].GetCurrent().Draw(entityBodyPosWithOffset, Color.White, 0f, Vector2.Zero, new Vector2(scaleX, scaleY), 0f);
        }

        public void DrawHitbox()
        {
            hitbox.Draw(Color.Red);
        }
    }
}
