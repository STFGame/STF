using Controller.Mechanism;
using Actor.Collisions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Enums;

/* ACTOR
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
    #endregion
    public abstract class Actor : MonoBehaviour
    {
        private static Dictionary<PlayerNumber, GameObject> _actorDictionary = new Dictionary<PlayerNumber, GameObject>();
        private BodyArea bodyArea;

        protected Component component;
        protected IControl control;

        protected bool onGround = true;
        protected bool isHit;

        protected virtual void Awake()
        {
            GetComponent<ActorCollider<bool>>().SetEncounter<Groundbox>();
            component = GetComponent<Component>();
            control = GetComponent<IControl>();

            if (!_actorDictionary.ContainsValue(gameObject))
                _actorDictionary.Add(control.PlayerNumber, gameObject);
        }

        protected virtual void OnEnable()
        {
            Subscribe<ActorCollider<bool>, bool>(Update_OnGround);
            Subscribe<ActorTrigger<Hitbox>, Hitbox>(Update_IsHit);
        }
        protected virtual void OnDisable()
        {

            Unsubscribe<ActorCollider<bool>, bool>(Update_OnGround);
            Unsubscribe<ActorTrigger<Hitbox>, Hitbox>(Update_IsHit);
        }

        private void Subscribe<T, U>(Action<U> method) where T : ActorEncounter<U>
        {
            foreach (T item in GetComponentsInChildren<T>())
                item.Encounter.Action += method;
        }

        private void Unsubscribe<T, U>(Action<U> method) where T : ActorEncounter<U>
        {
            foreach (T item in GetComponentsInChildren<T>())
                item.Encounter.Action -= method;
        }

        public static GameObject GetPlayer(PlayerNumber playerNumber)
        {
            GameObject gameObject = null;
            _actorDictionary.TryGetValue(playerNumber, out gameObject);
            return gameObject;
        }

        #region Action Methods
        private void Update_OnGround(bool value)
        {
            onGround = value;
            print(onGround);
        }

        private void Update_IsHit(Hitbox value)
        {
            print(value.BodyArea);
        }
        #endregion
    }
}
