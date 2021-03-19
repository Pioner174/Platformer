using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   [SerializeField] private float health = 100f;
    [SerializeField] private Animator _animator;

   

    public void ReduceHealth(float damage){
        _animator.SetTrigger("takeDamage");
        health -= damage;

        if (health<=0f)
        Die();
    }

    private void Die(){
        gameObject.SetActive(false);
    }
}
