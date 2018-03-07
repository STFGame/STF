using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Character.Instrument
{
    public class Instru : MonoBehaviour
    {
        private float axleX;
        private float axleY;

        public Action<Vector2> AxleUnit;

        private void Update()
        {
            axleX = Input.GetAxis("Horizontal");
            axleY = Input.GetAxis("Vertical");

            if (AxleUnit != null)
                AxleUnit(new Vector2(axleX, axleY));
        }
    }
}
