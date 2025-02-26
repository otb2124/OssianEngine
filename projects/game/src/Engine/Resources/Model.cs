using Physics;


namespace Resources
{
    public class Model
    {
        public FlatBody body;
        public Sprite sprite;

        public Model(FlatBody body, Sprite sprite)
        {
            this.body = body;
            this.sprite = sprite;
        }

        public void Draw()
        {
            body.Draw(sprite);
        }

    }
}
