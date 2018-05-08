using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] private float maxGravity = 100f;
    [SerializeField] [Range(0f, 10f)] private float increaseRate = 1f;
    [SerializeField] [Range(0f, 10f)] private float decreaseRate = 1f;

    public float Gravitation { get; private set; }

    private new Rigidbody rigidbody;

    private Vector3 gravityVelocity = Vector3.down;

    // Use this for initialization
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        Gravitation = maxGravity;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rigidbody.AddForce(gravityVelocity);
    }

    public void DecreaseGravity(bool decrease)
    {
        if (decrease)
        {
            if (Gravitation > 0f)
                Gravitation -= decreaseRate / Time.deltaTime;
        }
        else
        {
            if (Gravitation < maxGravity)
                Gravitation += increaseRate / Time.deltaTime;
        }

        Gravitation = Mathf.Clamp(Gravitation, 0f, maxGravity);

        ModifyVelocity(Gravitation);
    }

    private void ModifyVelocity(float gravitation)
    {
        Vector3 targetVelocity = Vector3.down * gravitation;
        gravityVelocity = (targetVelocity - rigidbody.velocity);

        gravityVelocity.x = rigidbody.velocity.x;
        gravityVelocity.y = Mathf.Clamp(gravityVelocity.y, -maxGravity, maxGravity);
        gravityVelocity.z = 0f;
    }
}
