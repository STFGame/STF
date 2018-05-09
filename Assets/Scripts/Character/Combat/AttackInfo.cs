using Boxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackInfo", menuName = "Combat", order = 2)]
public class AttackInfo : ScriptableObject
{
    [SerializeField] private string attackTag = "Base Attack";
    public AttackZone[] attackZones = null;
    public float knockback = 10f;
    public float damage = 5f;

    public void Initiate(Hitbox hitbox)
    {
        AttackID = Animator.StringToHash(attackTag);

        for (int i = 0; i < attackZones.Length; i++)
            attackZones[i].Initiate(hitbox);
    }

    public void EnableAttack(bool enable)
    {
        for (int i = 0; i < attackZones.Length; i++)
            attackZones[i].EnableHitbox(enable);
    }

    public int AttackID { get; private set; }
}
