using Boxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class AttackZone
{
    public BoxArea boxArea;

    [SerializeField] private float maxLength;
    [SerializeField] private float minLength;

    private float enableTimer = 0f;
    private bool enabled = false;

    public Hitbox hitbox;

    public void Initiate(Hitbox hitbox)
    {
        if (hitbox.boxArea == boxArea)
        {
            this.hitbox = hitbox;
            this.hitbox.gameObject.layer = (int)Layer.PlayerDynamic;
        }
    }

    public void EnableHitbox(bool enable)
    {
        enabled = enable || enabled;
        if (enabled)
            enableTimer += Time.deltaTime;

        if (enableTimer > minLength && enableTimer < maxLength)
        {
            hitbox.gameObject.layer = (int)Layer.Hitbox;
            return;
        }

        enabled = false;
        enableTimer = 0;
    }
}
