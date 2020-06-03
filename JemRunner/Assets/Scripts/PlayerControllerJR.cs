using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControllerJR : MonoBehaviour
{
    private Animator playerAnim;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio; 
    public Button restartButton;
    public Button goToMainMenuButton;
    public GameObject player;
    public GameObject powerupIndicator;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private Rigidbody playerRb; 
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOvertext;
    private Vector3 offset = new Vector3(0, 1, 0);
    private int gemCount = 0;
    public float jumpForce = 10.0f;
    public float speed = 10.0f;
    public float turnSpeed;
    public float horizontalInput;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool hasGem;
    public bool isGameActive;
    public bool hasPowerup;
    public string sceneName; 


    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        UpdateScore(0);
        gameOvertext.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        playerAudio = GetComponent<AudioSource>();
        sceneName = SceneManager.GetActiveScene().name;
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
                playerAudio.PlayOneShot(jumpSound, 1.0f); 
            }
            if(hasPowerup == true)
            {
                powerupIndicator.transform.position = player.transform.position + offset;
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
                playerAudio.PlayOneShot(crashSound, 1.0f); 
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
                hasGem = true;
                Destroy(other.gameObject);
           
        }
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            jumpForce = 16;
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true); 
        }
        if (other.CompareTag("Finish Line"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 

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
        restartButton.gameObject.SetActive(true);
        goToMainMenuButton.gameObject.SetActive(true);
        gameOvertext.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false); 
    }

   public void GoToMainMenu()
    {
        SceneManager.LoadScene("Start"); 
    }

   
   
}
