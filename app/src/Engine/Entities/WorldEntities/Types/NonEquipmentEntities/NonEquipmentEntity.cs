using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class NonEquipmentEntity : NPCEntity
    {


        public NonEquipmentEntity() : base()
        {

        }

        public NonEquipmentEntity(Models modelPreset, Vector2 pos, float rotation = 0f) : base(modelPreset, pos, rotation)
        {
            
        }

        public override void SetBattleBodies()
        {
            //BattleBodyManager = new BattleBodyManager();
        }
    }
}
