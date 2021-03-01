using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float speedX = 4f;
    [SerializeField]private float speedY =  1f;
    [SerializeField]private Animator animator; 

    private bool isGround = false;
    private bool isJump = false;
    private float horizontal;
    private Rigidbody2D rb;

    private bool IsFasingRight = true;

    const float SpeedMultyplier = 50f;
    
    void Start()
    {
     rb =GetComponent<Rigidbody2D>();
     rb.velocity = new Vector2(1f, 2f);    
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("SpeedX", Mathf.Abs(horizontal));
        if (Input.GetKey(KeyCode.W) && isGround){
            isJump=true;
        }
    }
   
   void FixedUpdate()
   {
        rb.velocity = new Vector2(horizontal * speedX * SpeedMultyplier * Time.fixedDeltaTime, rb.velocity.y);
        if (isJump){
            rb.AddForce(new Vector2(0f, 300f * speedY));
            isGround = false;
            isJump = false;
        }
        if(horizontal > 0f && !IsFasingRight){
            Flip();
        }else if(horizontal<0 && IsFasingRight){
            Flip(); 
        }
        
   }

    void Flip(){
        IsFasingRight  = !IsFasingRight;
        Vector3 playerscale = transform.localScale;
        playerscale.x *= -1;
        transform.localScale = playerscale;
    }
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")){
            isGround = true;
        }
    }

}
