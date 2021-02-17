using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float speedX = -1f;
    [SerializeField]private float speedY =  0f;
    private Rigidbody2D rb;

    const float SpeedMultyplier = 50f;
    
    void Start()
    {
     rb =GetComponent<Rigidbody2D>();
     rb.velocity = new Vector2(1f, 2f);    
    }

    void Update()
    {
        
    }
   
   void FixedUpdate()
   {
       
       rb.velocity = new Vector2(speedX * SpeedMultyplier * Time.fixedDeltaTime, rb.velocity.y);
   }
}
