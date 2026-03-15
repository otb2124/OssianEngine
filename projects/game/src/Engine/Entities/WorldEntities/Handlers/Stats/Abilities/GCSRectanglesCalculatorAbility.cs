using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class GCSRectanglesCalculatorAbility : EntityAbility
    {

        public bool IsTouchingCeiling;
        public bool IsTouchingWalls;
        public bool IsGrounded;

        public GCSRectanglesCalculatorAbility()
        {
            Type = EntityStatFeatures.GCS;
        }

        public override void Update(StatsManager statsManager, Resources.Model model)
        {
            IsGrounded = CollisionHelper.GetAnyGround(model) != null;
            IsTouchingCeiling = CollisionHelper.GetAnyCeiling(model) != null;
            IsTouchingWalls = CollisionHelper.GetAnyWalls(model) != null;
        }
    }
}
