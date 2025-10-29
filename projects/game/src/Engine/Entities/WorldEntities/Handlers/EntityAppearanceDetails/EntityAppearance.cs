using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Entities
{

    public enum EntityAppearanceAttributes
    {
        BODY,
        BODY_DETAILS,
        ARMOR,
    };


    public class EntityAppearanceAttribute
    {
        public EntityAppearanceAttributes Type;
        public SpriteSheets SpriteSheet;
    }
}
