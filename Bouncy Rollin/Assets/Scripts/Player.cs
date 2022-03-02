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
    Vector2 playerInput;

    [SerializeField]
    Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.forward;

        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInput.y != 0)
        {
            rBody.AddRelativeTorque(accelerationRate * playerInput.y, 0, 0, ForceMode.Force);
        }
    }

    public void OnMove(InputValue input)
    {
        playerInput = input.Get<Vector2>();
    }

    public void OnRoll(InputValue input)
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawRay(transform.position, direction);
    }
}
