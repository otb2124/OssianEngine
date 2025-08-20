using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using static Entities.WeaponComboMovesetFactory;

namespace Entities
{
    public class WeaponComboHitSet
    {
        public List<WeaponComboHit> Combohits;
        public int CurrentComboHitId;
        public float ContinuationAllowCounter;
        public float ContinuationAllowTimeSec;
        public bool AllowContinuation;

        public WeaponComboHitSet()
        {
            Combohits = new List<WeaponComboHit>();
            CurrentComboHitId = 0;
            ContinuationAllowTimeSec = 0.75f;
            ContinuationAllowCounter = 0f;
            AllowContinuation = false;
        }

        public WeaponComboHit GetCurrentHit()
        {
            if (Combohits.Count == 0 || CurrentComboHitId < 0 || CurrentComboHitId >= Combohits.Count)
            {
                return null;
            }
            return Combohits[CurrentComboHitId];
        }

        public bool CanContinueWith(AttackTypes attackType, List<AttackTypes> attackHistory, BattleMovesets set)
        {
            var currentHit = GetCurrentHit();
            if (currentHit == null) return false;

            var hitTemplates = GetWeaponComboHits(set);
            var nextHits = hitTemplates.Where(h => h.AttackSequence.Length == currentHit.AttackSequence.Length + 1 &&
                                                  h.AttackSequence.Take(currentHit.AttackSequence.Length).SequenceEqual(currentHit.AttackSequence) &&
                                                  h.AttackSequence.Last() == attackType).ToList();

            bool canContinue = ContinuationAllowCounter < ContinuationAllowTimeSec && nextHits.Count + 1 > 0;
            return canContinue;
        }

        public void UpdateCounter(float deltaTime, List<AttackTypes> attackHistory)
        {
            if (AllowContinuation)
            {
                ContinuationAllowCounter += deltaTime;
                if (ContinuationAllowCounter >= ContinuationAllowTimeSec)
                {
                    ResetCombo(attackHistory);
                }
            }
        }

        public void UpdateSet(List<AttackTypes> attackHistory, BattleMovesets set)
        {
            UpdateHits(attackHistory, set);
            CurrentComboHitId = 0;
            ContinuationAllowCounter = 0f;
            AllowContinuation = Combohits.Any(h => h.AttackSequence.Length < 3);
        }

        public void ResetCombo(List<AttackTypes> attackHistory)
        {
            CurrentComboHitId = 0;
            ContinuationAllowCounter = 0f;
            AllowContinuation = false;
            Combohits.Clear();
            if (attackHistory != null)
            {
                attackHistory.Clear();
            }
        }

        public void StartContinuationWindow()
        {
            ContinuationAllowCounter = 0f;
            AllowContinuation = Combohits.Any(h => h.AttackSequence.Length < 3);
        }

        public void UpdateHits(List<AttackTypes> attackHistory, BattleMovesets set)
        {
            if (!attackHistory.Any())
            {
                Combohits.Clear();
                return;
            }

            var hitTemplates = GetWeaponComboHits(set);
            Combohits.Clear();
            WeaponComboHit bestMatchHit = null;
            int bestMatchLength = 0;

            foreach (var hit in hitTemplates)
            {
                int matchLength = GetMatchLength(hit.AttackSequence, attackHistory);
                if (matchLength == hit.AttackSequence.Length && matchLength >= bestMatchLength)
                {
                    bestMatchLength = matchLength;
                    bestMatchHit = hit;
                }
            }

            if (bestMatchHit != null)
            {
                Combohits.Add(bestMatchHit);
            }
            else
            {
                var lastAttack = attackHistory.Last(); 
                var defaultHit = hitTemplates.FirstOrDefault(h => h.AttackSequence.Length == 1 && h.AttackSequence[0] == lastAttack);
                Combohits.Add(defaultHit ?? hitTemplates[0]); // Fallback to X
            }
            CurrentComboHitId = 0;
        }

        private int GetMatchLength(AttackTypes[] sequence, List<AttackTypes> history)
        {
            int minLength = Math.Min(sequence.Length, history.Count);
            for (int i = 0; i < minLength; i++)
            {
                if (sequence[i] != history[history.Count - minLength + i])
                    return i;
            }
            return minLength;
        }
    }
}