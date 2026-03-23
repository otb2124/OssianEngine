using Myra;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework;

namespace UI
{
    public class UIManager
    {

        public UIDesktop UIDesktop;

        public UIManager() { }

        public void Init(Game game)
        {
            UIDesktop = new UIDesktop();
            UIDesktop.Init(game);
        }

        public void Update()
        {
            if (Entities.Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.TOGGLEMENUPRESSED])
            {
                if(!UIDesktop.HasComponent<UIIngameMenuComponent>())
                {
                    UIDesktop.AddComponent(new UIIngameMenuComponent());
                }
                else
                {
                    UIDesktop.RemoveComponent<UIIngameMenuComponent>();
                }
            }
        }

        public void Draw()
        {
            UIDesktop.Draw();
        }
    }
}