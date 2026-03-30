using Microsoft.Xna.Framework;
using Resources;
using System.Collections.Generic;

namespace Entities
{
    public static class EventMapSetter
    {

        public static List<Event> FillEventMap(int mapId, int layerId)
        {
            List<Event> events = new List<Event>();



            switch(mapId)
            {
                case 0:

                    switch (layerId)
                    {
                        case 0:
                            events = new List<Event>()
                            {
                                { new MapChangeEvent(0, new Vector2(-350, 150), new Vector2(40, 200), Directions.LEFT, 1, 0, new Vector2(1100, -500), MapChangeEvent.MapChangeEvents.INTERACT_PRESSED) },
                                { new MapChangeEvent(0, new Vector2(0, -600), new Vector2(40, 200), Directions.LEFT, 1, 0,new Vector2(0, -500), MapChangeEvent.MapChangeEvents.INTERACT_PRESSED) },
                                { new LayerChangeEvent(0, new Vector2(-100, -600), new Vector2(40, 100), LayerChangeEvent.LayerChangeType.NEXT) }
                            };
                            break;

                        case 1:
                            events = new List<Event>()
                            {
                                { new LayerChangeEvent(0, new Vector2(-100, -600), new Vector2(40, 100), LayerChangeEvent.LayerChangeType.PREVIOUS) }
                            };
                            break;
                    }
                    break;
                case 1:
                    switch (layerId)
                    {
                        case 0:
                            events = new List<Event>()
                            {
                                { new MapChangeEvent(0, new Vector2(1150, -500), new Vector2(40, 200), Directions.RIGHT, 0, 0, new Vector2(-300, 150)) },
                                { new MapChangeEvent(0, new Vector2(0, -500), new Vector2(40, 200), Directions.RIGHT, 0, 0, new Vector2(0, 150), MapChangeEvent.MapChangeEvents.INTERACT_PRESSED) }
                            };
                            break;
                    }
                    break;
            }
            

            return events;
        }
    }
}
