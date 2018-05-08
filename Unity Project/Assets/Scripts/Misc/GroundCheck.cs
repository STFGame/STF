using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public delegate void GroundChange(bool grounded);
    public event GroundChange GroundEvent;

    private bool grounded = true;

    private void OnTriggerEnter(Collider other)
    {
        OnGround(true);
    }

    private void OnTriggerExit(Collider other)
    {
        OnGround(false);
    }

    private void OnGround(bool grounded)
    {
        if (this.grounded == grounded)
            return;

        this.grounded = grounded;

        if (GroundEvent != null)
            GroundEvent(this.grounded);
    }
}
