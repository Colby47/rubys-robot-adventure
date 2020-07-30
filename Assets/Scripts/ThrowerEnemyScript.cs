using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowerEnemyScript : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody2D;
    Animator animator;
    float timer;
    int direction = 1;

    bool broken = true;

    public ParticleSystem smokeEffect;

    AudioSource audioSource;

    public AudioClip fixRobotAudio;

    public GameObject projectilePrefab;

    Vector2 lookDirection = new Vector2(1,0);

    public static int throwerEnemyHealth = 2;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(!broken){
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Launch();
            direction = -direction;
            timer = changeTime;
            
            
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
            lookDirection.Set(0, direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            lookDirection.Set(direction,0);
        }
        
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix(){
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        EnemyController.fixedBots ++;
        GameObject.Find("Robots Fixed").GetComponent<Text>().text = "Bots Fixed: " + EnemyController.fixedBots + " / 4";

        GameObject.Find("Ruby").GetComponent<RubyController>().PlaySound(fixRobotAudio);

        Destroy(audioSource);
    }
    void Launch(){
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);



    }


}
