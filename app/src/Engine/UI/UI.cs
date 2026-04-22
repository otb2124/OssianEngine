using Microsoft.Xna.Framework;

namespace UI
{
    public static class UI
    {
        public static UIManager UIManager;

        public static bool PreventButtonPressedOverlap;

        public static void Init(Game game)
        {
            UIManager = new UIManager();
            UIManager.Init(game);

            PreventButtonPressedOverlap = false;
        }

        public static void Setup()
        {
            UIManager.Setup();
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