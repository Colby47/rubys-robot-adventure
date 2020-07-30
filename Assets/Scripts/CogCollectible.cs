using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogCollectible : MonoBehaviour
{
    public AudioClip cogCollectedAudio;
    void OnTriggerEnter2D(Collider2D other)
   {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.increaseAmmo();
            controller.setAmmoText();
            Destroy(gameObject);

            controller.PlaySound(cogCollectedAudio);
                        
        }
    }
}
