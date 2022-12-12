using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyCollition : MonoBehaviour
{
    public PlayerHealth Enemy;

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Weapon"){
            //Aqui va alguna animacion
            Enemy.TakeDamage(10);
            Debug.Log("Damage Enemy:");
            Debug.Log(Enemy.health);
            /*if(MiniEnemy.health <= 0){
                MiniEnemy.health = Mathf.Clamp(MiniEnemy.health,0,MiniEnemy.maxhealth);
                MiniEnemy.UpdateHealthUI();
                Enemy.SetActive (false);
            }*/
        }
    }
}
