using Microsoft.Xna.Framework;
using Utils;

namespace Entities
{
    public class EquipmentEntity : NPCEntity
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
