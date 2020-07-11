using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    private int count;
    private bool endUI;
    public float speed;
    public Text countText;
    public Text winText;
    public Text timeText;
    private float dragDistance;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    public float timeLeft;
    public int difficulty;
    private float extraTime;
    public GameObject dif;
    private void Start()
    {
        dif= GameObject.Find("DifficultyObj");
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        endUI = false;
        dragDistance = Screen.height * 15 / 100;

        difficulty = Settings.GetDifficulty();
        if (difficulty == 1)//Easy
        {
            timeLeft = 30f;
            extraTime = 3f;
            SetTimeText();
        }
        else if (difficulty == 2)//normal
        {
            timeLeft = 25f;
            extraTime = 3f;
            SetTimeText();
        }
        else if (difficulty == 3)//hard
        {
            timeLeft = 20f;
            extraTime = 2f;
            SetTimeText();
        }
        
       
    }
    private void Update()
    {
        timeLeft -= Time.deltaTime;
        SetTimeText();
        if (timeLeft < 0)
        {
            SceneManager.LoadScene("LoseMenu");
        }
        if (endUI)
        {
            StartCoroutine(WaitFinish());
            SceneManager.LoadScene("WinMenu");
            
        }  
    }

    private IEnumerator WaitFinish()
    {
        yield return new WaitForSeconds(5);
    }

    private void FixedUpdate()
    {

        float moveHorizontal = 0;
        float moveVertical = 0;
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            moveHorizontal = 15;
                        }
                        else
                        {   //Left swipe
                            moveHorizontal = -15;
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            moveVertical = 15;
                        }
                        else
                        {   //Down swipe
                            moveVertical = -15;
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
          
        }
        if (Input.GetKeyDown("d"))  //If the movement was to the right)
        {   //Right swipe
                moveHorizontal = 15;
        }
        if (Input.GetKeyDown("a"))
        {   //Left swipe
                moveHorizontal = -15;
        }
        if (Input.GetKeyDown("w"))  //If the movement was up
        {   //Up swipe
                moveVertical = 15;
        }
        if (Input.GetKeyDown("s"))
        {   //Down swipe
                moveVertical = -15;
        }
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            timeLeft += extraTime;
            if (count == 12)
            {
                winText.text = "You Win!";
                endUI = true;
            }
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void SetTimeText()
    {
        string timetxt = string.Format("Time Left: {0:n0}s", timeLeft);
        timeText.text = timetxt;
    }
}
