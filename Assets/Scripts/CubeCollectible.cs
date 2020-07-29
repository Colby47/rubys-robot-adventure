using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollectible : MonoBehaviour
{
    public GameObject healthPrefab;
    public GameObject ammoPrefab;
    public GameObject cubePrefab;

    // Start is called before the first frame update
    void Start()
    {
        RubyController ruby = new RubyController.GetComponent<RubyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void randomCollectible(){
        if(RubyController.health < RubyController.ammo){
            Instantiate(healthPrefab, cubePrefab.transform.position, Quaternion.identity);
        }
        Debug.Log("You have called the randomCollectible function");
    }
}
