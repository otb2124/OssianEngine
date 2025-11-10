using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class VFXManager
    {


        public List<VFX> VFXs;

        public VFXManager() 
        {
            VFXs = new List<VFX>();
        }


        public void Update()
        {
            foreach (VFX vfx in VFXs)
            {
                vfx.Update();

                if (vfx.WasPlayed)
                {
                    VFXs.Remove(vfx);
                }
            }
        }

        public void Draw()
        {
            foreach (VFX vfx in VFXs)
            {
                vfx.Draw();
            }
        }
    }
}
