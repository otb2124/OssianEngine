using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class IOGameConfig : IOConfig
    {

        public string GameTitle { get; set; } = "My Game";
        public int ResolutionWidth { get; set; } = 1280;
        public int ResolutionHeight { get; set; } = 720;
        public bool Fullscreen { get; set; } = false;
        public bool VSync { get; set; } = true;
        public float MasterVolume { get; set; } = 1.0f;
        public float MusicVolume { get; set; } = 0.8f;
        public float SFXVolume { get; set; } = 1.0f;
        public bool ShowFPS { get; set; } = false;

        public IOGameConfig()
        {
            FilePath = "gameConfig";
        }
    }
}
