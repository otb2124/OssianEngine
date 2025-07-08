using System.Collections.Generic;

namespace Entities
{

    public class EventManager
    {

        public List<Event> events;

        public EventManager()
        {}

        public void Init()
        {
            events = EventSetter.GetEvents(Entities.entityMapManager.CurrentMapId);

            foreach (var Event in events)
            {
                Event.Init();
            }
        }

        public void Update()
        {
            foreach (var Event in events)
            {
                Event.Update();
            }
        }

        public void DrawColliders()
        {
            foreach (Event Event in events)
            {
                if(Event is MapChangeEvent lce)
                {
                    lce.DrawCollider();
                }
            }
        }
    }
}
