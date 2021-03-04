using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerController : MonoBehaviour
{
    [SerializeField]private float speedX = 4f;
    [SerializeField]private float speedY =  1f;
    [SerializeField]private Animator animator; 

    private bool isFinish = false;
    private bool isGround = false;
    private bool isJump = false;
    private bool IsFasingRight = true;
    private bool isLeverArm = false;

    private float horizontal;
    private Rigidbody2D rb;
    private Finish finish;
    private LeverArm leverArm;
    

    const float SpeedMultyplier = 50f;
    
    void Start()
    {
        rb =GetComponent<Rigidbody2D>();
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();   
        leverArm = FindObjectOfType<LeverArm>(); 
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("SpeedX", Mathf.Abs(horizontal));
        if (Input.GetKey(KeyCode.W) && isGround){
            isJump=true;
        }
        if(Input.GetKeyDown(KeyCode.F)){
            if(isFinish){
                finish.FinishLevel();
            }
            if(isLeverArm){
                leverArm.ActivateLever();
            }
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
        if(Input.GetKeyDown(KeyCode.F) && isLeverArm){

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
    private void OnTriggerEnter2D(Collider2D other) {
        LeverArm LeverArmTemp = other.GetComponent<LeverArm>();
        if(other.CompareTag("Finish")){
            isFinish = true;
            Debug.Log(isFinish);
        }
        if(LeverArmTemp != null){
            Debug.Log("isLeverArm стал true");
            isLeverArm = true;
        }
    }
    private void  OnTriggerExit2D(Collider2D other) {
        LeverArm LeverArmTemp = other.GetComponent<LeverArm>();
        if (other.CompareTag("Finish")){
           isFinish = false;
            Debug.Log(isFinish);
        }
        if(LeverArmTemp != null){
             Debug.Log("isLeverArm стал false");
            isLeverArm = false;
        }
    }
}
