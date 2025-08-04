using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Entities
{
    public class WeaponComboHitSet
    {

        public WeaponComboHit[] Combohits;
        public int CurrentComboHitId;

        public float ContinuationAllowCounter;
        public float ContinuationAllowTimeSec;
        public bool AllowContinuation; 

        public WeaponComboHitSet(WeaponComboHit[] hits)
        {
            CurrentComboHitId = 0;
            ContinuationAllowTimeSec = 0.5f;
            Combohits = hits;
            AllowContinuation = false;
            ContinuationAllowCounter = 0;
        }

        public WeaponComboHit GetCurrentHit()
        {
            return Combohits[CurrentComboHitId];
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
            CurrentComboHitId = (CurrentComboHitId + 1) % Combohits.Length;
            ContinuationAllowCounter = 0f;
            AllowContinuation = true;
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
            AllowContinuation = true;
        }
    }
}
