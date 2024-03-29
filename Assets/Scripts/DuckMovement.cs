using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class DuckMovement : MonoBehaviour
{
    private Animator a_Animator;

    public GameObject Player;
    public Rigidbody rb;
    public float sideSpeed = 1f;
    private float currentX;
    private bool rechts;
    public static bool win;

    public Text WinMessage;
    public Text RestartWin;
    public AudioSource Swim, Munch, Fishy;

    void Start()
    {
        float currentX = this.transform.position.x;
        a_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Score.ded == false && win == false)
        {
            float currentX = this.transform.position.x;
            if (Input.GetKey("space"))
            {
                
                this.transform.Translate(sideSpeed, 0, 0, Space.World);
                //probably a braindead way of doing this, checks wether the duck is about to collide with the shore by comparing its x coordinate with a preset value (cf spawner)
                //you could probably use a collision system and rb.addforce instead but that made changing directions a nightmare so fuck it
           
                if (currentX > 4.5 && rechts == false)
                {
                    sideSpeed = sideSpeed * (-1);
                    rechts = true;
                }
                else if (currentX < -4.5 && rechts == true)
                {
                    sideSpeed = sideSpeed * (-1);
                    rechts = false;
                }
                
            }

            if (Score.scorenumber >= 5)
            {
                win = true;
                WinMessage.text = "You Won";
                RestartWin.text = "Press R to Play Again";
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "food")
        {
            Munch.Play();
            Score.scorenumber++;
            a_Animator.SetTrigger("Eat");

        }
    }
}
