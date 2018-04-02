using Actor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility.Enums;
using Utility.Identifer;

namespace Actor.Encounters
{
    public class Box
    {
        private Encounter<bool> encounter;
        private GameObject gameObject;
        private BodyArea bodyArea;

        #region Constructors
        public Box(){}

        public Box(Encounter<bool> encounter)
        {
            this.bodyArea = encounter.BodyArea;
            this.encounter = encounter;

            gameObject = new GameObject();
        }
        #endregion

        public void Add<T>() where T : Component
        {
            gameObject.AddComponent<T>();
        }

        public void IsTrigger(bool value)
        {
            gameObject.GetComponent<Collider>().isTrigger = value;
        }

        #region Setters
        public void SetTag(string tag)
        {
            gameObject.tag = tag;
        }

        public void SetName(string name)
        {
            gameObject.name = name;
        }

        public void SetLayer(Transform layer)
        {
            gameObject.layer = layer.transform.gameObject.layer;
        }

        public void SetParent(Transform parent)
        {
            gameObject.transform.SetParent(parent, false);
        }
        #endregion

        #region Properties
        public BodyArea BodyArea
        {
            get { return bodyArea; }
        }

        public Encounter<bool> Encounter
        {
            get { return encounter; }
        }

        public GameObject GameObject
        {
            get { return gameObject; }
        }
        #endregion
    }
}
