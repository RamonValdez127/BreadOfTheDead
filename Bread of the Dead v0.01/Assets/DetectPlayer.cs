using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public int activePlatform;
    private bool atoleActive;
    public GameObject[] platforms;
    private int targetPlat;

    private int[] bucket = {0,0,0,0};
    private bool wait = false;
    int p = 0;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        activePlatform = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(p < 4 && !atoleActive && !wait){
            do{
                targetPlat = Random.Range(0,4);
            }while(bucket[targetPlat] != 0);
            bucket[targetPlat] = 1;
            p += 1;
            platforms[targetPlat].SetActive(true);
            atoleActive = true;
        }

        else if(wait){
            time += Time.deltaTime;
            if(time > 5){
                time = 0;
                wait = false;
            }
        }
    }

    public void toggleWait(){
        platforms[targetPlat].SetActive(false);
        atoleActive = false;
        wait = true;
    }
    
   
}
