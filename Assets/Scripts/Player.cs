using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private CharacterController controller;
    public float score;
    public Text scoreText;
    public	int scoreCoin;
    public Text scoreCoinText;

    public float speed;
    public float jumpHeigth;
    private float jumpVelocity;
    public float gravity;

    public float rayRadios;
    public LayerMask layer;
    public LayerMask coinLayer;

    public float HTamanho;

    private int blockMoveRight;
    private int blockMoveLeft;
    private bool isMovingRight;
    private bool isMovingLeft;

    private bool buttonLeft;
    private bool buttonRight;
    private bool buttonJump;

    public Animator anim;
    private bool isDead;

    private Player player;

    void Start() {
        Debug.Log("Morto = " + isDead);
        controller = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update() {

        Vector3 direction = Vector3.zero;

        if (controller.isGrounded) {

            // Input.GetKeyDown(KeyCode.Space)
            if ( buttonJump ) {
                jumpVelocity = jumpHeigth;
                buttonJump = false;
            }

            // Input.GetKeyDown(KeyCode.RightArrow)
            if ( buttonRight && blockMoveRight != 1 && !isMovingRight) {
                isMovingRight = true;
                buttonRight = false;
                StartCoroutine(RightMove());
                blockMoveRight++;
                blockMoveLeft--;
            }

            // Input.GetKeyDown(KeyCode.LeftArrow)
            if ( buttonLeft && blockMoveLeft != 1 && !isMovingLeft) {
                isMovingLeft = true;
                buttonLeft = false;
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

        if(!player.isDead)
        {
	        score += Time.deltaTime * 2f;
	        scoreText.text = Mathf.Round(score).ToString();
	    }

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

    void OnCollision() 
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayRadios, layer) && !isDead ) {
            Debug.Log("Bateu em algum objeto!");
            // Game Over
            anim.SetTrigger("die");
            isDead = true;
            speed = 0;
            jumpHeigth = 0;
            HTamanho = 0;
        }

        RaycastHit coinHit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(0,1f,0)), out coinHit, rayRadios, coinLayer)) {
        	AddCoin();
        	Destroy(coinHit.transform.gameObject);
        }
    }

    public bool isGameOver() {
        return isDead;
    }

    public void JumpPlayer() {
        buttonJump = true;
    }

    public void RightPlayer() {
        buttonRight = true;
    }

    public void LeftPlayer() {
        buttonLeft = true;
    }

    public void	AddCoin()
    {
    	scoreCoin ++;
    	scoreCoinText.text = scoreCoin.ToString();
    }
}
