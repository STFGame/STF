using Controller.Mechanism;
using UnityEngine;
using Utility.Enums;

namespace Actor
{
    public class ActorControl : MonoBehaviour, IControl
    {
        [SerializeField] private PlayerNumber playerNumber;

        private Lever lever;
        private Button[] buttons;

        private void Awake()
        {
            lever = new Lever();
            buttons = new Button[5];

            for (int i = 0; i < buttons.Length; i++)
                buttons[i] = new Button();
        }

        private void Update()
        {
            lever.OnUpdate(playerNumber);
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].OnUpdate(playerNumber, i + 1);
        }

        public Button GetButton(ButtonType buttonType)
        {
            return buttons[(int)buttonType];
        }

        #region Properties
        public Lever Lever { get { return lever; } }

        public PlayerNumber PlayerNumber { get { return playerNumber; } }
        #endregion
    }
}
