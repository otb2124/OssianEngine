using Microsoft.Xna.Framework;
using Physics;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using MathHelper = Utils.MathHelper;

namespace Entities
{
    public class BattleEntity : StatsEntity
    {

        public BattleBodyManager BattleBodyManager;

        public BattleEntity(Models modelPreset, Vector2 pos, float rot = 0) : base(modelPreset, pos, rot)
        {
            SetBattleBodies();
        }

        public BattleEntity() : base()
        {
            SetBattleBodies();
        }

        public virtual void SetBattleBodies()
        {
            //BattleBodyManager = new BattleBodyManager(BattleBodyTypes.BODY);
        }


        public virtual void UpdateBattleBodyManager()
        {
            BattleBodyManager.Update(Model);
        }

        public override void Update()
        {
            UpdateBattleBodyManager();
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            BattleBodyManager.Draw(Model);
        }

        public virtual void DrawHitboxes()
        {
            BattleBodyManager.DrawHitboxes();
        }
    }
}
