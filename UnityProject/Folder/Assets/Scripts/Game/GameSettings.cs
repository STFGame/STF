using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Misc
{
    public class GameSettings
    {
        private static string[] characterNames = new string[4];

        public static int NumberOfCharacters{get; private set;}

        public static void SetCharacter(string name, PlayerNumber playerNumber)
        {
            characterNames[(int)playerNumber - 1] = "Characters/" + name;

            NumberOfCharacters = 0;
            for (int i = 0; i < characterNames.Length; i++)
                NumberOfCharacters = (characterNames[i] != null) ? NumberOfCharacters + 1 : NumberOfCharacters;
        }

        public static string GetCharacter(PlayerNumber playerNumber)
        {
            if ((int)playerNumber > characterNames.Length)
                return null;

            return characterNames[(int)playerNumber - 1];
        }

        public static string GetCharacter(int playerNumber)
        {
            if (playerNumber >= characterNames.Length)
                return null;

            return characterNames[playerNumber];
        }

        public static int Length { get { return characterNames.Length; } }
    }
}