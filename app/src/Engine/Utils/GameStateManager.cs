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
            PLAY_STATE,
            PAUSE_STATE,
        }

        public enum GameModes
        {
            PLAY_MODE,
            COLLISION_DEBUG_MODE,
            HITBOX_DEBUG_MODE
        }

        public static GameStates CurrentGameState;
        public static GameModes gameMode;

        public static bool exitRequested;

        public static bool IsDevMode;
        public static bool IsGod;

        public static void SetDefault()
        {
            CurrentGameState = GameStates.PLAY_STATE;
            gameMode = GameModes.PLAY_MODE;
            IsDevMode = true;
            IsGod = false;
        }


        public static void CheckGameStatusChange()
        {
            if (Entities.Entities.Player == null)
                return;

            if (Entities.Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.TOGGLECOLLISIONDEBUGPRESSED] && IsDevMode)
            {
                gameMode = gameMode == GameModes.PLAY_MODE ? GameModes.COLLISION_DEBUG_MODE : GameModes.PLAY_MODE;
            }
            if (Entities.Entities.Player.EntityControlHandler.ControlStateMap[Inputs.KeyHandler.KeyStates.TOGGLEHITBOXDEBUGPRESSED] && IsDevMode)
            {
                gameMode = gameMode == GameModes.PLAY_MODE ? GameModes.HITBOX_DEBUG_MODE : GameModes.PLAY_MODE;
            }
            if (exitRequested)
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
