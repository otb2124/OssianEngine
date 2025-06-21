using Microsoft.Xna.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace UI
{
    public class UIButtonIconComponent : UIComponent
    {


        public Vector2 Size;
        public int ButtonId;

        public UIButtonIconComponent(int id, int buttonid, Vector2 position, StaticSprites sprite) : base(id)
        {
            type = UIComponentTypes.BUTTON_ICON;

            this.sprite = StaticSprites.UI_GAME_ICON;
            this.Init();

            Position = position;
            Size = aManager.GetCurrent().GetCurrentFrame().Size.ToVector2();
            ButtonId = buttonid;

            stickToCamera = true;
            stickToZoom = true;
            applyHalfScreenOrigin = true;
        }


        public override void Update()
        {
            //
            UI.UIButtonHandler.CheckHover(ButtonId, new Vector2(Position.X, Position.Y), Size);

            base.Update();

        }
    }
}
