using Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class that manages the UI.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        #region UIManager Variables
        UICursorManager UICursorManager = null;

        public static bool MatchReady { get; private set; }
        #endregion

        #region Load
        private void Awake()
        {
            UICursorManager = GetComponent<UICursorManager>();
        }
        #endregion

        #region Updates
        private void Update()
        {

        }


        #endregion
    }
}
