using Actor.Components;
using System;
using Actor.Encounters;
using UnityEngine;
using Utility.Identifer;
using Utility.Enums;
using System.Collections.Generic;

namespace Actor
{
    public class EntityEncounter : MonoBehaviour
    {
        [SerializeField] private Groundbox groundbox = new Groundbox();
        private List<Box> boxList = new List<Box>();
        private BodyArea bodyArea;

        Dictionary<BodyArea, Box> _dictionary = new Dictionary<BodyArea, Box>();

        private void Awake()
        {
            foreach (Transform item in GetComponentsInChildren<Transform>())
            {
                if (Armature.Search(item.name, ref bodyArea))
                {
                    Box hitbox = new Box(new Hitbox(bodyArea));
                    hitbox.Add<SphereCollider>();
                    hitbox.Add<EntityBox>();
                    hitbox.SetParent(item);
                    hitbox.SetTag(Tag.Attackbox);
                    hitbox.SetName(bodyArea.ToString() + " Hitbox");
                    hitbox.IsTrigger(true);
                    hitbox.GameObject.GetComponent<EntityBox>().encounter = (Hitbox)hitbox.Encounter;

                    Box attackbox = new Box(new Attackbox(bodyArea));
                    attackbox.Add<SphereCollider>();
                    attackbox.Add<EntityBox>();
                    attackbox.SetParent(item);
                    attackbox.SetTag(Tag.Hurtbox);
                    attackbox.SetName(bodyArea.ToString() + " Attackbox");
                    attackbox.IsTrigger(true);
                    attackbox.GameObject.GetComponent<EntityBox>().encounter = (Attackbox)attackbox.Encounter;
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

        public Action<bool> Action
        {
            get { return groundbox.Action; }
        }
        #endregion
    }
}
