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

        public float ContinuationAllowCounter = 0;
        public float ContinuationAllowTimeSec;
        public bool AllowContinuation = false; 

        public WeaponComboHitSet()
        {
            CurrentComboHitId = 0;
            ContinuationAllowTimeSec = 0.5f;
            Combohits = new WeaponComboHit[]
            {
            new WeaponComboHit(0, new Vector2(0, 10), 1.7f, Vector2.Zero),
            new WeaponComboHit(1, new Vector2(0, 10), 1f, new Vector2(20, 0)),
            new WeaponComboHit(2, new Vector2(0, 10), 2f, new Vector2(30, 0)),
            };
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
