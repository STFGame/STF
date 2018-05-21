using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using UnityEngine;

namespace Spawners
{
    public class CursorSpawner : Spawner
    {
        [SerializeField] private Transform[] m_CursorParents = null;

        public override void Initialise()
        {
            if (!m_ManualSpawn)
            {
                int numberOfControllers = Input.GetJoystickNames().Length;

                Spawn(numberOfControllers);

                return;
            }

            int numberOfObjects = m_ObjectsToSpawn.Length;

            Spawn(numberOfObjects);
        }

        private void Spawn(int length)
        {
            for (int i = 0; i < length; i++)
            {
                GameObject cursor = Instantiate(m_ObjectsToSpawn[i], m_CursorParents[i], m_LoadInWorldSpace) as GameObject;

                if (!cursor.GetComponent<CursorUI>())
                    return;

                cursor.GetComponent<CursorUI>().Initialise(PlayerNumberUtility.m_PlayerNumbers[i + 1]);
            }
        }

        public override void Execute()
        {
            return;
        }

    }
}
