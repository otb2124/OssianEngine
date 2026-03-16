using Microsoft.Xna.Framework;
using Entities;

namespace UI
{
    public class UIDebugInfoComponent : UIComponent
    {
        private UITextAreaComponent _area;

        private readonly Vector2 _position;
        private readonly Vector2 _areaSize;
        private readonly int _fontId;

        public UIDebugInfoComponent(int id, Vector2 position, Vector2 areaSize, int fontId = 0) : base(id)
        {
            type = UIComponentTypes.HUD;
            _position = position;
            _areaSize = areaSize;
            _fontId = fontId;
        }

        public override void Update()
        {
            Player player = Entities.Entities.Player;
            if (player == null) return;

            bool isGrounded = player.StatsManager.GetStatAbility<GCSRectanglesCalculatorAbility>().IsGrounded;
            bool allowDblJump = player.StatsManager.GetStatAbility<DoubleJumpAbility>().AllowDoubleJump;
            bool allowJmpDesc = player.StatsManager.GetStatAbility<DescencionAbility>().AllowJumpDescending;
            float velY = player.Model.Body.linearVelocity.Y;
            float posX = player.Model.Body.Position.X;
            float posY = player.Model.Body.Position.Y;
            ModelStates state = player.Model.ModelState;

            string text =
                $"<br><colored_severity=\"debug\">State: {state}</colored>" +
                $"<br><colored_severity=\"debug\">Grounded: {isGrounded} </colored>" +
                $"<br><colored_severity=\"debug\">AllowDoubleJump: {allowDblJump} </colored>:" +
                $"<br><colored_severity=\"debug\">AllowJumpDesc: {allowJmpDesc} </colored>" +
                $"<br><colored_severity=\"debug\">VelocityY: {velY:F0} </colored>" +
                $"<br><colored_severity=\"debug\">Pos: {posX:F0}, {posY:F0}</colored>";

            _area = new UITextAreaComponent(-1, _position, text, _fontId, _areaSize);
            _area.Update();
        }

        public override void DrawDebug()
        {
            _area?.Draw();
        }
    }
}