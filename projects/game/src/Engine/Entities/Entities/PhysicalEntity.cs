using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using Resources;
using static Graphics.Animation;

namespace Entities
{
    public class PhysicalEntity : Entity
    {
        public enum Directions
        {
            LEFT,
            RIGHT
        }

        public Resources.Model model;

        public PhysicalEntity(ModelFactory.Models modelPreset, Vector2 pos, float rotation = 0f) : base() 
        {
            model = ModelFactory.CreateModel(modelPreset);
            model.body.MoveTo(FlatConverter.ToFlatVector(pos));
            model.body.RotateTo(rotation);
            Physics.Physics.flatWorld.AddBody(model.body);
            model.body.owner = this;

            SetAnimations();
        }

        public virtual void SetAnimations()
        {
            this.model.aManager.AddStaticAnimation(this.model.sprite);
        }

        public override void Draw()
        {
            this.model.Draw();
        }

        public override void DrawDebug()
        {
            this.model.DrawDebug();
        }
    }
}
