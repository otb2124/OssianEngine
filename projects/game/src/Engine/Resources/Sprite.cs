using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class Sprite : Resource
    {
        public Texture2D texture;
        public Sprite(string path, int id): base(path, id)
        {
            this.Load();
        }

        public override void Load()
        {

            Debug.WriteLine(Directory.GetCurrentDirectory());

            using (FileStream fileStream = new FileStream("../../../../res/sprites/" + this.path, FileMode.Open))
            {
                this.texture = Texture2D.FromStream(Graphics.Graphics.graphicsDeviceManager.GraphicsDevice, fileStream);
            }
        }
    }
}
