﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollectible : MonoBehaviour
{
    public GameObject healthPrefab;
    public GameObject ammoPrefab;
    public GameObject cubePrefab;

    //public RubyController ruby;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void randomCollectible(){
        if(RubyController.currentHealth < RubyController.ammo){
            Instantiate(healthPrefab, cubePrefab.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else{
            Instantiate(ammoPrefab, cubePrefab.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}
