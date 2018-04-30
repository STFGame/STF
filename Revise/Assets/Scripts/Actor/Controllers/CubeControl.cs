using Actor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour
{
    ActorCombat combat;

	// Use this for initialization
	void Start () {
        combat = GetComponent<ActorCombat>();
	}
	
	// Update is called once per frame
	void Update () {
        combat.PerformAttack();
	}
}
