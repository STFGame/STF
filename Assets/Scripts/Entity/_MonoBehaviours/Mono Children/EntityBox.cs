using Entity.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility.Identifer;

namespace Entity
{
    public class EntityBox : MonoBehaviour
    {
        public Hurtbox hurtbox = new Hurtbox();

        private void OnTriggerEnter(Collider other)
        {
            hurtbox.OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            hurtbox.OnTriggerExit(other);
        }
    }
}
