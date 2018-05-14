using Character;
using Misc;
using Survival;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private PlayerManager playerManager = null;
        private TimeManager timeManger = null;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            timeManger = GetComponent<TimeManager>();
        }

        private void Update()
        {
            UpdateTime();
        }

        private void UpdateTime()
        {
            if (!timeManger)
                return;

            timeManger.UpdateTime();
        }
    }
}