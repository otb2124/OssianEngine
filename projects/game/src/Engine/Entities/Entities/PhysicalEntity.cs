using Microsoft.Xna.Framework;
using Physics;
using Resources;

namespace Entities
{
    public class PhysicalEntity : SpriteEntity
    {

        public Model model;

        public PhysicalEntity(ModelFactory.Models modelPreset, Vector2 pos, float rotation = 0f) : base(ModelFactory.GetSpriteFromModel(modelPreset), pos) 
        {
            model = ModelFactory.createModel(modelPreset);
            model.body.MoveTo(FlatConverter.ToFlatVector(pos));
            model.body.RotateTo(rotation);
            Physics.Physics.flatWorld.AddBody(model.body);
            model.body.owner = this;
        }


        public override void Draw()
        {
            this.model.Draw();

            //Debug
            //this.body.Draw();

            //base.Draw();
        }
    }
}
