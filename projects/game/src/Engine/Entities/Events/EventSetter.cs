using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class EventSetter
    {

        public static List<Event> GetEvents(int mapId)
        {
            List<Event> events = new List<Event>();

            switch(mapId)
            {
                case 0:
                    events = new List<Event>()
                    {
                        { new MapChangeEvent(0, new Vector2(500, -400), new Vector2(40, 200), Utils.Directions.RIGHT, 1, new Vector2(0, -400)) }
                    };
                    break;
                case 1:
                    events = new List<Event>()
                    {
                        { new MapChangeEvent(0, new Vector2(-500, -400), new Vector2(40, 200), Utils.Directions.RIGHT, 0, new Vector2(0, 100)) }
                    };
                    break;
            }
            

            return events;
        }
    }
}
