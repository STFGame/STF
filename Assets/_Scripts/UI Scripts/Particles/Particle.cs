using System;
using UnityEngine;

namespace Particles
{
    public enum ParticleType { None, Hit, Shield }

    [Serializable]
    public class Particle
    {
        [SerializeField] private GameObject m_particlePrefab = null;
        [SerializeField] private ParticleType m_particleType = ParticleType.None;

        private ParticleSystem[] m_particleSystem = null;
        private bool m_isNull = true;

        public ParticleType ParticleType { get { return m_particleType; } }

        public void InstantiateParticle(Transform parent, Vector3 position)
        {
            if (m_isNull)
            {
                m_particlePrefab = GameObject.Instantiate(m_particlePrefab, parent) as GameObject;
                m_particlePrefab.transform.position = position;
                m_particleSystem = m_particlePrefab.GetComponentsInChildren<ParticleSystem>();

                m_isNull = false;

                return;
            }

            for (int i = 0; i < m_particleSystem.Length; i++)
            {
                m_particleSystem[i].transform.position = position;
                m_particleSystem[i].Clear();
                m_particleSystem[i].Play();
            }
        }
    }
}
