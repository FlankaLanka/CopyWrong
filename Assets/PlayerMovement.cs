using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool shieldActivated = false;

    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip gemPickupSound;
    public AudioClip bushSound;
    public GameObject cherry;
    public GameObject PanelLose;
    public GameObject PanelWin;
    public Text newScoreText;
    public float maxTime;
    public float spawnRate;
    public float movementSpeed;
    public float jumpHeight;
    public Text timerText;
    public GameObject enemyPrefab;
    public List<Vector2> movements = new List<Vector2>();
    public List<Vector2> directionsFacing = new List<Vector2>();
    public LayerMask groundlayer;
    public List<GameObject> invisFloors;

    private bool spawning = false;
    private bool isGrounded = false;
    private bool collectedGem = false;
    private bool pauseTime = false;
    private Rigidbody2D rb;
    private Vector2 currentPos;

    //private int i = 0;


    // Start is called before the first frame update
    void Start()
    {
        timerText.text = maxTime.ToString();
        currentPos = GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody2D>();        
    }


    // Update is called once per frame

    void Update()
    {
        if(Time.timeScale != 0f)
        {
            GetInput();
        }

        if (!pauseTime)// && firstInputRecieved)
        {
            if (maxTime <= 0)
            {
                pauseTime = true;
                timerText.text = "0.00";
                LoseLevel();
            }
            else
            {
                if (Time.timeScale != 0f)
                {
                    maxTime -= Time.deltaTime;
                }
                double displayTime = System.Math.Round(maxTime, 2);
                timerText.text = displayTime.ToString();

            }
        }

        isGrounded = Physics2D.Raycast(transform.position - new Vector3(0.03f, 0, 0), Vector2.down, 0.09f, groundlayer)
                     || Physics2D.Raycast(transform.position + new Vector3(0.02f, 0, 0), Vector2.down, 0.09f, groundlayer);
        
        //Debug.DrawLine(transform.position - new Vector3(0.03f, 0, 0), transform.position - new Vector3(0.03f, 0, 0) + Vector3.down * 0.09f, Color.red);
        //Debug.DrawLine(transform.position + new Vector3(0.02f, 0, 0), transform.position + new Vector3(0.02f, 0, 0) + Vector3.down * 0.09f, Color.red);
        
    }

    private void GetInput()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, rb.velocity.y);

        //simple rotation for player
        if(rb.velocity.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        else if(rb.velocity.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            isGrounded = false;
        }
        

        if (!collectedGem)
        {
            movements.Add(rb.transform.position);
            directionsFacing.Add(transform.eulerAngles);
        }

        if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)
            || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }


    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Spike"))
        {
            LoseLevel();
        }

        if(collision.gameObject.CompareTag("Enemy"))// && !shieldActivated)
        {
           
            if(shieldActivated)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                AudioSource.PlayClipAtPoint(bushSound, Camera.main.transform.position, 0.5f);
                //Destroy(collision.gameObject);
            }
            else
            {
                LoseLevel();
            }
        }

        if (collision.gameObject.CompareTag("Gem"))
        {
            if(!collectedGem)
            {
                AudioSource.PlayClipAtPoint(gemPickupSound, Camera.main.transform.position, 0.25f);
                pauseTime = true;
                collectedGem = true;
                spawning = true;
                StartCoroutine(EnemySpawner());
                collision.gameObject.SetActive(false);
                cherry.SetActive(true);

                //change floors if possible
                for(int i = 0; i < invisFloors.Count; i++)
                {
                    invisFloors[i].GetComponent<BoxCollider2D>().enabled = true;
                    invisFloors[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    invisFloors[i].tag = "Floor";
                    invisFloors[i].layer = LayerMask.NameToLayer("Floor");
                }
            }
        }

        if (collision.gameObject.CompareTag("Cherry"))
        {
            WinLevel();
            collision.gameObject.SetActive(false);
        }
    }

    private IEnumerator EnemySpawner()
    {
        while(spawning)
        {
            Instantiate(enemyPrefab, currentPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
    }


    private void WinLevel()
    {
        AudioSource.PlayClipAtPoint(winSound, Camera.main.transform.position, 0.1f);
        spawning = false;
        gameObject.SetActive(false);
        PanelWin.SetActive(true);

        string currentLevelNum = "HighScore" + SceneManager.GetActiveScene().buildIndex.ToString();

        if(maxTime > PlayerPrefs.GetFloat(currentLevelNum, 0))
        {
            double displayTime = maxTime;
            displayTime = System.Math.Round(maxTime, 2);
            newScoreText.text = "New High Score: " + displayTime;
            PlayerPrefs.SetFloat(currentLevelNum, (float)displayTime);
        }
        else
        {
            newScoreText.text = "High Score: " + PlayerPrefs.GetFloat(currentLevelNum, 0).ToString();
        }
    }

    private void LoseLevel()
    {
        AudioSource.PlayClipAtPoint(loseSound, Camera.main.transform.position, 0.1f);
        spawning = false;
        gameObject.SetActive(false);
        PanelLose.SetActive(true);
    }
}
