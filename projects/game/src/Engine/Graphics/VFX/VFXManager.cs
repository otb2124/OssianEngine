using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class VFXManager
    {


        public List<VFX> VFXList;

        public VFXManager() 
        {
            VFXList = new List<VFX>();
        }

        public void Init()
        {
            AddVFX(VFXs.EXPLOSION, new Vector2(0, 0), new Vector2(1, 1));
        }

        public void AddVFX(VFXs type, Vector2 pos, Vector2 size)
        {
            VFXList.Add(new VFX(type, pos, size, AnimationSetSetter.CreateAnimationSetBySpriteSheet(VFX.VFXSpriteSheetMap[type])));
        }

        public void AddSingleVFX(VFXs type, Vector2 pos, Vector2 size)
        {
            if (!VFXList.Any(vfx => vfx.Type == type))
            {
                VFXList.Add(new VFX(type, pos, size, AnimationSetSetter.CreateAnimationSetBySpriteSheet(VFX.VFXSpriteSheetMap[type])));
            }
        }

        public void Update()
        {
            for (int i = VFXList.Count - 1; i >= 0; i--)
            {
                VFX vfx = VFXList[i];
                vfx.Update();

                if (vfx.WasPlayed)
                {
                    VFXList.RemoveAt(i);
                }
            }
        }

        public void Draw()
        {
            foreach (VFX vfx in VFXList)
            {
                vfx.Draw();
            }
        }
    }
}
