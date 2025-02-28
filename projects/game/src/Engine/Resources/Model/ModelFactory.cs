using Microsoft.Xna.Framework;
using System;

namespace Resources
{
    public static class ModelFactory
    {

        public enum Models
        {
            HERO,
            CRATE_BIG,
            CRATE_SMALL,
            BALL,
            PLATFORM,
            MOB
        }

        public static Model createModel(Models model)
        {
            Vector2 offset;

            switch (model)
            {
                case Models.HERO:
                    offset = new Vector2(10, 5);
                    return new Model(
                        FlatBodyFactory.createFlatBody(FlatBodyFactory.FlatBodyPreset.HUMANOID, offset),
                        StaticSpriteFactory.GetSprite(StaticSpriteFactory.StaticSprites.HERO))
                    { bodyOffset = offset };
                case Models.CRATE_BIG:
                    return new Model(
                        FlatBodyFactory.createFlatBody(FlatBodyFactory.FlatBodyPreset.BLOCK),
                        StaticSpriteFactory.GetSprite(StaticSpriteFactory.StaticSprites.CRATE));
                case Models.CRATE_SMALL:
                    return new Model(FlatBodyFactory.createFlatBody(
                        FlatBodyFactory.FlatBodyPreset.BOX),
                        StaticSpriteFactory.GetSprite(StaticSpriteFactory.StaticSprites.CRATE));
                case Models.BALL:
                    return new Model(FlatBodyFactory.createFlatBody(
                        FlatBodyFactory.FlatBodyPreset.CIRCLE),
                        StaticSpriteFactory.GetSprite(StaticSpriteFactory.StaticSprites.CIRCLE));
                case Models.PLATFORM:
                    return new Model(FlatBodyFactory.createFlatBody(
                        FlatBodyFactory.FlatBodyPreset.PLATFORM),
                        StaticSpriteFactory.GetSprite(StaticSpriteFactory.StaticSprites.PLATFORM));
                case Models.MOB:
                    offset = new Vector2(10, 5);
                    return new Model(FlatBodyFactory.createFlatBody(
                        FlatBodyFactory.FlatBodyPreset.HUMANOID, offset),
                        StaticSpriteFactory.GetSprite(StaticSpriteFactory.StaticSprites.MOB))
                    { bodyOffset = offset };
                default:
                    throw new ArgumentOutOfRangeException(nameof(model), model, null);
            }
        }



        public static StaticSpriteFactory.StaticSprites GetSpriteFromModel(Models model)
        {
            switch (model)
            {
                case Models.HERO:
                    return StaticSpriteFactory.StaticSprites.HERO;
                case Models.CRATE_BIG:
                    return StaticSpriteFactory.StaticSprites.CRATE;
                case Models.CRATE_SMALL:
                    return StaticSpriteFactory.StaticSprites.CRATE;
                case Models.BALL:
                    return StaticSpriteFactory.StaticSprites.CIRCLE;
                case Models.PLATFORM:
                    return StaticSpriteFactory.StaticSprites.PLATFORM;
                case Models.MOB:
                    return StaticSpriteFactory.StaticSprites.MOB;
                default:
                    throw new ArgumentException("Unknown model type", nameof(model));
            }
        }


    }
}
