using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Player : LivingEntity
    {

        public Player(Vector2 pos, float rotation) : base(FlatBodyFactory.FlatBodyPreset.HUMANOID, pos, rotation)
        {

        }


        public override void Update()
        {
            if(Inputs.Inputs.keyHandler.moveRightPressed)
            {
                body.Move(new FlatVector(1, 0));
            }
            
            if(Inputs.Inputs.keyHandler.moveLeftPressed)
            {
                body.Move(new FlatVector(-1, 0));
            }

            if (Inputs.Inputs.keyHandler.jumpPressed)
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
