﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float timeToWait = 5f;

    private Rigidbody2D rb;
    private Vector2 LeftBoundaryPosition;
    private Vector2 RightBoundaryPosition;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        LeftBoundaryPosition = transform.position;
        RightBoundaryPosition = LeftBoundaryPosition + Vector2.right * walkDistance;
    }

    private void FixedUpdate() {
        rb.MovePosition((Vector2)transform.position +Vector2.right * walkSpeed *Time.fixedDeltaTime);
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(LeftBoundaryPosition, RightBoundaryPosition);
    }
}
