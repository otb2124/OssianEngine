using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System.Diagnostics;

namespace Entities
{
    public class Player : LivingEntity
    {

        public Player(Vector2 pos) : base(ModelFactory.Models.HERO, pos, 0f)
        {
            
        }


        public override void setStats()
        {
            stats.maxHP = 100;
            stats.HP = 100;
            stats.dmg = 1;
            stats.maxSpeed = 1;
            stats.speed = 1;

            base.setStats();
        }


        public override void Update()
        {

            Debug.WriteLine(stats.HP);

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVERIGHTPRESSED])
            {
                model.body.Move(new FlatVector(stats.speed, 0));
            }
            
            if(Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.MOVELEFTPRESSED])
            {
                model.body.Move(new FlatVector(-stats.speed, 0));
            }

            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.JUMPPRESSED])
            {
                model.body.Move(new FlatVector(0, 10));
            }

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }

    }
}
