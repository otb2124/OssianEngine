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
            if(!Entities.Entities.EntityManager.HasPlayer())
            {
                Entities.Entities.Player.StatsManager.RefillAll();
                Entities.Entities.Player.Model.Body.MoveTo(new PhysicalVector(Graphics.Graphics.camera.position.X, Graphics.Graphics.camera.position.Y));
                Entities.Entities.EntityManager.AddEntity(Entities.Entities.Player);
                Console.WriteLine("Ressurected.");
            }
            else
            {
                Console.WriteLine("Error ressurect. Detected Player alive");
            }
            
        }
    }
}
