using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;

    public float speed;
    public float jumpHeigth;
    private float jumpVelocity;
    public float gravity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        Vector3 direction = Vector3.forward * speed;
        
        if ( controller.isGrounded ) {
            if ( Input.GetKeyDown( KeyCode.Space ) ) {
                jumpVelocity = jumpHeigth;
            }
        } else {
            jumpVelocity -= gravity;
        }

        direction.y = jumpVelocity;

        controller.Move( direction * Time.deltaTime );

    }
}
