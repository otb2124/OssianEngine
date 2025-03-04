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


        public static void SetDefault()
        {
            gameState = GameStates.playState;
            gameMode = GameModes.playMode;
        }


        public static void CheckGameStatusChange()
        {
            if (Inputs.Inputs.keyHandler.keyStates[Inputs.KeyHandler.KeyStates.DEBUGPRESSED])
            {
                gameMode = gameMode == GameModes.playMode ? GameModes.debugMode : GameModes.playMode;
            }
        }
    }
}
