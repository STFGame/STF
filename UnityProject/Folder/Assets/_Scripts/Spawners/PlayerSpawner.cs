using Character;
using Managers;
using UnityEngine;

namespace Spawners
{
    public class PlayerSpawner : Spawner
    {
        [SerializeField] protected Transform[] m_ObjectParents = null;

        public override void Initialise()
        {
            if (!m_ManualSpawn)
            {
                m_ObjectsToSpawn = new GameObject[PlayerSettings.NumberOfCharacters];
                for (int i = 0; i < PlayerSettings.NumberOfCharacters; i++)
                    m_ObjectsToSpawn[i] = PlayerSettings.GetCharacter(i);
            }
            Spawn();
        }

        private void Spawn()
        {
            for (int i = 0; i < m_ObjectsToSpawn.Length; i++)
            {
                Transform parent = m_ObjectParents[i];
                if (parent)
                    m_ObjectsToSpawn[i] = Instantiate(m_ObjectsToSpawn[i], parent, m_LoadInWorldSpace) as GameObject;
                else
                    m_ObjectsToSpawn[i] = Instantiate(m_ObjectsToSpawn[i]) as GameObject;

                Initialise(m_ObjectsToSpawn[i].GetComponent<CharacterManager>(), i);
            }
        }

        private void Initialise(CharacterManager controller, int number)
        {
            if (!controller)
                return;

            controller.InitialiseDevice(number);
        }

        public override void Execute()
        {
            return;
        }
    }

}

