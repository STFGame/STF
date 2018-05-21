using UnityEngine;

namespace Managers
{
    public static class PlayerSettings
    {
        private static GameObject[] m_CharacterPrefabs = new GameObject[4];

        public static int NumberOfCharacters { get; private set; }

        public static void SetCharacter(GameObject character, int playerNumber)
        {
            m_CharacterPrefabs[playerNumber] = character;

            NumberOfCharacters = 0;
            for (int i = 0; i < m_CharacterPrefabs.Length; i++)
                NumberOfCharacters = (m_CharacterPrefabs[i] != null) ? NumberOfCharacters + 1 : NumberOfCharacters;
        }

        public static GameObject GetCharacter(PlayerNumber playerNumber)
        {
            if ((int)playerNumber > m_CharacterPrefabs.Length)
                return null;

            return m_CharacterPrefabs[(int)playerNumber - 1];
        }

        public static GameObject GetCharacter(int playerNumber)
        {
            if (playerNumber >= m_CharacterPrefabs.Length)
                return null;

            return m_CharacterPrefabs[playerNumber];
        }
    }
}