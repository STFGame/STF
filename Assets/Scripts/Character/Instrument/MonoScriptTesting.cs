using Character.Instrument;
using Character.Instrument.Profiles;
using UnityEngine;

public class MonoScriptTesting : MonoBehaviour
{
    string[] playStation4Control = { "Wireless Controller" };
    private void Awake()
    {
        for (int i = 0; i < playStation4Control.Length; i++)
            InstrumentFactory.Register<PlayStation4Profile>(playStation4Control[i]);

        string name = Input.GetJoystickNames()[0];

        Instrument inst = InstrumentFactory.Create("Wireless ");
    }

    private void Update()
    {
    }
}
