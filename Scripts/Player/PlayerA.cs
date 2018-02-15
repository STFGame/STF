using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Player;
using UnityEngine;

class PlayerA : MonoBehaviour
{
    public PlayerManager player = new PlayerManager();

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        player.UpdateInternal(Time.deltaTime, this.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
