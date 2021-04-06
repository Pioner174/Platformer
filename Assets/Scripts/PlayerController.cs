using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerController : MonoBehaviour
{
    [SerializeField]private float speedX = 4f;
    [SerializeField]private float speedY =  1f;
    [SerializeField]private Animator animator;
    [SerializeField]private Transform playerModelTransform;
    [SerializeField] private AudioSource jumpSound;

    private bool _isFinish = false;
    private bool _isGround = false;
    private bool _isJump = false;
    private bool _IsFasingRight = true;
    private bool _isLeverArm = false;

    private float _horizontal;
    private Rigidbody2D _rb;
    private Finish _finish;
    private LeverArm _leverArm;
    
    

    const float SpeedMultyplier = 50f;
    
    void Start()
    {
        _rb =GetComponent<Rigidbody2D>();
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();   
        _leverArm = FindObjectOfType<LeverArm>(); 
    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("SpeedX", Mathf.Abs(_horizontal));

        if (Input.GetKeyDown(KeyCode.W) && _isGround){
            _isJump=true;
            jumpSound.Play();
        }
        if(Input.GetKeyDown(KeyCode.F)){
            if(_isFinish){
                _finish.FinishLevel();
            }
            if(_isLeverArm){
                _leverArm.ActivateLever();
            }
        }
    }
   
   void FixedUpdate()
   {
        _rb.velocity = new Vector2(_horizontal * speedX * SpeedMultyplier * Time.fixedDeltaTime, _rb.velocity.y);
        if (_isJump){
            _rb.AddForce(new Vector2(0f, 300f * speedY));
            _isGround = false;
            _isJump = false;
        }
        if(_horizontal > 0f && !_IsFasingRight){
            Flip();
        }else if(_horizontal<0 && _IsFasingRight){
            Flip(); 
        }
        if(Input.GetKeyDown(KeyCode.F) && _isLeverArm){

        }
   }

    void Flip(){
        _IsFasingRight  = !_IsFasingRight;
        Vector3 playerscale = playerModelTransform.transform.localScale;
        playerscale.x *= -1;
        playerModelTransform.transform.localScale = playerscale;
    }
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")){
            _isGround = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        LeverArm LeverArmTemp = other.GetComponent<LeverArm>();
        if(other.CompareTag("Finish")){
            _isFinish = true;
            Debug.Log(_isFinish);
        }
        if(LeverArmTemp != null){
            Debug.Log("_isLeverArm стал true");
            _isLeverArm = true;
        }
    }
    private void  OnTriggerExit2D(Collider2D other) {
        LeverArm LeverArmTemp = other.GetComponent<LeverArm>();
        if (other.CompareTag("Finish")){
           _isFinish = false;
            Debug.Log(_isFinish);
        }
        if(LeverArmTemp != null){
             Debug.Log("_isLeverArm стал false");
            _isLeverArm = false;
        }
    }
}
