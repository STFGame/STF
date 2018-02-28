using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    [HideInInspector] public string Name = "New Attack";
    [HideInInspector] public AudioClip Sound;
    [HideInInspector] public float CoolDown = 1.0f;

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility();
}
