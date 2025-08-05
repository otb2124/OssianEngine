using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using static Entities.WeaponComboHitSetFactory;
using Model = Resources.Model;

namespace Entities
{
    public class WeaponEntity
    {
        public WeaponHitbox hitbox;
        public StaticSprites sprite;
        public AnimationManager[] aManagers;
        private List<AttackTypes> attackHistory;
        public WeaponComboHitSets MoveSet;
        private bool ComboHistoryUpdated = false;

        public readonly float GlobalWeaponSwingSpeedMultiplier = 0.6f;
        public float WeaponSwingSpeedMultiplier = 1f;
        public float currentSwingTime = 0f;
        public bool isSwinging = false;

        public Vector2 Size;

        public WeaponComboHitSet Combo;

        private Dictionary<int, int> animationIndexMap;

        public WeaponEntity()
        {
            hitbox = new WeaponHitbox();
            animationIndexMap = new Dictionary<int, int>();
            attackHistory = new List<AttackTypes>();
            Combo = new WeaponComboHitSet();
        }

        public void Init()
        {
            var hits = GetWeaponComboHits(MoveSet);
            sprite = StaticSprites.ENTITIES_WEAPONS_TERRABLADE;
            int totalHits = GetTotalComboHits(MoveSet);
            aManagers = new AnimationManager[totalHits];
            int animationIndex = 0;

            for (int i = 0; i < hits.Length; i++)
            {
                aManagers[animationIndex] = new AnimationManager();
                float eachFrameDuration = hits[i].SwingTimeSec * WeaponSwingSpeedMultiplier / 4 * GlobalWeaponSwingSpeedMultiplier;
                aManagers[animationIndex].AddAnimationForBothDirections(
                    StaticSpriteFactory.spriteMappings[sprite],
                    AnimationStates.IDLE,
                    4,
                    new Vector2(0, 0),
                    new Vector2(128, 128),
                    eachFrameDuration
                );
                animationIndexMap[i] = animationIndex++;
            }

            Combo.UpdateHits(attackHistory, MoveSet);
        }

        public void Update(Model model)
        {
            float deltaTime = (float)Graphics.Graphics.CurrentLogicTime/(float)Graphics.Graphics.TimeScale;

            if (model.modelState == ModelStates.ATTACKING_LIGHT || model.modelState == ModelStates.ATTACKING_HEAVY)
            {
                AttackTypes currentAttack = model.modelState == ModelStates.ATTACKING_LIGHT ? AttackTypes.LIGHT : AttackTypes.HEAVY;
                UpdateComboSelection(currentAttack);
                UpdateHitbox(model);
                UpdateSwingAndCombo(model, currentAttack, deltaTime);
                UpdateAnimation(model.direction);
            }
            else
            {
                Combo.UpdateCounter(deltaTime, attackHistory);
                hitbox.Update(Vector2.Zero, Vector2.Zero);
                isSwinging = false;
                ComboHistoryUpdated = false;
            }
        }

        private void UpdateComboSelection(AttackTypes currentAttack)
        {
            if (ComboHistoryUpdated)
                return;

            ComboHistoryUpdated = true;
            attackHistory.Add(currentAttack);
            int maxComboLength = GetWeaponComboHits(MoveSet).Max(h => h.AttackSequence.Length);
            if (attackHistory.Count > maxComboLength)
                attackHistory.RemoveAt(0);

            Console.WriteLine($"Attack History: [{string.Join(", ", attackHistory)}]");
            Combo.UpdateHits(attackHistory, MoveSet);
        }

        private void UpdateHitbox(Model model)
        {
            var currentHit = Combo.GetCurrentHit();
            if (currentHit == null)
            {
                model.modelState = ModelStates.WEAPON_OUT_IDLE;
                return;
            }

            int horizontalXFactor = model.direction == Directions.RIGHT ? 1 : -1;
            Vector2 weaponPosition = model.body.Position.ToVector2() + currentHit.HitboxPositionOffset * new Vector2(horizontalXFactor, 1f);
            hitbox.Update(
                weaponPosition,
                Size,
                currentHit.HitboxRotationOffset * horizontalXFactor
            );
        }

        private void UpdateSwingAndCombo(Model model, AttackTypes currentAttack, float deltaTime)
        {
            if (!isSwinging)
            {
                isSwinging = true;
                currentSwingTime = 0f;

                if (Combo.CanContinueWith(currentAttack, attackHistory, MoveSet))
                {
                    Combo.UpdateSet(attackHistory, MoveSet);
                }
                else
                {
                    Combo.ResetCombo(attackHistory);
                    Combo.UpdateHits(attackHistory, MoveSet);
                }
                Combo.StartContinuationWindow();

                var currentHit = Combo.GetCurrentHit();
                if (currentHit == null)
                {
                    return;
                }

                int hitIndex = Array.IndexOf(GetWeaponComboHits(MoveSet), currentHit);
                if (!animationIndexMap.ContainsKey(hitIndex))
                {
                    Console.WriteLine($"UpdateSwingAndCombo: Invalid hit index {hitIndex}, resetting");
                    Combo.ResetCombo(attackHistory);
                    Combo.UpdateHits(attackHistory, MoveSet);
                    return;
                }
                int animationIndex = animationIndexMap[hitIndex];
                aManagers[animationIndex].GetCurrent().Reset();
                aManagers[animationIndex].GetCurrent().Start();

                int horizontalXFactor = model.direction == Directions.RIGHT ? 1 : -1;
                model.body.Move(new FlatVector(
                    currentHit.EntityPositionOffset.X * horizontalXFactor,
                    currentHit.EntityPositionOffset.Y
                ));
                Sounds.Sounds.SoundManager.AddSoundSource(new Sounds.SoundSource(
                    Resources.Sounds.SWING_SWORD,
                    model.body.Position.ToVector2(),
                    currentHit.SwingTimeSec * WeaponSwingSpeedMultiplier
                ));
                Console.WriteLine($"Playing Combo Hit Sequence [{string.Join(", ", currentHit.AttackSequence)}]");
            }

            currentSwingTime += deltaTime;

            var hit = Combo.GetCurrentHit();
            if (hit != null && currentSwingTime >= WeaponSwingSpeedMultiplier * GlobalWeaponSwingSpeedMultiplier * hit.SwingTimeSec)
            {
                isSwinging = false;
                model.modelState = ModelStates.WEAPON_OUT_IDLE;
                Console.WriteLine($"Finished Combo Hit Sequence [{string.Join(", ", hit.AttackSequence)}]");
                var hitTemplates = GetWeaponComboHits(MoveSet);
                var nextHits = hitTemplates.Where(h => h.AttackSequence.Length == hit.AttackSequence.Length + 1 &&
                                                      h.AttackSequence.Take(hit.AttackSequence.Length).SequenceEqual(hit.AttackSequence)).ToList();
                if (!nextHits.Any())
                {
                    attackHistory.Clear();
                    Console.WriteLine("Attack History Cleared: Combo Finished");
                    Combo.UpdateHits(attackHistory, MoveSet);
                }
            }
        }

        private void UpdateAnimation(Directions direction)
        {
            var currentHit = Combo.GetCurrentHit();
            if (currentHit == null)
            {
                return;
            }

            int hitIndex = Array.IndexOf(GetWeaponComboHits(MoveSet), currentHit);
            if (!animationIndexMap.ContainsKey(hitIndex))
            {
                Console.WriteLine($"UpdateAnimation: Invalid hit index {hitIndex}, skipping");
                return;
            }
            int animationIndex = animationIndexMap[hitIndex];
            aManagers[animationIndex].Update(new Tuple<Directions, AnimationStates>(direction, AnimationStates.IDLE));
        }

        public void Draw(Model model)
        {
            if (model.modelState != ModelStates.ATTACKING_LIGHT && model.modelState != ModelStates.ATTACKING_HEAVY)
                return;

            var currentHit = Combo.GetCurrentHit();
            if (currentHit == null)
            {
                return;
            }

            Rectangle spriteSize = model.aManager.GetCurrent().GetCurrentFrame();
            float scaleX = 1f;
            float scaleY = 1f;
            float bodyWidth = model.body.Width + model.bodyOffset.X;
            float bodyHeight = model.body.Height + model.bodyOffset.Y;
            scaleX = bodyWidth / spriteSize.Width;
            scaleY = bodyHeight / spriteSize.Height;

            Vector2 entityBodyPos = model.body.Position.ToVector2();
            float directionXOffset = model.direction == Directions.RIGHT ? -10 : model.body.Width * 3f + 10;
            Vector2 entityBodyPosWithOffset = new Vector2(entityBodyPos.X - model.body.Width / 2f - directionXOffset, entityBodyPos.Y - model.body.Height / 2f);

            int hitIndex = Array.IndexOf(WeaponComboHitSetFactory.GetWeaponComboHits(MoveSet), currentHit);
            if (!animationIndexMap.ContainsKey(hitIndex))
            {
                Console.WriteLine($"Draw: Invalid hit index {hitIndex}, skipping");
                return;
            }
            int animationIndex = animationIndexMap[hitIndex];
            aManagers[animationIndex].GetCurrent().Draw(entityBodyPosWithOffset, Color.White, 0f, Vector2.Zero, new Vector2(scaleX, scaleY), 0f);
        }

        public void DrawHitbox()
        {
            var currentHit = Combo.GetCurrentHit();
            if (currentHit == null)
            {
                return;
            }
            hitbox.Draw(Color.Red);
        }
    }
}