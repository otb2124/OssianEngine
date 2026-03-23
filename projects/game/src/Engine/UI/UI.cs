using Microsoft.Xna.Framework;

namespace UI
{
    public static class UI
    {
        public static UIManager UIManager;
        

        public static void Init(Game game)
        {
            UIManager = new UIManager();
            UIManager.Init(game);
        }

        public static void Update()
        {
            UIManager.Update();
        }

        public static void Draw()
        {
            UIManager.Draw();
        }
    }
}