using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public GameObject dialogBoxFinished;
    float timerDisplay;
    float timerChangeLevel;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
        timerChangeLevel = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
                dialogBoxFinished.SetActive(false);
            }
        }

        if(timerChangeLevel >= 0){
            timerChangeLevel -= Time.deltaTime;
            if(timerChangeLevel < 0){
                SceneManager.LoadScene("SecondaryScene");
                EnemyController.fixedBots = 0;
            }
        }


        
    }
    public void DisplayDialog()
    {
        if(EnemyController.fixedBots != 4){
            timerDisplay = displayTime;
            //Debug.Log(EnemyController.botsFixed);
            dialogBox.SetActive(true);
        }

        else {
            timerDisplay = displayTime;
            timerChangeLevel = displayTime;

            dialogBoxFinished.SetActive(true);

        }
    }
}
