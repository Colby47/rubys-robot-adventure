using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
   {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.increaseAmmo();
            controller.setAmmoText();
            Destroy(gameObject);
            
        }
    }
}
