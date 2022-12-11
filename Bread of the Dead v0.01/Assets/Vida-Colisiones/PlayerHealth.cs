using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    private float learpTimer;
    public float maxhealth = 100;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health,0,maxhealth);
        UpdateHealthUI();
    }

    public void UpdateHealthUI(){
        Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health/maxhealth;

        if(fillB > hFraction){
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            learpTimer += Time.deltaTime;
            float percentComplete = learpTimer/chipSpeed;
            percentComplete = percentComplete*percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB,hFraction,percentComplete);
        }
        if(fillF < hFraction){
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            learpTimer += Time.deltaTime;
            float percentComplete = learpTimer / chipSpeed;
            percentComplete = percentComplete*percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF,backHealthBar.fillAmount,percentComplete);
        }
    }

    public void TakeDamage(float damage){
        health -= damage;
        learpTimer = 0f;
    }

    public void RestoreHealth(float healAmount){
        health += healAmount;
        learpTimer = 0f;
    }
}
