using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misc
{
    public class CharacterSelection
    {
        private static string[] playerNames = new string[4];

        public static void SetCharacter(string name, PlayerNumber playerNumber)
        {
            playerNames[(int)playerNumber - 1] = "Resources/Characters/" + name;
        }

        public static string GetCharacter(PlayerNumber playerNumber)
        {
            if ((int)playerNumber > playerNames.Length)
                return null;

            return playerNames[(int)playerNumber - 1];
        }

        public static int Length { get { return playerNames.Length; } }
    }
}