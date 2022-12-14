using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject rock;
    public GameObject rockdestroytext;
    public int jumpforce = 1;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;


    // For global timer?

    //private float globalTimer = float.MaxValue;

    //private float RumbleTime = 1;
    

    




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);

        Physics.gravity = new Vector3(0,-30.0f,0);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 84) 
        {
            //winTextObject.SetActive(true);
            
            rockdestroytext.SetActive(true);
            Invoke("hiderocktext", 5.0f);
            rock.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void Update()
    {
        if (Input.GetKeyDown("space") || Input.GetKeyDown("joystick button 0"))
        {
            Jump();
        }
    }

    void hiderocktext()
    {
        rockdestroytext.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }

    void Jump()
    {
        bool Grounded;
        Grounded = Physics.Raycast(transform.position, Vector3.down, 0.71f);
        if (Grounded == true)
        {
            rb.AddForce(new Vector3(0, 800, 0));
        }
    }
}