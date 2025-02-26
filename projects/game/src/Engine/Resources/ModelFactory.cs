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
            FlatBodyFactory.FlatBodyPreset bodyPreset = FlatBodyFactory.FlatBodyPreset.HUMANOID;
            SpriteFactory.Sprites spritePreset = SpriteFactory.Sprites.HERO;

            switch (model)
            {
                case Models.HERO:
                    bodyPreset = FlatBodyFactory.FlatBodyPreset.HUMANOID;
                    spritePreset = SpriteFactory.Sprites.HERO;
                    break;
                case Models.CRATE_BIG:
                    bodyPreset = FlatBodyFactory.FlatBodyPreset.BLOCK;
                    spritePreset = SpriteFactory.Sprites.CRATE;
                    break;
                case Models.CRATE_SMALL:
                    bodyPreset = FlatBodyFactory.FlatBodyPreset.BOX;
                    spritePreset = SpriteFactory.Sprites.CRATE;
                    break;
                case Models.BALL:
                    bodyPreset = FlatBodyFactory.FlatBodyPreset.CIRCLE;
                    spritePreset = SpriteFactory.Sprites.CIRCLE;
                    break;
                case Models.PLATFORM:
                    bodyPreset = FlatBodyFactory.FlatBodyPreset.PLATFORM;
                    spritePreset = SpriteFactory.Sprites.PLATFORM;
                    break;
                case Models.MOB:
                    bodyPreset = FlatBodyFactory.FlatBodyPreset.HUMANOID;
                    spritePreset = SpriteFactory.Sprites.MOB;
                    break;
            }

            return new Model(FlatBodyFactory.createFlatBody(bodyPreset), ResourceLoader.sprites[spritePreset]);
        }



        public static SpriteFactory.Sprites GetSpriteFromModel(Models model)
        {
            switch (model)
            {
                case Models.HERO:
                    return SpriteFactory.Sprites.HERO;
                case Models.CRATE_BIG:
                    return SpriteFactory.Sprites.CRATE;
                case Models.CRATE_SMALL:
                    return SpriteFactory.Sprites.CRATE;
                case Models.BALL:
                    return SpriteFactory.Sprites.CIRCLE;
                case Models.PLATFORM:
                    return SpriteFactory.Sprites.PLATFORM;
                case Models.MOB:
                    return SpriteFactory.Sprites.MOB;
                default:
                    throw new ArgumentException("Unknown model type", nameof(model));
            }
        }


    }
}
