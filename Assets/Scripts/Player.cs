using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(LineRenderer))]
public class Player : MonoBehaviour
{
    Vector3 startPos; //the position where you first clicked the mouse
    bool haveFirstPosition;
    LineRenderer line;
    Material lineMaterial;
    bool clickedOnPlayer;

    public GameObject FailedText;
    public GameObject RestartPanel;

    //Sound Effects
    AudioSource audioSource;
    public AudioClip CollideSound;
    public AudioClip FlingSound;
    public AudioClip TouchSound;
    public AudioClip DeathSound;
    public AudioClip GameOverSound;
    public AudioClip CollectEffect;

    public float Force;
    float audioTimePlayed;
    Quaternion particleRotation;
    bool isAlive = true;
    bool isFinished = false;

    //Stars
    byte collectedStars;

    // Use this for initialization
    void Start()
    {
        clickedOnPlayer = false;
        haveFirstPosition = false;
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.sortingOrder = 4;
        line.startWidth = 1.5f;
        line.endWidth = .7f;

        audioSource = GetComponent<AudioSource>();

        particleRotation = transform.GetChild(0).rotation;
        
    }

    public void Finished()
    {
        isFinished = true;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        line.positionCount = 0;
    }

    public void Death()
    {
        if (isAlive)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
            audioSource.PlayOneShot(DeathSound);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().simulated = false;
            line.positionCount = 0;
            foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
            {
                ps.gameObject.transform.rotation = particleRotation;
                ps.Play();
            }
            isAlive = false;
            StartCoroutine(GameOver());
        }
    }

    public void CollectStar()
    {
        audioSource.PlayOneShot(CollectEffect);
        collectedStars++;
    }

    public byte GetStars()
    {
        return collectedStars;
    }

    IEnumerator GameOver()
    {
        
        yield return new WaitForSeconds(0.2f);
        audioSource.PlayOneShot(GameOverSound);
        FailedText.transform.DOLocalMoveY(3.73f, 1.6f);
        RestartPanel.transform.DOLocalMoveY(-1f, 2.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            MouseInput();
        }
    }

    void TouchInput()
    {
        Vector3 beganPos;

        // Handle screen touches.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                beganPos = touch.position;
                Debug.Log(beganPos + " beganPos");
            }

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position;
                Debug.Log(pos + " Moved Position");
            }
        }
    }

    void RaycastCheck(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Player"))
        {
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (audioTimePlayed + 0.1f < Time.timeSinceLevelLoad)
        {
            //audioSource.clip = CollideSound;
            //audioSource.time = 0.005f;
            audioSource.PlayOneShot(CollideSound);
            audioTimePlayed = Time.timeSinceLevelLoad;
        }
    }

    void MouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            //Check if we clicked near player //CHANGES 10f used to be 1f
            if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 10f)
            {
                clickedOnPlayer = true;
                Time.timeScale = 0.05f;
                Time.fixedDeltaTime = 0.002f;
                startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            //if we're here, then the mouse button is held down
            if (haveFirstPosition == false && clickedOnPlayer == true)
            {
                //First time we pressed down mouse and clicked on player.
                //startPos = new Vector3(transform.position.x, transform.position.y, 100f);
                //remember we've run this once, so not again until the mouse button is released
                haveFirstPosition = true;
                //enable the line. We'll setup the parameters later
                line.enabled = true;

                //Play sound effects
                audioSource.PlayOneShot(TouchSound, 0.65f);
            }

            //get the mouse position at this point in time. force z value for renderer
            Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100.0f));
            //diff is the direction vector
            Vector3 diff = currentMousePos - transform.position;//startPos;
            diff = Vector2.ClampMagnitude(diff, 1.3f);
            //take that diff and add it to the original click position.
            //vector is now pointing in the exact opposite to diff
            Vector3 lineEndPoint = transform.position + (diff * -1.0f);

            line.SetPosition(0, transform.position);
            line.SetPosition(1, lineEndPoint);
        }
        else if (Input.GetMouseButtonUp(0) && clickedOnPlayer)
        {
            //Reset first click vars
            haveFirstPosition = false;
            clickedOnPlayer = false;

            //disable the line
            line.enabled = false;

            //Speed up time
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;

            //Get Direction
            Vector2 heading = line.GetPosition(1) - transform.position;
            float distance = heading.magnitude;

            //Normalized direction
            Vector2 direction = heading / distance;
            //Debug.Log(direction + " direction");
            //Debug.Log(distance + " distance");

            //Clamp distance
            if (distance > 1f)
            {
                distance = 1f;
            }

            //Play sound effects
            //audioSource.clip = FlingSound;
            //audioSource.time = 0.07f;
            audioSource.PlayOneShot(FlingSound);

            //Launch!
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(direction * (distance * Force));
        }
    }
}
