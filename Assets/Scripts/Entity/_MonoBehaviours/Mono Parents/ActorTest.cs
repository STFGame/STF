using Actor.Components;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace Actor
{
    [RequireComponent(typeof(Rigidbody))]
    public class ActorTest : MonoBehaviour
    {
        private const int REPS = 100000;

        private Rigidbody rb;

        private Unit unit;

        private Stopwatch stopwatch;
        private Rect drawRect;
        private StringBuilder stringBuilder;

        private void Start()
        {
            stopwatch = new Stopwatch();

            rb = GetComponent<Rigidbody>();

            unit = new Unit(this);
            unit.Register<Rigidbody>(GetComponent<Rigidbody>());

            drawRect = new Rect(0, 0, Screen.width, Screen.height);
            stringBuilder = new StringBuilder();
        }

        private void OnGUI()
        {
            stringBuilder.Length = 0;

            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < REPS; i++)
            {
                unit.GetUnit<Rigidbody>().AddForce(Vector3.zero);
                //unit.Rigidbody.AddForce(Vector3.zero);
                //unit.GetUnit<Rigidbody>().AddForce(Vector3.zero);
            }
            stopwatch.Stop();
            stringBuilder.Append("Unit Param: ");
            stringBuilder.Append(stopwatch.ElapsedMilliseconds);
            stringBuilder.Append('\n');

            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < REPS; i++)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.zero);
            }
            stopwatch.Stop();
            stringBuilder.Append("Type Param: ");
            stringBuilder.Append(stopwatch.ElapsedMilliseconds);
            stringBuilder.Append('\n');

            GUI.Label(drawRect, stringBuilder.ToString());
        }

        private void Optimization<T>(string type) where T : Component
        {
            stringBuilder.Length = 0;

            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < REPS; i++)
            {
                unit.Rigidbody.AddForce(Vector3.zero);
            }
            stopwatch.Stop();
            stringBuilder.Append(type + " ");
            stringBuilder.Append(stopwatch.ElapsedMilliseconds);
            stringBuilder.Append('\n');
        }
    }
}
