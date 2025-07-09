using System.Collections.Generic;

namespace Entities
{

    public class EventManager
    {

        public EventManager()
        {}

        public void Update()
        {
            foreach (Event Event in Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Events)
            {
                Event.Update();
            }
        }

        public void DrawColliders()
        {
            foreach (Event Event in Entities.entityMapManager.maps[Entities.entityMapManager.CurrentMapId].Events)
            {
                if(Event is MapChangeEvent lce)
                {
                    lce.DrawCollider();
                }
            }
        }
    }
}
