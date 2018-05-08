using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private float offsetX;
        [SerializeField] private float offsetY;

        private void Update()
        {
            transform.position = new Vector2(Screen.width + offsetX, Screen.height + offsetY);
        }
    }
}
