using Microsoft.Xna.Framework;

namespace UI
{
    public class UIWarningWindowComponent : UIComponent
    {

        public UIWarningWindowComponent(int id) : base(id)
        {
            Vector2 margins = new Vector2(10, 80);
            Point iconSrcRectSize = new Point(64, 64);
            Vector2 buttonSize = new Vector2(iconSrcRectSize.X, iconSrcRectSize.Y);
            Vector2 buttonMargins = new Vector2(8, 12);

            Vector2 frameSize = new Vector2(80, (buttonSize.Y + buttonMargins.Y) * 6);
            Position = new Vector2(0 + margins.X, Graphics.Graphics.screen.Height - frameSize.Y - margins.Y);

            type = UIComponentTypes.WARNING_WINDOW;

            children = new UIComponent[3];
            children[0] = new UIFrameComponent(-1, Position, frameSize);

            children[1] = new UIButtonTextFrameComponent(-1, 10, Position, "Yes", 0, Vector2.One, Vector2.Zero);
            children[2] = new UIButtonTextFrameComponent(-1, 10, Position, "No", 0, Vector2.One, Vector2.Zero);
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
