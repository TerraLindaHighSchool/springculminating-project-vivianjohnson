using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerJR : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private int numLives = 2; 
    public float turnSpeed; 
    public float jumpForce = 10.0f;
    public float horizontalInput; 
    public float speed = 10.0f; 
    public bool isOnGround = true;
    public bool gameOver = false;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); 

        if(!gameOver)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
           numLives--;
            Debug.Log("Oh no you have " + numLives + " lives left!");
            if(numLives == 1)
            {
                Debug.Log("Oh no you have 1 life left! Be careful - get a powerup!");
            }
            if(numLives == 0)
            {
                explosionParticle.Play();
                Debug.Log("Game Over");
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                gameOver = true;
            }
 
        } 
    }
}
