using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using Model = Resources.Model;

namespace Entities
{
    public class WeaponEntity
    {

        public enum AttackType { Light, Heavy }

        public WeaponHitbox hitbox;
        public StaticSprites sprite;
        public AnimationManager[] aManagers;

        private List<AttackType> attackHistory;

        public float WeaponSwingSpeedMultiplier;
        public float currentSwingTime = 0f;
        public bool isSwinging = false;

        public Vector2 Size;

        public WeaponComboHitSet[] Combos;
        public int CurrentComboId = 0;

        private Dictionary<(int comboId, int hitId), int> animationIndexMap;

        private bool ComboSelected = false;

        public WeaponEntity()
        {
            hitbox = new WeaponHitbox();
            animationIndexMap = new Dictionary<(int comboId, int hitId), int>();
            attackHistory = new List<AttackType>();
        }

        public void Init()
        {
            WeaponSwingSpeedMultiplier = 1f;
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
                    animationIndexMap[(i, j)] = animationIndex++;
                    Console.WriteLine($"Combo {i} Hit {j}: AnimationIndex {animationIndex - 1}, FrameDuration {eachFrameDuration}");
                }
            }
        }



        public void Update(Model model)
        {
            float deltaTime = (float)Graphics.Graphics.gameTime.ElapsedGameTime.TotalSeconds;

            if (model.modelState == ModelStates.ATTACKING_LIGHT || model.modelState == ModelStates.ATTACKING_HEAVY)
            {
                AttackType currentAttack = model.modelState == ModelStates.ATTACKING_LIGHT ? AttackType.Light : AttackType.Heavy;
                UpdateComboSelection(currentAttack);
                UpdateHitbox(model);
                UpdateSwingAndCombo(model, currentAttack, deltaTime);
                UpdateAnimation(model.direction);
            }
            else
            {
                Combos[CurrentComboId].UpdateCounter(deltaTime);
                hitbox.Update(Vector2.Zero, Vector2.Zero);
                isSwinging = false;
                ComboSelected = false;
            }
        }

        private void UpdateComboSelection(AttackType currentAttack)
        {
            if (ComboSelected)
                return;

            ComboSelected = true;
            attackHistory.Add(currentAttack);
            int maxComboLength = Combos.Max(c => c.AttackSequence.Length);
            if (attackHistory.Count > maxComboLength)
                attackHistory.RemoveAt(0);

            // Find the best matching combo
            int bestMatchComboId = CurrentComboId;
            int bestMatchLength = Combos[CurrentComboId].GetMatchLength(attackHistory);
            for (int i = 0; i < Combos.Length; i++)
            {
                int matchLength = Combos[i].GetMatchLength(attackHistory);
                if (matchLength > bestMatchLength ||
                    (matchLength == bestMatchLength && Combos[i].AttackSequence.Length > Combos[bestMatchComboId].AttackSequence.Length))
                {
                    bestMatchComboId = i;
                    bestMatchLength = matchLength;
                }
            }

            // Fallback to Combo 0 (Light) or Combo 1 (Heavy) if no match
            if (bestMatchLength == 0)
            {
                bestMatchComboId = currentAttack == AttackType.Light ? 0 : 1;
                bestMatchLength = 1;
            }

            // Switch or update combo if needed
            if (bestMatchComboId != CurrentComboId || !Combos[CurrentComboId].CanContinueWith(currentAttack))
            {
                Combos[CurrentComboId].ResetCombo();
                CurrentComboId = bestMatchComboId;
                Combos[CurrentComboId].CurrentComboHitId = Math.Max(0, bestMatchLength - 1);
            }
        }

        private int GetMatchLength(AttackType[] sequence, List<AttackType> history)
        {
            int minLength = Math.Min(sequence.Length, history.Count);
            for (int i = 0; i < minLength; i++)
            {
                if (sequence[i] != history[history.Count - minLength + i])
                    return i;
            }
            return minLength;
        }

        private void UpdateHitbox(Model model)
        {
            int horizontalXFactor = model.direction == Directions.RIGHT ? 1 : -1;
            Vector2 weaponPosition = model.body.Position.ToVector2() + Combos[CurrentComboId].GetCurrentHit().HitboxPositionOffset * new Vector2(horizontalXFactor, 1f);
            hitbox.Update(
                weaponPosition,
                Size,
                Combos[CurrentComboId].GetCurrentHit().HitboxRotationOffset * horizontalXFactor
            );
        }


        private void UpdateSwingAndCombo(Model model, AttackType currentAttack, float deltaTime)
        {
            if (!isSwinging)
            {
                isSwinging = true;
                currentSwingTime = 0f;
                if (Combos[CurrentComboId].CanContinueWith(currentAttack))
                {
                    Combos[CurrentComboId].UpdateSet();
                }
                else if (Combos[CurrentComboId].CurrentComboHitId != 0)
                {
                    Combos[CurrentComboId].ResetCombo();
                    
                }
                Combos[CurrentComboId].StartContinuationWindow();

                int animationIndex = animationIndexMap[(CurrentComboId, Combos[CurrentComboId].CurrentComboHitId)];
                aManagers[animationIndex].GetCurrent().Reset();
                aManagers[animationIndex].GetCurrent().Start();

                int horizontalXFactor = model.direction == Directions.RIGHT ? 1 : -1;
                model.body.Move(new FlatVector(
                    Combos[CurrentComboId].GetCurrentHit().EntityPositionOffset.X * horizontalXFactor,
                    Combos[CurrentComboId].GetCurrentHit().EntityPositionOffset.Y
                ));
                Sounds.Sounds.SoundManager.AddSoundSource(new Sounds.SoundSource(
                    Resources.Sounds.SWING_SWORD,
                    model.body.Position.ToVector2(),
                    Combos[CurrentComboId].GetCurrentHit().SwingTimeSec * WeaponSwingSpeedMultiplier
                ));

                Console.WriteLine($"Attack History: [{string.Join(", ", attackHistory)}]");
            }

            currentSwingTime += deltaTime;

            if (currentSwingTime >= WeaponSwingSpeedMultiplier * Combos[CurrentComboId].GetCurrentHit().SwingTimeSec)
            {
                isSwinging = false;
                model.modelState = ModelStates.WEAPON_OUT_IDLE;
            }
        }

        private void UpdateAnimation(Directions direction)
        {
            int animationIndex = animationIndexMap[(CurrentComboId, Combos[CurrentComboId].CurrentComboHitId)];
            aManagers[animationIndex].Update(new Tuple<Directions, AnimationStates>(direction, AnimationStates.IDLE));
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
