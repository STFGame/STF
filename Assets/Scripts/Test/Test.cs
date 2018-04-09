using Controller.Mechanism;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Utility.Enums;

public class Test : MonoBehaviour
{
    private const int REPS = 100000;

    public Lever lever;
    public Button[] buttons = new Button[10];

    private Stopwatch stopwatch;
    private Rect drawRect;
    private StringBuilder stringBuilder;

    private void Start()
    {
        stopwatch = new Stopwatch();
        drawRect = new Rect(0, 0, Screen.width, Screen.height);
        stringBuilder = new StringBuilder();

        for (int i = 0; i < buttons.Length; i++)
            buttons[i] = new Button();
    }

    private void OnGUI()
    {
        stringBuilder.Length = 0;

        stopwatch.Reset();
        stopwatch.Start();
        for (int i = 0; i < REPS; i++)
        {
            //bool e = Input.GetKey(KeyCode.Joystick1Button0);
            if (GetButton(ButtonType.Action1).Click)
                GetButton(ButtonType.Action1).Click = false;
        }
        stopwatch.Stop();
        stringBuilder.Append(stopwatch.ElapsedMilliseconds);

        GUI.Label(drawRect, stringBuilder.ToString());
    }

    private Button GetButton(ButtonType type)
    {
        return buttons[(int)type];
    }
}
