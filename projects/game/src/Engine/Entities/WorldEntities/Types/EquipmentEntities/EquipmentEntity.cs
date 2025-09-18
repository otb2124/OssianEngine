using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using MathHelper = Utils.MathHelper;

namespace Entities
{
    public class EquipmentEntity : AIEntity
    {

        public EquipmentManager EquipmentManager;

        public EquipmentEntity(Models modelPreset, Vector2 pos, float rot = 0) : base(modelPreset, pos, rot)
        {
            SetEquipment();
        }

        public EquipmentEntity() : base()
        {
            SetEquipment();
        }

        public virtual void SetEquipment()
        {
            EquipmentManager = new EquipmentManager();
        }

        public override void UpdateBattleBodyManager()
        {
            BattleBodyManager.Update(Model, EquipmentManager);
        }

        public override void SetBattleBodies()
        {
            BattleBodyManager = new BattleBodyManager(BattleBodyTypes.WEAPON);
        }
    }
}
