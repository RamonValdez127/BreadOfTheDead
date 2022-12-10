using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollition : MonoBehaviour
{
    public WeponController wp;
    public PlayerHealth MiniEnemy;
    public GameObject Enemy;

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Enemy" && wp.IsAttacking){
            //Aqui va alguna animacion
            MiniEnemy.TakeDamage(10);
            if(MiniEnemy.health <= 0){
                MiniEnemy.health = Mathf.Clamp(MiniEnemy.health,0,MiniEnemy.maxhealth);
                MiniEnemy.UpdateHealthUI();
                Enemy.SetActive (false);
            }
        }
    }
}
