using Controller.Mechanism;
using UnityEngine;
using Utility.Enums;

namespace Entity
{
    public class EntityControl : MonoBehaviour, IControl
    {
        [SerializeField] private Control console = new Control();

        private void Update()
        {
            console.OnUpdate();
        }

        public Button GetButton(ButtonType buttonType)
        {
            return console.GetButton(buttonType);
        }

        public Lever Lever
        {
            get { return console.Lever; }
        }
    }
}
