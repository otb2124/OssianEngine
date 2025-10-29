using Graphics;
using Microsoft.Xna.Framework;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI;
using Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Entities
{

    public enum ModelAppearanceParts
    {
        BODY,
        BODY_DETAILS,
        ARMOR,
    };

    public class ModelAppearancePart
    {
        public List<AnimationSet> AnimationsSets;

        public ModelAppearanceParts Type;
        public ModelAppearancePart(ModelAppearanceParts modelAppearanceParts, List<AnimationSet> sets) 
        {
            Type = modelAppearanceParts;
            AnimationsSets = sets;
        }

        public ModelAppearancePart(ModelAppearanceParts modelAppearanceParts)
        {
            Type = modelAppearanceParts;
            AnimationsSets = new List<AnimationSet>();
        }

        public void AddAnimationSet(AnimationSet animationSet)
        {
            AnimationsSets.Add(animationSet);
        }

        public void Update(AnimationKey animationKey)
        {
            foreach (AnimationSet set in AnimationsSets)
            {
                set.Update(animationKey);
            }
        }

        public void Draw(FlatBody body, Vector2 BodyOffset, float DrawAngle)
        {
            foreach (AnimationSet aManager in AnimationsSets)
            {
                //Model
                Animation animation = aManager.GetCurrent();
                Rectangle spriteSize = animation.GetCurrentFrame();
                float scaleX = 1f;
                float scaleY = 1f;
                Vector2 newPos = new Vector2(body.Position.X, body.Position.Y);
                Vector2 textureCenter = new Vector2(spriteSize.Width / 2f, spriteSize.Height / 2f);

                float bodyWidth = body.Width + BodyOffset.X;
                float bodyHeight = body.Height + BodyOffset.Y;

                if (body.BodyShapeType == BodyShapeType.Box)
                {
                    scaleX = bodyWidth / (spriteSize.Width - animation.AnimationFramesData.EachFrameSizeOffset.X);
                    scaleY = bodyHeight / (spriteSize.Height - animation.AnimationFramesData.EachFrameSizeOffset.Y);
                    newPos = FlatConverter.ToVector2(body.Position) - new Vector2(bodyWidth / 2f, bodyHeight / 2f);
                    newPos += new Vector2(spriteSize.Width / 2f * scaleX, spriteSize.Height / 2f * scaleY);
                    newPos += new Vector2(animation.AnimationFramesData.EachFramePositionOffset.X * scaleX, animation.AnimationFramesData.EachFramePositionOffset.Y * scaleY);
                }
                else
                {
                    scaleX = body.Radius / spriteSize.Width * 2;
                    scaleY = body.Radius / spriteSize.Height * 2;
                    newPos = FlatConverter.ToVector2(body.Position) - new Vector2(body.Radius, body.Radius);
                    newPos += new Vector2(spriteSize.Width / 2f * scaleX, spriteSize.Height / 2f * scaleY);
                }

                aManager.DrawCurrent(newPos, Color.White, DrawAngle, textureCenter, new Vector2(scaleX, scaleY), 0f);
            }
        }
    }

    public class ModelAppearance
    {

        public List<ModelAppearancePart> AppearanceParts;

        public ModelAppearance()
        {
            AppearanceParts = new List<ModelAppearancePart>();
        }

        public List<AnimationSet> GetAnimationSets(ModelAppearanceParts partType)
        {
            if (AppearanceParts == null)
                return new List<AnimationSet>();

            var part = AppearanceParts.FirstOrDefault(p => p.Type == partType);
            return part?.AnimationsSets ?? new List<AnimationSet>();
        }

        public void Update(AnimationKey animationKey)
        {
            foreach (ModelAppearancePart part in AppearanceParts)
            {
                part.Update(animationKey);
            }
        }


        public void Draw(FlatBody body, Vector2 BodyOffset, float DrawAngle)
        {
            foreach (ModelAppearancePart part in AppearanceParts)
            {
                part.Draw(body, BodyOffset, DrawAngle);
            }
        }
    }
}
