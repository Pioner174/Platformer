using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float timeToWait = 5f;

    private Transform _playerTransform;
    private Rigidbody2D _rb;
    private Vector2 _LeftBoundaryPosition;
    private Vector2 _RightBoundaryPosition;
    private bool _isFacingRight = true;
    private bool _isWait = false;
    private bool _isChasingPlayer = false;
    private float _waitTime;
    private Vector2 _nextPoint;

    public bool IsFacingRight{
        get => _isFacingRight;
    }
    public void StartChasingPlayer(){
        _isChasingPlayer = true;
    }
    private void Start() {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _LeftBoundaryPosition = transform.position;
        _RightBoundaryPosition = _LeftBoundaryPosition + Vector2.right * walkDistance;
        _waitTime = timeToWait;
    }
    private void Update() {
        if(_isWait){
            Wait();
            }
        

        
        if(ShouldWait()){
            _isWait = true;
        }
    }
    private void FixedUpdate() {
        _nextPoint = Vector2.right * walkSpeed *Time.fixedDeltaTime;
       
        if(_isChasingPlayer){
            ChasePlayer();
        }
        if(!_isWait && !_isChasingPlayer){
            Patrol();
        }
    }

    private void Patrol(){
        if(!_isFacingRight){
            _nextPoint *= -1;
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }
    private void ChasePlayer(){
        float distance = _playerTransform.position.x - transform.position.x; 
        if(distance < 0 )
            _nextPoint.x *= -1;
        if (distance > 0f && !_isFacingRight){
            Flip();
        }else if(distance < 0f && _isFacingRight){
            Flip();
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }

    private void Wait(){
        _waitTime -= Time.deltaTime;
            if(_waitTime <0f){
                _waitTime = timeToWait;
                _isWait = false;
                Flip();
            }
    }
    private bool ShouldWait(){
        bool isOutOfRightBoundary = _isFacingRight && transform.position.x >= _RightBoundaryPosition.x;
        bool isOutOfLeftBoundary = !_isFacingRight && transform.position.x <= _LeftBoundaryPosition.x;
        return isOutOfRightBoundary || isOutOfLeftBoundary;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_LeftBoundaryPosition, _RightBoundaryPosition);
    }
    void Flip(){
        _isFacingRight  = !_isFacingRight;
        Vector3 playerscale = transform.localScale;
        playerscale.x *= -1;
        transform.localScale = playerscale;
    }
}
