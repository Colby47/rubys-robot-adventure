using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;
    public int health { get { return currentHealth; }}
    int currentHealth;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    bool isInvincible;
    float invincibleTimer;
    public float timeInvincible = 2.0f;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    public GameObject projectilePrefab;
    public GameObject healthEffect;
    public GameObject damageEffect;

    public int ammo;

    public Text ammoText;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        ammo = 3;
        currentHealth = maxHealth;

        ammoText.text = "Cogs: " + ammo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
                
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            if (ammo > 0){
            Launch();
            ammo--;
            setAmmoText();}
        }

        if (Input.GetKeyDown(KeyCode.X))
            {
                RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC", "Cube"));
                if (hit.collider != null)
                {
                    NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                    if (character != null)
                    {
                        character.DisplayDialog();
                    }

                    CubeCollectible cube = hit.collider.GetComponent<CubeCollectible>();
                    if(cube != null){
                        cube.randomCollectible();
                    }
                }
            }
    }

    void FixedUpdate(){
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition (position);


    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {   
            
            if (isInvincible){
                return;
            }
            Instantiate(damageEffect, rigidbody2d.position, Quaternion.identity);
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        else{
            Instantiate(healthEffect, rigidbody2d.position, Quaternion.identity);
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

        if (currentHealth == 0){
            SceneManager.LoadScene("MainScene");
        }
        
    }

    void Launch(){
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");


        //if (Projectile.isFixed() == true){
        //SetCountText();
        //}

        
    }

    public void increaseAmmo(){
        ammo = ammo + 3;
    }
    public void setAmmoText(){
        ammoText.text = "Cogs: " + ammo.ToString();
    }


}
