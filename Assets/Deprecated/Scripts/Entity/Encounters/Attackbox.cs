using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Entity.Encounters
{
    [Serializable]
    public class Attackbox : Encounter<bool>
    {
        [SerializeField] private Collider[] hitboxes;

        private bool hasHit = false;

        public override void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Finish")
                Debug.Log("HIT");
        }
    }
}
