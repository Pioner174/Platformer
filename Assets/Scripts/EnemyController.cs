using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform enemyModelTransform;
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float chasingSpeed = 3f;
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
    private float _walkSpeed;
    private Vector2 _nextPoint;
    


    public bool IsFacingRight{
        get => _isFacingRight;
    }
    public void StartChasingPlayer(){
        _isChasingPlayer = true;
        _chaseTime = timeToChase;
        _walkSpeed = chasingSpeed;
    }
    private void Start() {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _LeftBoundaryPosition = transform.position;
        _RightBoundaryPosition = _LeftBoundaryPosition + Vector2.right * walkDistance;
        _waitTime = timeToWait;
        _chaseTime = timeToChase;
        _walkSpeed = patrolSpeed;
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
        _nextPoint = Vector2.right * _walkSpeed *Time.fixedDeltaTime;
       if(_isChasingPlayer && Mathf.Abs(DistanceToPlayer()) < minDistance){
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
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }

    private void StartChaseTimer(){
        _chaseTime -= Time.deltaTime;
        if(_chaseTime < 0f){
            _isChasingPlayer = false;
            _chaseTime = timeToChase;
            _walkSpeed = patrolSpeed;
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
        Vector3 playerscale = enemyModelTransform.localScale;
        playerscale.x *= -1;
        enemyModelTransform.localScale = playerscale;
    }
}
