using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControllerJR : MonoBehaviour
{
    private Animator playerAnim;
    public Button restartButton;
    private Rigidbody playerRb;
    private int gemCount = 0;
    public float jumpForce = 10.0f;
    public float speed = 10.0f;
    public float turnSpeed;
    public float horizontalInput;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool hasGem;
    public bool isGameActive;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOvertext;


    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        UpdateScore(0);
        gameOvertext.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (isGameActive == true && !gameOver)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                dirtParticle.Stop();
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            dirtParticle.Play();
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            {
                explosionParticle.Play();
                dirtParticle.Stop();
                Debug.Log("Game Over");
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                gameOver = true;
                GameOver(); 
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            UpdateScore(1);
            Debug.Log("Great job! You have: " + gemCount + " gems in your bag");
            hasGem = true;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Finish Line"))
        {
            Debug.Log("You made it to the finish line! You beat the game! Now onto the next level");
            GameOver();

        }
    }

    private void UpdateScore(int gemsToAdd)
    {
        gemCount += gemsToAdd;
        scoreText.text = "Gems: " + gemCount;
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOvertext.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
