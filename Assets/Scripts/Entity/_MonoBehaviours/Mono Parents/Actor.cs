using Controller.Mechanism;
using Actor.Components;
using Actor.Encounters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Gravity;
using Utility.Enums;
using Utility.Events;

/* ENTITY
 * Sean Ryan
 * March 29, 2018
 * 
 * This is a class that inherits from MonoBehaviour. It is the parent class to all other Entity classes. 
 * The purpose of the class is to contain all relevant information that is shared between multiple classes.
 */

namespace Actor
{
    #region Required Components
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(ActorControl))]
    [RequireComponent(typeof(EntityEncounter))]
    #endregion
    public abstract class Actor : MonoBehaviour
    {
        private static Dictionary<PlayerNumber, GameObject> _actorDictionary = new Dictionary<PlayerNumber, GameObject>();

        protected Component component;
        protected IControl control;

        protected bool onGround = true;
        protected bool isHit;

        protected virtual void Awake()
        {
            component = GetComponent<Component>();
            control = GetComponent<IControl>();

            if(!_actorDictionary.ContainsValue(gameObject))
                _actorDictionary.Add(control.PlayerNumber, gameObject);
        }

        private void OnEnable()
        {
            GetComponent<ActorCollider>().SetEncounter<Groundbox>();
            StartCoroutine(Subscribe<ActorCollider>(Update_OnGround));
        }

        private void OnDisable()
        {
            StartCoroutine(Unsubscribe<ActorCollider>(Update_OnGround));
        }

        public static GameObject GetPlayer(PlayerNumber playerNumber)
        {
            GameObject gameObject = null;
            _actorDictionary.TryGetValue(playerNumber, out gameObject);
            return gameObject;
        }

        #region Subscription IEnumerators
        private IEnumerator Subscribe<T>(Action<bool> method) where T : ActorEncounter<bool>
        {
            yield return new WaitForEndOfFrame();
            GetComponent<T>().Encounter.Action += method;
        }

        private IEnumerator Unsubscribe<T>(Action<bool> method) where T : ActorEncounter<bool>
        {
            foreach (T item in GetComponentsInChildren<T>())
                item.Encounter.Action -= method;
            yield break;
        }
        #endregion

        #region Action Methods
        private void Update_OnGround(bool value)
        {
            onGround = value;
            print(onGround);
        }

        private void Update_IsHit(bool value)
        {
            isHit = value;
        }
        #endregion
    }
}
