using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    public float maxSpeed = 3f;
    public float speed = 50f;
    public float jumpPower = 150f;
    // stats
    public int curHealth;
    public int maxHealth = 5;

    public bool grounded = true;
    public bool canDoubleJump = false;

    private Rigidbody2D rb2d;
    private Animator anim;

    // Use this for initialization
    void Start ()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D> ();
        anim = gameObject.GetComponent<Animator> ();

        curHealth = maxHealth;
    }
	
    // Update is called once per frame
    void Update ()
    {

        // update  Unity Parameters to C# variables
        anim.SetBool ("Grounded", grounded);
//		anim.SetFloat ("Speed", Mathf.Abs (Input.GetAxis ("Horizontal"))); // not the actual speed, but 1
        anim.SetFloat ("Speed", Mathf.Abs (rb2d.velocity.x));


        float AxisH = Input.GetAxis ("Horizontal");

        if (AxisH < -0.1f) {
            transform.localScale = new Vector3 (-1, 1, 1);
        } else if (AxisH > 0.1f) {
            transform.localScale = new Vector3 (1, 1, 1);
        }

        // jumping
        if (Input.GetButtonDown ("Jump")) {
            if (grounded) {
                rb2d.AddForce (Vector2.up * jumpPower);// space pressed
                canDoubleJump = true;
            } else {
                if (canDoubleJump) {
                    canDoubleJump = false;
                    rb2d.velocity = new Vector2 (rb2d.velocity.x, 0); // don't fall
                    rb2d.AddForce (Vector2.up * jumpPower / 1.75f);// space pressed
                }
            }
           
           
//            Debug.Log ("Jump"); // log example 
        }

        if (curHealth > maxHealth) {
            curHealth = maxHealth;
        }

        if (curHealth <= 0) {
            Die ();
        }
       
    }

    // Physics movement
    void FixedUpdate ()
    {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f; // its 2d, not 3d
        easeVelocity.x *= 0.75f;

        // fake frition / easing x speed of play
        if (grounded) {
            rb2d.velocity = easeVelocity;
        }


        float h = Input.GetAxis ("Horizontal"); // xs arrows: 1 or -1

        if (rb2d.velocity.x > maxSpeed) {
            rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y); // limit max speed
        }
        if (rb2d.velocity.x < -maxSpeed) {
            rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y); // limit max speed
        }

        rb2d.AddForce ((Vector2.right * speed) * h);
    }

    void Die ()
    {
        // restart
        Application.LoadLevel (Application.loadedLevel);
    }
}
