using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float totalHealth = 100f;


    private Animator _animator;
    private float _health;
    private void Start() {
        _health = totalHealth;
        _animator = GetComponent<Animator>();
        InitHealth();
    }

    public void ReduceHealth(float damage){
        _animator.SetTrigger("takeDamage");
        _health -= damage;
        InitHealth();
        if (_health<=0f)
        Die();
    }
    private void InitHealth(){
        healthSlider.value = _health/totalHealth;
    }
    private void Die(){
        gameObject.SetActive(false);
    }
}
