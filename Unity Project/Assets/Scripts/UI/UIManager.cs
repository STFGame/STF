using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        //[SerializeField] private GameObject cursorOne;
        //[SerializeField] private GameObject cursorTwo;
        //[SerializeField] private GameObject cursorThree;
        //[SerializeField] private GameObject cursorFour;

        [SerializeField] private Cursor[] cursors;

        private void Awake()
        {
            
        }

        private void Update()
        {
        }
    }
}
