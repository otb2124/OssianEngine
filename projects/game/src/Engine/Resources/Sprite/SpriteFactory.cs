using System;

namespace Resources
{
    public static class SpriteFactory
    {
        public enum Sprites
        {
            PLATFORM,
            CRATE,
            CIRCLE,
            HERO,
            CURSOR,
            BACKGROUND,
            MOB,
            DRAGON,
        }

        public static Sprite CreateSprite(Sprites sprite)
        {
            switch (sprite)
            {
                case Sprites.PLATFORM:
                    return new Sprite("entities/platform.png");
                case Sprites.CIRCLE:
                    return new Sprite("entities/ball.png");
                case Sprites.CRATE:
                    return new Sprite("entities/crate.png");
                case Sprites.HERO:
                    return new Sprite("entities/hero.png") { zIndex = 100 };
                case Sprites.MOB:
                    return new Sprite("entities/mob.png") { zIndex = 100 };
                case Sprites.BACKGROUND:
                    return new Sprite("entities/bg.png") { zIndex = -100 };
                case Sprites.DRAGON:
                    return new Sprite("entities/dragon.png") { zIndex = -99 };
                case Sprites.CURSOR:
                    return new Sprite("ui/sprite0.png");
                default:
                    throw new ArgumentOutOfRangeException(nameof(sprite), sprite, null);
            }

        }
    }
}
