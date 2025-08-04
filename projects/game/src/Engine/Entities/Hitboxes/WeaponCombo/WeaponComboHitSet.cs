using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using static Entities.WeaponEntity;

namespace Entities
{
    public class WeaponComboHitSet
    {
        public WeaponComboHit[] Combohits;
        public AttackType[] AttackSequence;
        public int CurrentComboHitId;
        public float ContinuationAllowCounter;
        public float ContinuationAllowTimeSec;
        public bool AllowContinuation;

        public WeaponComboHitSet(WeaponComboHit[] hits, AttackType[] attackSequence)
        {
            if (hits.Length != attackSequence.Length)
                throw new ArgumentException("Hits and attack sequence lengths must match.");

            Combohits = hits;
            AttackSequence = attackSequence;
            CurrentComboHitId = 0;
            ContinuationAllowTimeSec = 0.5f;
            ContinuationAllowCounter = 0;
            AllowContinuation = false;
        }

        public WeaponComboHit GetCurrentHit()
        {
            if (CurrentComboHitId < 0 || CurrentComboHitId >= Combohits.Length)
            {
                CurrentComboHitId = 0; // Reset to prevent crash
            }
            return Combohits[CurrentComboHitId];
        }

        public bool CanContinueWith(AttackType attackType)
        {
            bool canContinue = AllowContinuation &&
                              ContinuationAllowCounter < ContinuationAllowTimeSec &&
                              CurrentComboHitId + 1 < AttackSequence.Length &&
                              AttackSequence[CurrentComboHitId + 1] == attackType;
            Console.WriteLine($"CanContinueWith: Attack {attackType}, Next {AttackSequence.ElementAtOrDefault(CurrentComboHitId + 1)}, Allow {AllowContinuation}, Timer {ContinuationAllowCounter}/{ContinuationAllowTimeSec}, CanContinue {canContinue}");
            return canContinue;
        }

        public bool MatchesSequence(List<AttackType> history)
        {
            int minLength = Math.Min(AttackSequence.Length, history.Count);
            for (int i = 0; i < minLength; i++)
            {
                if (AttackSequence[i] != history[history.Count - minLength + i])
                    return false;
            }
            return true;
        }

        public int GetMatchLength(List<AttackType> history)
        {
            int minLength = Math.Min(AttackSequence.Length, history.Count);
            for (int i = 0; i < minLength; i++)
            {
                if (AttackSequence[i] != history[history.Count - minLength + i])
                    return i;
            }
            return minLength;
        }

        public void UpdateCounter(float deltaTime)
        {
            if (AllowContinuation)
            {
                ContinuationAllowCounter += deltaTime;
                if (ContinuationAllowCounter >= ContinuationAllowTimeSec)
                {
                    ResetCombo();
                }
            }
        }

        public void UpdateSet()
        {
            CurrentComboHitId++;
            if (CurrentComboHitId >= Combohits.Length)
                CurrentComboHitId = Combohits.Length - 1; // Stay at last hit
            ContinuationAllowCounter = 0f;
            AllowContinuation = CurrentComboHitId < Combohits.Length - 1;
        }

        public void ResetCombo()
        {
            CurrentComboHitId = 0;
            ContinuationAllowCounter = 0f;
            AllowContinuation = false;
        }

        public void StartContinuationWindow()
        {
            ContinuationAllowCounter = 0f;
            AllowContinuation = CurrentComboHitId < Combohits.Length - 1;
        }
    }
}