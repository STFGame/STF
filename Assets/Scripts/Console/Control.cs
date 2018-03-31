using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utility.Enums;

namespace Controller.Mechanism
{
    [Serializable]
    public class Control : IControl
    {
        [SerializeField] private PlayerType playerType;

        private Lever lever;
        private Button[] buttons;

        public Control()
        {
            lever = new Lever();
            buttons = new Button[10];

            for (int i = 0; i < buttons.Length; i++)
                buttons[i] = new Button();
        }

        public Control(PlayerType playerType)
        {
            this.playerType = playerType;
        }

        public void OnUpdate()
        {
            lever.OnUpdate(playerType);

            for (int i = 0; i < buttons.Length; i++)
                buttons[i].OnUpdate(playerType, i + 1);
        }

        public Button GetButton(ButtonType buttonType)
        {
            return buttons[(int)buttonType];
        }

        public Lever Lever
        {
            get { return lever; }
        }
    }
}
