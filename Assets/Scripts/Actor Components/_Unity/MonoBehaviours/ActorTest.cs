using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace Actor
{
    [RequireComponent(typeof(Rigidbody))]
    public class ActorTest : MonoBehaviour
    {
        private const int REPS = 100000;

        private Stopwatch stopwatch;
        private Rect drawRect;
        private StringBuilder stringBuilder;

        private void Start()
        {
            stopwatch = new Stopwatch();
            drawRect = new Rect(0, 0, Screen.width, Screen.height);
            stringBuilder = new StringBuilder();
        }

        private void OnGUI()
        {
            stringBuilder.Length = 0;

            GUI.Label(drawRect, stringBuilder.ToString());
        }
    }
}
