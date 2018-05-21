using Life;
using Survival;

namespace Spawners
{
    public class HealthSpawner : Spawner
    {
        private HealthUI[] m_HealthUI = null;

        public override void Execute()
        {
            return;
        }

        public override void Initialise()
        {
            m_HealthUI = new HealthUI[m_ObjectsToSpawn.Length];

            if (m_HealthUI.Length <= 0)
                return;

            for (int i = 0; i < m_ObjectsToSpawn.Length; i++)
                m_HealthUI[i] = m_ObjectsToSpawn[i].GetComponent<HealthUI>();

            PlayerSpawner playerSpawner = GetComponent<PlayerSpawner>();

            if (!playerSpawner)
                return;

            for (int i = 0; i < playerSpawner.Length; i++)
            {
                Health health = playerSpawner.GetObject(i).GetComponent<Health>();
                if (health)
                    m_HealthUI[i].Initialise(health);
            }
        }
    }
}
