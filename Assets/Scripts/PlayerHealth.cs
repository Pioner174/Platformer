﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private AudioSource damageSound;

    private float _health;
    
    private void Start() {
        _health = totalHealth;
        InitHealth();
    }
    
    public void ReduceHealth(float damage){
        _animator.SetTrigger("takeDamage");
        _health -= damage;
        damageSound.Play();
        InitHealth();
        if (_health<=0f)
        Die();
    }
    private void InitHealth(){
        healthSlider.value = _health/totalHealth;
    }
    private void Die(){
        gameObject.SetActive(false);
        gameOverCanvas.SetActive(true);
    }
}
