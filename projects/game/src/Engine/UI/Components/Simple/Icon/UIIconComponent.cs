using Graphics;
using Microsoft.Xna.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Resources.StaticSpriteFactory;

namespace UI
{
    public class UIIconComponent : UIComponent
    {


        public UIIconComponent(int id, SpriteData spriteData, Vector2 pos, Vector2 scale) : base(id)
        {
            this.type = UIComponentTypes.ICON;
            this.spriteData = spriteData;
            aManager = new Animator(spriteData);

            this.Position = pos;
            Scale = scale;

            IsStickToCameraState = true;
            IsStickToZoomState = true;
            IsAppliedHalfScreenOriginState = true;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
