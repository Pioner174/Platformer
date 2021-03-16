using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float timeToWait = 5f;
    [SerializeField] private float minDistance  = 1f;
    [SerializeField] private float timeToChase = 3f;

    private Transform _playerTransform;
    private Rigidbody2D _rb;
    private Vector2 _LeftBoundaryPosition;
    private Vector2 _RightBoundaryPosition;
    private bool _isFacingRight = true;
    private bool _isWait = false;
    private bool _isChasingPlayer = false;
    private float _chaseTime;
    private float _waitTime;
    private Vector2 _nextPoint;


    public bool IsFacingRight{
        get => _isFacingRight;
    }
    public void StartChasingPlayer(){
        _isChasingPlayer = true;
        _chaseTime = timeToChase;
    }
    private void Start() {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _LeftBoundaryPosition = transform.position;
        _RightBoundaryPosition = _LeftBoundaryPosition + Vector2.right * walkDistance;
        _waitTime = timeToWait;
        _chaseTime = timeToChase;
    }
    private void Update() {
        if(_isChasingPlayer){
            StartChaseTimer();
        }
        if(_isWait && !_isChasingPlayer){
            StartWaitTimer();
            }
        
        if(ShouldWait()){
            _isWait = true;
        }
    }
    private void FixedUpdate() {
        _nextPoint = Vector2.right * walkSpeed *Time.fixedDeltaTime;
       if(Mathf.Abs(DistanceToPlayer()) < minDistance){
           return;
       }
        if(_isChasingPlayer){
            ChasePlayer();
        }
        if(!_isWait && !_isChasingPlayer){
            Patrol();
        }
    }
    private float DistanceToPlayer(){
        return _playerTransform.position.x - transform.position.x;
    }
    private void Patrol(){
        if(!_isFacingRight){
            _nextPoint *= -1;
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }
    private void ChasePlayer(){
        float distance = DistanceToPlayer(); 
        if(distance < 0 )
            _nextPoint.x *= -1;
        if (distance > 0.2f && !_isFacingRight){
            Flip();
        }else if(distance < 0.2f && _isFacingRight){
            Flip();
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint*3);
    }

    private void StartChaseTimer(){
        _chaseTime -= Time.deltaTime;
        if(_chaseTime < 0f){
            _isChasingPlayer = false;
            _chaseTime = timeToChase;
        }
    }
    private void StartWaitTimer(){
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
