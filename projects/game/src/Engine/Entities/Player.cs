using Microsoft.Xna.Framework;
using Physics;
using Resources;

namespace Entities
{
    public class Player : LivingEntity
    {

        public Player(Vector2 pos, float rotation) : base(FlatBodyFactory.FlatBodyPreset.HUMANOID, Sprite.Sprites.HERO, pos, rotation)
        {

        }


        public override void Update()
        {
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED])
            {
                body.Move(new FlatVector(1, 0));
            }
            
            if(Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED])
            {
                body.Move(new FlatVector(-1, 0));
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.JUMPPRESSED])
            {
                body.Move(new FlatVector(0, 10));
            }

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

    }
}
