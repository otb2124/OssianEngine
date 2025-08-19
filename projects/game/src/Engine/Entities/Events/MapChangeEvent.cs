using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{
    public class MapChangeEvent : Event
    {
        public Hitbox LocationChangeHitbox;
        public Vector2 Position;
        public Vector2 Size;

        public Directions Direction;
        public int MapTo;
        public Vector2 PosTo;

        public MapChangeEvents Type;

        public enum MapChangeEvents
        {
            AUTO,
            INTERACT_PRESSED,
        }

        public MapChangeEvent(int id, Vector2 pos, Vector2 size, Directions direction, int mapTo, Vector2 posTo, MapChangeEvents type = MapChangeEvents.AUTO) : base(id)
        {
            Position = pos;
            Size = size;
            MapTo = mapTo;
            Direction = direction;
            PosTo = posTo;
            Type = type;
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
            if(HitboxChecker.CheckForHit(Entities.Player.EquipmentManager.GetCurrentArmor().hitbox.extends, LocationChangeHitbox.extends))
            {
                if(Type == MapChangeEvents.AUTO)
                {
                    Entities.entityMapManager.GlobalMapTime.AdjustForTravel(GlobalMapTime.MapTravelTimeMap[new Point(Entities.entityMapManager.CurrentMapId, MapTo)]);
                    Entities.entityMapManager.LoadMap(MapTo, PosTo);
                }

                if(Type == MapChangeEvents.INTERACT_PRESSED)
                {
                    if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.INTERACTRESSED])
                    {
                        Entities.entityMapManager.GlobalMapTime.AdjustForTravel(GlobalMapTime.MapTravelTimeMap[new Point(Entities.entityMapManager.CurrentMapId, MapTo)]);
                        Entities.entityMapManager.LoadMap(MapTo, PosTo);
                    }
                }
            }
        }


        public void DrawCollider()
        {
            LocationChangeHitbox.Draw(Color.Purple);
        }

    }
}
