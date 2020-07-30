using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{

    Rigidbody2D rigidbody2d;
    //public Text robotText;
    //private int robotCount;

    // Start is called before the first frame update
    /*void Start(){
        count = 0;
        robotText.text = "Robots Fixed: " + robotCount.ToString() + " / 4";
    }*/

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 1000.0f){
            Destroy(gameObject);
        }
    }

    public bool Launch(Vector2 direction, float force){
        rigidbody2d.AddForce(direction * force);
        return true;
    }

    void OnCollisionEnter2D(Collision2D other){
        EnemyController e = other.collider.GetComponent<EnemyController>();
        FastEnemyController f = other.collider.GetComponent<FastEnemyController>();
        ThrowerEnemyScript t = other.collider.GetComponent<ThrowerEnemyScript>();
        RubyController r = other.collider.GetComponent<RubyController>();
        if (e != null)
        {
            e.Fix();
        }

        if(f != null){
            f.Fix();
        }

        if(t != null){
            ThrowerEnemyScript.throwerEnemyHealth -= 1;
            if(ThrowerEnemyScript.throwerEnemyHealth >= 1){
                t.PlaySound();
            }

            if(ThrowerEnemyScript.throwerEnemyHealth <= 0){
            t.Fix();
            }
        }

        if(r != null){
            r.ChangeHealth(-1);
        }
        
        Destroy(gameObject);
    }


}
