using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Management
{
    public class PlayerManager : MonoBehaviour
    {
        private void Awake()
        {
            foreach (Transform item in GetComponentsInChildren<Transform>())
                item.tag = gameObject.tag;
        }
    }
}
