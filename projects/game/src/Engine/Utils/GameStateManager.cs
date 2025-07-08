using App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class GameStateManager
    {

        public enum GameStates
        {
            playState,
            pauseState,
            ingameMenuState
        }

        public enum GameModes
        {
            playMode,
            debugMode
        }

        public static GameStates gameState;
        public static GameModes gameMode;

        public static bool exitRequested;

        public static bool IsDevMode;
        public static bool IsGod;

        public static void SetDefault()
        {
            gameState = GameStates.playState;
            gameMode = GameModes.playMode;
            IsDevMode = false;
            IsGod = false;
        }


        public static void CheckGameStatusChange()
        {
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.TOGGLEDEBUGPRESSED] && IsDevMode)
            {
                gameMode = gameMode == GameModes.playMode ? GameModes.debugMode : GameModes.playMode;
            }
            if(exitRequested)
            {
                //system exit
            }
        }

        public static void RequestExit()
        {
            exitRequested = true;
        }
    }
}
