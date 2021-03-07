using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float timeToWait = 5f;

    private Rigidbody2D _rb;
    private Vector2 _LeftBoundaryPosition;
    private Vector2 _RightBoundaryPosition;

    private void Start(){
        _rb = GetComponent<Rigidbody2D>();
        _LeftBoundaryPosition = transform.position;
        _RightBoundaryPosition = _LeftBoundaryPosition + Vector2.right * walkDistance;
    }

    private void FixedUpdate() {
        _rb.MovePosition((Vector2)transform.position +Vector2.right * walkSpeed *Time.fixedDeltaTime);
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_LeftBoundaryPosition, _RightBoundaryPosition);
    }
}
