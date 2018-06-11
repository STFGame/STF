using System;
using System.Collections.Generic;
using UnityEngine;

namespace Particles
{
    [Serializable]
    public class ParticleList
    {
        [SerializeField] private List<Particle> m_particleList = null;

        public void CreateParticle(Transform parent, Vector3 position, ParticleType particleType)
        {
            for (int i = 0; i < m_particleList.Count; i++)
            {
                if (m_particleList[i].ParticleType == particleType)
                    m_particleList[i].InstantiateParticle(parent, position);
            }
        }
    }
}
