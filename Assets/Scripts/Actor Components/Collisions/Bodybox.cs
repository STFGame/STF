using UnityEngine;
using Utility.Enums;

namespace Actor.Collisions
{
    public class Bodybox<T>
    {
        private Encounter<T> encounter;
        private GameObject gameObject;
        private BodyArea bodyArea;

        #region Constructors
        public Bodybox(){}

        public Bodybox(Encounter<T> encounter)
        {
            this.bodyArea = encounter.BodyArea;
            this.encounter = encounter;

            gameObject = new GameObject();
        }
        #endregion

        public void Add<U>() where U : Component
        {
            gameObject.AddComponent<U>();
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

        public Encounter<T> Encounter
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
