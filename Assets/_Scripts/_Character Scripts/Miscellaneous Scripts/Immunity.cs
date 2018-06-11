using Boxes;
using Broadcasts;
using Characters;
using Life;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Effects
{
    /// <summary>
    /// Class that handles making the character immune.
    /// </summary>
    [RequireComponent(typeof(IHealth))]
    public class Immunity : MonoBehaviour, IBroadcast
    {
        [SerializeField] private float m_immunityLength;

        private BroadcastMessage m_message;
        private List<Hurtbox> m_hurtboxes = new List<Hurtbox>();

        public bool IsImmune { get; private set; }

        private void Start()
        {
            BoxArea[] boxAreas = Enum.GetValues(typeof(BoxArea)).Cast<BoxArea>().ToArray();
            for (int i = 0; i < boxAreas.Length; i++)
            {
                Hurtbox hurtbox = GetComponent<BoxManager>().GetBox(BoxType.Hurtbox, boxAreas[i]) as Hurtbox;
                if (hurtbox)
                    m_hurtboxes.Add(hurtbox);
            }
        }

        private void OnEnable()
        {
            GetComponent<IHealth>().HealthChange += HealthChange;
            GetComponent<Evasion>().EvasionEvent += Immune;
        }

        private void OnDisable()
        {
            GetComponent<IHealth>().HealthChange -= HealthChange;
            GetComponent<Evasion>().EvasionEvent -= Immune;
        }

        private void Update()
        {
            if (m_message == Broadcasts.BroadcastMessage.Dead)
                EnableHurtboxes(false);
            else if(!IsImmune)
                EnableHurtboxes(true);
        }

        private void Immune(bool immune, float immunityLength)
        {
            if (m_message == Broadcasts.BroadcastMessage.Dead)
            {
                StopCoroutine(MakeImmune(immunityLength));
                return;
            }

            if(immune)
            {
                StopCoroutine(MakeImmune(immunityLength));
                StartCoroutine(MakeImmune(immunityLength));
            }
        }

        private void HealthChange(float currentHealth)
        {
            if (m_message == Broadcasts.BroadcastMessage.Dead)
            {
                StopCoroutine(MakeImmune(m_immunityLength));
                return;
            }

            StopCoroutine(MakeImmune(m_immunityLength));
            StartCoroutine(MakeImmune(m_immunityLength));
        }

        private IEnumerator MakeImmune(float immunityLength)
        {
            IsImmune = true;
            EnableHurtboxes(!IsImmune);
            gameObject.layer = (int)Layer.PlayerDynamic;

            yield return new WaitForSeconds(immunityLength);

            gameObject.layer = (int)Layer.PlayerStatic;
            IsImmune = false;
            EnableHurtboxes(!IsImmune);
        }

        private void EnableHurtboxes(bool enable)
        {
            for (int i = 0; i < m_hurtboxes.Count; i++)
                m_hurtboxes[i].Enabled(enable);
        }

        public void Inform(BroadcastMessage message)
        {
            m_message = message;
        }
    }
}
