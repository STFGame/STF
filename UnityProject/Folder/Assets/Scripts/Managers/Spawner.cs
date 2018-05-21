using Character;
using Misc;
using System;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Class responsible for instantiating different prefabs
    /// </summary>
    [Serializable]
    public class Spawner
    {
        [SerializeField] private string m_PathName = "Path Name";
        [SerializeField] private Transform m_Parent = null;
        [SerializeField] private bool m_CreateInWorldSpace = true;

        private ISpawnable m_Spawnable = null;

        public void LoadResource()
        {
            m_Spawnable = UnityEngine.Object.Instantiate(Resources.Load(m_PathName), m_Parent, m_CreateInWorldSpace) as ISpawnable;
        }

        public ISpawnable GetPrefab()
        {
            return m_Spawnable;
        }
    }
}
