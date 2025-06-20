using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace UI
{
    class UIFrameComponent : UIComponent
    {
        public UIFrameComponent(Vector2 pos, Vector2 size) : base()
        {
            Position = pos;

            type = ComponentTypes.FRAME;

            children = new UIComponent[9];
            children[0] = new UIFramePartComponent(UIFramePartComponent.UIFramePartType.CORNER_LT, Position, size);
            children[1] = new UIFramePartComponent(UIFramePartComponent.UIFramePartType.CORNER_RT, Position, size);
            children[2] = new UIFramePartComponent(UIFramePartComponent.UIFramePartType.CORNER_LB, Position, size);
            children[3] = new UIFramePartComponent(UIFramePartComponent.UIFramePartType.CORNER_RB, Position, size);
            children[4] = new UIFramePartComponent(UIFramePartComponent.UIFramePartType.BORDER_T, Position, size);
            children[5] = new UIFramePartComponent(UIFramePartComponent.UIFramePartType.BORDER_B, Position, size);
            children[6] = new UIFramePartComponent(UIFramePartComponent.UIFramePartType.BORDER_L, Position, size);
            children[7] = new UIFramePartComponent(UIFramePartComponent.UIFramePartType.BORDER_R, Position, size);
            children[8] = new UIFramePartComponent(UIFramePartComponent.UIFramePartType.INNER, Position, size);
        }

        public override void Update()
        {
            if (children != null)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].Update();
                }
            }
        }

        public override void Draw()
        {
            if (children != null)
            {
                children[8].Draw();

                for (int i = 0; i < 8; i++)
                {
                    children[i].Draw();
                }
            }
        }
    }
}
