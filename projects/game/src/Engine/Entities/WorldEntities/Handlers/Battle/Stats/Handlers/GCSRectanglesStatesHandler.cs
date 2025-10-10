using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class GCSRectanglesStatesHandler : EntityStatFeature
    {


        public GCSRectanglesStatesHandler()
        {
            Type = EntityStatFeatures.GCS;
        }

        public override void Update(StatsManager statsManager, Resources.Model model)
        {
            statsManager.IsGrounded = CollisionHelper.GetAnyGround(model) != null;
            statsManager.IsTouchingCeiling = CollisionHelper.GetAnyCeiling(model) != null;
            statsManager.IsTouchingWalls = CollisionHelper.GetAnyWalls(model) != null;
        }
    }
}
