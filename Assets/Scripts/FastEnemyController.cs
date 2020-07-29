﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastEnemyController : MonoBehaviour
{
    public float speed;
    private bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody2D;
    Animator animator;
    float timer;
    int direction = 1;

    bool broken = true;

    public ParticleSystem smokeEffect;





    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        GameObject.Find("Robots Fixed").GetComponent<Text>().text = "Bots Fixed: " + EnemyController.fixedBots + " / 4"; 

    }

    void Update()
    {
        if(!broken){
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
            randomNumber();
        }
    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        
        if(!broken){
            return;
        }

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else 
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-2);
        }
    }

    public void Fix(){
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        EnemyController.fixedBots ++;
        GameObject.Find("Robots Fixed").GetComponent<Text>().text = "Bots Fixed: " + EnemyController.fixedBots + " / 4";
        // Get the game object you want to change
        // Get the component on that object
        // Modify the componen script

        // Track the value in one place. A static variable. 
        
    }

    void randomNumber(){
        int randomNumber = Random.Range(1,3);
        if(randomNumber == 1){
            vertical = true;
        }
        else{vertical = false;}
    }


}
