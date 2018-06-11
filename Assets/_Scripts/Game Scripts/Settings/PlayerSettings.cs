using UnityEngine;

namespace Managers
{
    public static class PlayerSettings
    {
        private static GameObject[] n_characterPrefabs = new GameObject[4];
        private static Sprite[] m_characterPortraits = new Sprite[4];

        public static int Length { get; private set; }

        public static void SetCharacter(GameObject character, int playerNumber)
        {
            n_characterPrefabs[playerNumber] = character;

            Length = 0;
            for (int i = 0; i < n_characterPrefabs.Length; i++)
                Length = (n_characterPrefabs[i] != null) ? Length + 1 : Length;
        }

        public static void SetCharacterPortrait(Sprite characterPortrait, int playerNumber)
        {
            m_characterPortraits[playerNumber] = characterPortrait;
        }

        public static GameObject GetCharacter(PlayerNumber playerNumber)
        {
            if ((int)playerNumber > n_characterPrefabs.Length)
                return null;

            return n_characterPrefabs[(int)playerNumber - 1];
        }

        public static GameObject GetCharacter(int playerNumber)
        {
            if (playerNumber >= n_characterPrefabs.Length)
                return null;

            return n_characterPrefabs[playerNumber];
        }

        public static Sprite GetCharacterPortrait(int playerNumber)
        {
            return m_characterPortraits[playerNumber];
        }
    }
}