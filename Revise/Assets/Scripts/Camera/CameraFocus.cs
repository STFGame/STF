using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Cameras
{
    [Serializable]
    public class CameraFocus
    {
        public Transform focus;

        public float halfXBounds = 20f;
        public float halfYBounds = 15f;
        public float halfZBounds = 15f;

        public Bounds focusBounds;

        public void Update()
        {
            Vector3 position = focus.position;

            Bounds bounds = new Bounds();

            bounds.Encapsulate(new Vector3(position.x - halfXBounds, position.y - halfYBounds, position.z - halfZBounds));
            bounds.Encapsulate(new Vector3(position.x + halfXBounds, position.y + halfYBounds, position.z + halfZBounds));

            focusBounds = bounds;
        }
    }
}
