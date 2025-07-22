using Entities;
using Physics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Utils
{
    public class ResCommand : IConsoleCommand
    {
        public string Name => "res";
        public string Description => "Ressurect";
        public bool IsForDebug => true;

        public void Execute(string[] args)
        {
            if(!Entities.Entities.entityManager.HasPlayer())
            {
                Entities.Entities.player.Stats.Refill();
                Entities.Entities.player.Model.body.MoveTo(new FlatVector(Graphics.Graphics.camera.position.X, Graphics.Graphics.camera.position.Y));
                Entities.Entities.entityManager.AddEntity(Entities.Entities.player);
                Console.WriteLine("Ressurected.");
            }
            else
            {
                Console.WriteLine("Error ressurect. Detected player alive");
            }
            
        }
    }
}
