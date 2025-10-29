using Entities;
using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using Utils;
using static Resources.ModelFactory;


namespace Resources
{

    public enum Models
    {
        HUMAN_M,
        CRATE_0, CRATE_1, BALL,
        ITEM_DROP,
        SLIME, BAT,
        PROJECTILE,
        LEDGE,
    }
    public class Model
    {
        public FlatBody Body;
        public StaticSprite SpriteData;

        public ModelAppearance ModelAppearance;

        public Vector2 BodyOffset;

        public Directions Direction;
        public AnimationStates AnimationState;

        public ModelStates ModelState;

        public float DrawAngle = 0;
        public bool UpdatesSurroundingRectangles = false;

        public RotatedRectangle GroundingRectangle;
        public RotatedRectangle CeilingRectangle;
        public RotatedRectangle SidingRectangle;

        public int OwnerId;

        public float highestJumpY = float.MinValue;
        public float HighestJumpY
        {
            get => highestJumpY;
            set => highestJumpY = value;
        }

        public Model()
        {
        }
        public Model(ModelPreset preset)
        {
            BodyOffset = preset.Offset;

            //TODO: FIND A MORE OPTIMIZED WAY
            Body = new FlatBody(preset.Body, preset.Body.Height, preset.Body.Width);
            SpriteData = preset.SpriteData;

            //AManagers = new AnimationSet();
            //AManagers = new AnimationSet[0];

            //TODO: OPTIMIZE TO REMOVE
            SetSurroundingRectangles();
        }

        public virtual void SetSurroundingRectangles()
        {
            GroundingRectangle = CollisionHelper.CreateGroundingRectangle(Body);
            CeilingRectangle = CollisionHelper.CreateCeilingRectangle(Body);
            SidingRectangle = CollisionHelper.CreateSidingRectangle(Body);
        }


        //TODO: OPTIMIZE USING RECTANGLE.UPDATE()
        public virtual void UpdateSurroundingRectangles()
        {
            GroundingRectangle = CollisionHelper.CreateGroundingRectangle(Body);
            CeilingRectangle = CollisionHelper.CreateCeilingRectangle(Body);
            SidingRectangle = CollisionHelper.CreateSidingRectangle(Body);
        }


        public void UpdateAppearance()
        {
            ModelAppearance.Update(new AnimationKey(AnimationState, Direction));
        }

        public void DrawCollider()
        {
            Color drawColor = new Color((byte)Color.Green.R, (byte)Color.Green.G, (byte)Color.Green.B, (byte)64);

            if (this.Body.BodyShapeType == BodyShapeType.Box)
            {
                Graphics.Graphics.shapes.DrawBoxFill(FlatConverter.ToVector2(Body.Position), Body.Width, Body.Height, Body.Angle, drawColor);
            }
            else
            {
                Graphics.Graphics.shapes.DrawCircleFill(FlatConverter.ToVector2(Body.Position), Body.Radius, 26, drawColor);
            }
        }

        public void DrawSurroundigRectangles()
        {
            Color surroundingRectDrawColor = Color.Orange;
            Color drawColor = new Color((byte)surroundingRectDrawColor.R, (byte)surroundingRectDrawColor.G, (byte)surroundingRectDrawColor.B, (byte)64);

            if (Body.BodyShapeType == BodyShapeType.Box)
            {
                Graphics.Graphics.shapes.DrawBoxFill(GroundingRectangle.Position, GroundingRectangle.Width, GroundingRectangle.Height, GroundingRectangle.Rotation, drawColor);
                Graphics.Graphics.shapes.DrawBoxFill(CeilingRectangle.Position, CeilingRectangle.Width, CeilingRectangle.Height, CeilingRectangle.Rotation, drawColor);
                Graphics.Graphics.shapes.DrawBoxFill(SidingRectangle.Position, SidingRectangle.Width, SidingRectangle.Height, SidingRectangle.Rotation, drawColor);
            }
        }

        public void Draw()
        {
            ModelAppearance.Draw(Body, BodyOffset, DrawAngle);
        }

        public void SwapDirection()
        {
            Direction = GetOppositeDirection(Direction);
        }

        public static Directions GetOppositeDirection(Directions direction)
        {
            if(direction == Directions.RIGHT)
            {
                return Directions.LEFT;
            }

            return Directions.RIGHT;
        }

        public static int GetDirectionCoefficient(Directions direction)
        {
            if (direction == Directions.RIGHT)
            {
                return 1;
            }

            return -1;
        }

        public static AnimationStates ModelStateToAnimationState(ModelStates state, AnimationStates defaultCase)
        {
            switch (state)
            {
                case ModelStates.MOVING:
                    return AnimationStates.MOVING;
                case ModelStates.IDLE:
                    return AnimationStates.IDLE;
                case ModelStates.JUMPING:
                    return AnimationStates.JUMPING;
                case ModelStates.JUMPING_AND_MOVING:
                    return AnimationStates.JUMPING;
                case ModelStates.SPRINTING:
                    return AnimationStates.SPRINTING;
                case ModelStates.WEAPON_OUT_IDLE:
                    return AnimationStates.WEAPON_OUT_IDLE;
                case ModelStates.WEAPON_OUT_MOVING:
                    return AnimationStates.WEAPON_OUT_MOVING;
                case ModelStates.ROLLING:
                    return AnimationStates.ROLL;
                case ModelStates.FALLEN:
                    return AnimationStates.FALLEN;
                case ModelStates.FALLING:
                    return AnimationStates.ROLL;
                case ModelStates.JUMPING_DESCENDING:
                    return AnimationStates.JUMPING_DESCENDING;
                case ModelStates.JUMPING_DESCENDING_AND_MOVING:
                    return AnimationStates.JUMPING_DESCENDING;
                case ModelStates.DESCENDING:
                    return AnimationStates.DESCENDING;
                case ModelStates.BLOCKING:
                    return AnimationStates.BLOCKING_SWORD;
                //case ModelStates.HANGING_ON_LEDGE:
                //return AnimationStates.HANGING_ALT;
                case ModelStates.FLYING:
                    return AnimationStates.FLYING;
                case ModelStates.FLYING_AND_MOVING:
                    return AnimationStates.FLYING_AND_MOVING;

                case ModelStates.DOUBLE_JUMPING:
                    return AnimationStates.ROLL;
                case ModelStates.DOUBLE_JUMPING_AND_MOVING:
                    return AnimationStates.ROLL;

                default:
                    return defaultCase;
            }
        }

    }
}
