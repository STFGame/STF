using Characters;
using Life;
using UnityEngine;
using UnityEngine.UI;

namespace Character.UI
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField] private Image m_portrait = null;
        [SerializeField] private HealthUI m_healthUI = null;
        [SerializeField] private ShieldUI m_shieldUI = null;
        [SerializeField] private LifeTokenUI m_lifeTokenUI = null;

        public void Initialise(GameObject player)
        {
            gameObject.SetActive(true);

            m_healthUI.Initialise(player.GetComponent<IHealth>());
            m_shieldUI.Initialise(player.GetComponent<Shield>());
            m_lifeTokenUI.Initialise(player.GetComponent<Death>());
        }
    }
}
