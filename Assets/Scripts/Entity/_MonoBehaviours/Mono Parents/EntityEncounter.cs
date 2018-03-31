using Entity.Components;
using System;
using Entity.Encounters;
using UnityEngine;
using Utility.Identifer;

namespace Entity
{
    public class EntityEncounter : MonoBehaviour
    {
        [SerializeField] private Groundbox groundbox = new Groundbox();

        private void Awake()
        {
            foreach (Transform item in GetComponentsInChildren<Transform>())
            {
                if (Armature.Search(item.name, out string side))
                {
                    print(side);
                    Box.Create(Tag.Hurtbox, "Hurtbox", item).transform.SetParent(item, false);
                    Box.Create(Tag.Attackbox, "Attackbox", item).transform.SetParent(item, false);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            groundbox.OnCollisionEnter(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            groundbox.OnCollisionExit(collision);
        }

        #region Properties
        public Groundbox Groundbox
        {
            get { return groundbox; }
        }
        #endregion
    }
}
