using Controller.Mechanism;
using Entity.Components;
using Entity.Encounters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Gravity;

/* ENTITY
 * Sean Ryan
 * March 29, 2018
 * 
 * This is a class that inherits from MonoBehaviour. It is the parent class to all other Entity classes. 
 * The purpose of the class is to contain all relevant information that is shared between multiple classes.
 */

namespace Entity
{
    #region Required Components
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(EntityControl))]
    [RequireComponent(typeof(EntityEncounter))]
    #endregion
    public class Entity : MonoBehaviour
    {
        protected Unit unit;
        protected IControl control;

        protected bool onGround;
        protected bool isHit;

        protected virtual void Awake()
        {
            unit = new Unit(this);
            control = GetComponent<IControl>();
        }

        protected virtual void OnEnable()
        {
            Debug.Log("Entity Enabled");

            onGround = GetComponent<EntityEncounter>().Groundbox.OnGround;
            //GetComponent<EntityEncounter>().groundbox.action += Update_OnGround;

            StopAllCoroutines();
            StartCoroutine(Subscribe<EntityBox>(Update_OnGround));

            //foreach (EntityBox item in GetComponentsInChildren<EntityBox>())
            //{
                //Debug.Log(item);
                //StartCoroutine(Subscribe<EntityBox>(item, Update_OnGround));

                ////item.hurtbox.action += Update_IsHit;
            //}
        }

        protected virtual void OnDisable()
        {
            //GetComponent<EntityEncounter>().groundbox.action -= Update_OnGround;

            //StartCoroutine(Subscribe(GetComponent<EntityEncounter>(), Update_OnGround));

            //foreach (EntityBox item in GetComponentsInChildren<EntityBox>())
            //{
                //item.hurtbox.action -= Update_IsHit;
            //}
        }

        #region Subscription IEnumerators
        private IEnumerator Subscribe<T>(Action<bool> method) where T : EntityBox
        {
            yield return new WaitForEndOfFrame();
            foreach (T item in GetComponentsInChildren<T>())
            {
                Debug.Log(item);
                item.hurtbox.action += method;
            }
        }

        private IEnumerator Unsubscribe<T>(Action<bool> method) where T : Encounter<bool>
        {
            yield return new WaitForEndOfFrame();
            foreach (T item in GetComponentsInChildren<T>())
                item.action -= method;
        }
        #endregion

        #region Action Methods
        private void Update_OnGround(bool value)
        {
            onGround = value;
        }

        private void Update_IsHit(bool value)
        {
            isHit = value;
        }
        #endregion
    }
}
