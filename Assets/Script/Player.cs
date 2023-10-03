using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpAmount = 1;

    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundlayer;
    public float overlap = 0.5f;

    public Animator anim;

    public int points;
    public int food;

    public TMP_Text scoreDispaly;

    public Image bar;
    public int maxFood = 2;
    public float fillAmount = 0.5f;

    public AudioSource jumpSound;
    public AudioSource blasterSound;
    public AudioSource UltimateSound;
    public int maxPoints = 10;

    public Transform FirePoint;
    public GameObject bulletPrefab;

    public GameObject UltimateBullet;

    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody2D>();
        scoreDispaly.text = "Score: " + points.ToString();
        bar.fillAmount = 0f; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
            }
        }

        if (isGrounded)
        {
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Jump", true);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if(groundCheck.position.y < -10)
        {
            SceneManager.LoadScene("GameOver");
        }
        
        

        FoodBar();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, overlap, groundlayer);
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        jumpSound.Play();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Point"))
        {
            points++;
            Debug.Log("points:" + points);
            scoreDispaly.text = "Score: " + points.ToString();

            if (points == 10)
            {
                SceneManager.LoadScene("WinScreen");
            }
        }

        if (other.CompareTag("Food"))
        {
            food++;
            bar.fillAmount += fillAmount;
            Debug.Log("FOOD HIT");

        }


    }



    void FoodBar()
    {

        if (food >= maxFood && Input.GetKeyDown(KeyCode.Q))
        {
            bar.fillAmount = 0f;
            food = 0;

            Instantiate(UltimateBullet, FirePoint.position, FirePoint.rotation);
            
            ScreenShakeController.instance.StartShake(5f, 1f);
            UltimateSound.Play();

        }
    }



    void Shoot ()
    {
        Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        blasterSound.Play();


    }

    
}
