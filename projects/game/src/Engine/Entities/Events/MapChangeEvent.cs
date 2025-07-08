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


        public MapChangeEvent(int id, Vector2 pos, Vector2 size, Directions direction, int mapTo, Vector2 posTo) : base(id)
        {
            Position = pos;
            Size = size;
            MapTo = mapTo;
            Direction = direction;
            PosTo = posTo;
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
            if(HitboxChecker.CheckForHit(Entities.player.EquipmentManager.GetCurrentArmor().hitbox.extends, LocationChangeHitbox.extends))
            {
                Console.WriteLine("HIT");
                Entities.entityMapManager.ChangeMap(MapTo, PosTo);
            }
        }


        public void DrawCollider()
        {
            LocationChangeHitbox.Draw(Color.Yellow);
        }

    }
}
