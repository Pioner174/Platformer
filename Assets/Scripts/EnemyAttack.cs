using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 5f;
    private void OnCollisionEnter2D(Collision2D other) {
       PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
       if(playerHealth != null){
           playerHealth.ReduceHealth(damage);
       }
   }
}
