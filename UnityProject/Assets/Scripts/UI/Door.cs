using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform[] doors;

        [SerializeField] private float smoothDamp = 0f;

        private Vector3 doorVelocity;

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < doors.Length; i++)
            {
                Vector3 currentScale = doors[i].transform.localScale;
                Vector3 targetScale = new Vector3(0f, 1f, 1f);

                Vector3 smoothing = Vector3.SmoothDamp(currentScale, targetScale, ref doorVelocity, smoothDamp);

                doors[i].transform.localScale = smoothing;
            }
        }
    }
}