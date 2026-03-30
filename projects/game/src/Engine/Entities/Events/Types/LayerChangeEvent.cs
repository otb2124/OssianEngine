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
    public class LayerChangeEvent : Event
    {
        public Hitbox LocationChangeHitbox;
        public Vector2 Position;
        public Vector2 Size;
        public LayerChangeType Type;

        public enum LayerChangeType
        {
            NEXT,
            PREVIOUS,
            BOTH,
        }

        public LayerChangeEvent(int id, Vector2 pos, Vector2 size, LayerChangeType type) : base(id)
        {
            Type = type;
            Position = pos;
            Size = size;
        }

        public override void Init()
        {
            LocationChangeHitbox = new Hitbox();

            base.Init();
        }

        public override void Update()
        {
            LocationChangeHitbox.Update(Position, Size);
            CheckForHit();

            base.Update();
        }


        public void CheckForHit()
        {
            if (HitboxChecker.CheckForHit(Entities.Player.BattleBodyManager.BodyHitbox.extends, LocationChangeHitbox.extends))
            {

                if(Type == LayerChangeType.NEXT || Type == LayerChangeType.BOTH)
                {
                    if (Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.MOVEUPPRESSED])
                    {
                        Entities.EntityMapManager.LoadNextLayer();
                    }
                }

                if (Type == LayerChangeType.PREVIOUS || Type == LayerChangeType.BOTH)
                {
                    if (Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.MOVEDOWNPRESSED])
                    {
                        Entities.EntityMapManager.LoadPreviousLayer();
                    }
                }
            }
        }


        public override void DrawCollider()
        {
            LocationChangeHitbox.Draw(Color.BlueViolet);
        }

    }
}
