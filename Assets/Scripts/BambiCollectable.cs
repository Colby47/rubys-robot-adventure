using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambiCollectable : MonoBehaviour
{
    public AudioClip bambiCollectedAudio;
    void OnTriggerEnter2D(Collider2D other)
   {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            Destroy(gameObject);

            controller.PlaySound(bambiCollectedAudio);

            NonPlayerCharacter.hasFrog = true;
                        
        }
    }
}
