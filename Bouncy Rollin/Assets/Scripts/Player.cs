using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField]
    Vector3 direction;

    [SerializeField]
    float accelerationRate;

    [SerializeField]
    float maxSpeed;

    [SerializeField]
    float turnRate;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    Vector2 playerInput;

    [SerializeField]
    Rigidbody rBody;

    [SerializeField]
    Vector3 Right
    {
        get
        {
            return Vector3.Cross(direction, Vector3.up);
        }
    }

    [SerializeField]
    Vector3 Forward
    {
        get
        {
            return direction;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.forward;

        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //  turn left/right
        if (playerInput.x != 0)
        {
            Vector3 turnForce = Vector3.up;
            turnForce.y = turnRate * Time.deltaTime;

            if(playerInput.x < 0)
            {
                turnForce *= -1f;
            }

            direction = Quaternion.Euler(turnForce) * direction;
        }
    }

    private void FixedUpdate()
    {
        //  Roll forward/backwards
        if (playerInput.y != 0)
        {
            float moveForce = accelerationRate;// * Time.deltaTime;

            if (playerInput.y > 0)
            {
                moveForce *= -1f;
            }

            rBody.AddTorque(Right * moveForce, ForceMode.Impulse);
        }
    }

    public void OnMove(InputValue input)
    {
        playerInput = input.Get<Vector2>();
    }

    public void OnRoll(InputValue input)
    {

    }

    public void OnJump()
    {
        rBody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawRay(transform.position, Forward);
    }
}
