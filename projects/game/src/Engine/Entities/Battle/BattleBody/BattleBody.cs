using Graphics;
using static Entities.WeaponComboMovesetFactory;
using Utils;
using Resources;

namespace Entities
{
    public class WeaponBodyData
    {
        public float WeaponSwingSpeedMultiplier;
        public StaticSprites Sprite;
        public WeaponMovesets MoveSet;
        public AnimationData WeaponOutAnimationData;
        public LightSource.LightSourceData LightSourceData;
        public WeaponBodyData() { }
    }

    public class BattleBody
    {
        public WeaponBodyData BattleBodyData;

        public virtual void Init(WeaponBodyData data)
        {

        }

        public virtual void Update(Model model)
        {

        }

        public virtual void Draw(Model model)
        {

        }

        public virtual void DrawHitbox()
        {

        }

    }
}
