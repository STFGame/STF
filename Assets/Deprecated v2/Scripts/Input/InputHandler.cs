using InControl;
using Player.Locomotion;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public enum PlayerNumber { Player1, Player2, Player3, Player4, Count }
    public PlayerNumber playerNum;

    Movement movement;
    IUpdate update;
    InputDevice inputDevice;

    private void Start()
    {
        movement = GetComponent<Movement>();

        string name = Input.GetJoystickNames()[(int)playerNum];

        update = GetComponent<IUpdate>();

        print(update);

        inputDevice = (InputManager.Devices.Count > (int)playerNum) ? InputManager.Devices[(int)playerNum] : null;
        if (inputDevice == null)
            throw new System.Exception("ERROR");
    }

    private void Update()
    {
        movement.OnUpdate();
    }

    private void FixedUpdate()
    {
        update.OnFixedUpdate(inputDevice);
        //movement.OnFixedUpdate(inputDevice);
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(100f, 100f, 100f, 100f), "Input device ");
    }
}
