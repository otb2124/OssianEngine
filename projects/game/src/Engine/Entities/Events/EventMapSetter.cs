using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Entities
{
    public static class EventMapSetter
    {

        public static List<Event> FillEventMap(int mapId)
        {
            List<Event> events = new List<Event>();

            switch(mapId)
            {
                case 0:
                    events = new List<Event>()
                    {
                        { new MapChangeEvent(0, new Vector2(-350, 150), new Vector2(40, 200), Utils.Directions.LEFT, 1, new Vector2(1100, -500), MapChangeEvent.MapChangeEvents.INTERACT_PRESSED) },
                        { new MapChangeEvent(0, new Vector2(0, -600), new Vector2(40, 200), Utils.Directions.LEFT, 1, new Vector2(0, -500), MapChangeEvent.MapChangeEvents.INTERACT_PRESSED) }
                    };
                    break;
                case 1:
                    events = new List<Event>()
                    {
                        { new MapChangeEvent(0, new Vector2(1150, -500), new Vector2(40, 200), Utils.Directions.RIGHT, 0, new Vector2(-300, 150)) },
                        { new MapChangeEvent(0, new Vector2(0, -500), new Vector2(40, 200), Utils.Directions.RIGHT, 0, new Vector2(0, 150), MapChangeEvent.MapChangeEvents.INTERACT_PRESSED) }
                    };
                    break;
            }
            

            return events;
        }
    }
}
