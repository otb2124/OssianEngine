using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace UI
{
    class UIFrameComponent : UIComponent
    {
        public UIFrameComponent() : base()
        {
            Position = Vector2.Zero;

            children = new UIComponent[4];
            children[0] = new UIFramePartComponent(UIFramePartComponent.UIFramePartType.CORNER_LT);
            children[1] = new UIFramePartComponent(UIFramePartComponent.UIFramePartType.CORNER_RT);
            children[2] = new UIFramePartComponent(UIFramePartComponent.UIFramePartType.CORNER_LB);
            children[3] = new UIFramePartComponent(UIFramePartComponent.UIFramePartType.CORNER_RB);

            children[0].Position = new Vector2(Position.X, Position.Y);
            children[1].Position = new Vector2(Position.X + 32, Position.Y);
            children[2].Position = new Vector2(Position.X, Position.Y + 32);
            children[3].Position = new Vector2(Position.X + 32, Position.Y + 32);
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
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].Draw();
                }
            }
        }
    }
}
