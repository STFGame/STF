using Actor.Encounters;
using System;
using UnityEngine;
using Utility.Events;

namespace Actor
{
    public abstract class ActorEncounter<U> : MonoBehaviour
    {
        protected Encounter<U> encounter;
        public void SetEncounter<T>() where T : Encounter<U>
        {
            T item = (T)Activator.CreateInstance(typeof(T));
            encounter = item;
        }
        public abstract Encounter<U> Encounter { get; }
    }
}
