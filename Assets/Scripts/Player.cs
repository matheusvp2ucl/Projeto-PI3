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

    public float rayRadios;
    public LayerMask layer;

    public float HTamanho;

    private int blockMoveRight;
    private int blockMoveLeft;
    private bool isMovingRight;
    private bool isMovingLeft;

    public Animator anim;
    private bool isDead;

    void Start() {
        Debug.Log("Morto = " + isDead);
        controller = GetComponent<CharacterController>();
    }

    void Update() {

        Vector3 direction = Vector3.zero;

        if (controller.isGrounded) {

            if (Input.GetKeyDown(KeyCode.Space)) {
                jumpVelocity = jumpHeigth;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && blockMoveRight != 1 && !isMovingRight) {
                isMovingRight = true;
                StartCoroutine(RightMove());
                blockMoveRight++;
                blockMoveLeft--;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && blockMoveLeft != 1 && !isMovingLeft) {
                isMovingLeft = true;
                StartCoroutine( LeftMove() );
                blockMoveLeft++;
                blockMoveRight--;
            }

        } else {
            jumpVelocity -= gravity;
        }

        OnCollision();

        direction.y = jumpVelocity;

        controller.Move( direction * Time.deltaTime );

    }

    IEnumerator LeftMove() {
        for (float i = 0; i < 10; i += 0.1f ) {
            controller.Move(Vector3.left * Time.fixedDeltaTime * HTamanho);
            yield return null;
        }
        isMovingLeft = false;
    }

    IEnumerator RightMove() {
        for (float i = 0; i < 10; i += 0.1f ) {
            controller.Move( Vector3.right * Time.fixedDeltaTime * HTamanho);
            yield return null;
        }
        isMovingRight = false;
        
    }

    void OnCollision() {
        RaycastHit hit;

        if ( Physics.Raycast( transform.position, transform.TransformDirection(Vector3.forward), out hit, rayRadios, layer) && !isDead ) {
            Debug.Log("Bateu em algum objeto !");
            // Game Over
            anim.SetTrigger("die");
            isDead = true;
            speed = 0;
            jumpHeigth = 0;
            HTamanho = 0;
        }
    }

    public bool isGameOver() {
        return isDead;
    }
}
