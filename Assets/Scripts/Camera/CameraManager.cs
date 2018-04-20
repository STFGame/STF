using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;

    private Vector3 offset;

	// Use this for initialization
	private void Awake ()
    {
        if(target != null)
            offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	private void LateUpdate ()
    {
        if (target == null) return;

        transform.position = target.position + offset;
	}
}
