using System;
using System.Collections;
using System.Collections.Generic;
using Nacho.Enemy.FINITE_STATE_MACHINE;
using UnityEngine;

namespace Nacho.Controller
{
    public abstract class Enemy : MonoBehaviour
    {
        [Header("State Data Holder")] 
        [SerializeField] protected EnemyBaseState initialState;
        
        public EnemyBaseState CurrentState { get; set; }

        [Header("Components")] 
        [SerializeField] internal Rigidbody rb;
        [SerializeField] internal CapsuleCollider capsuleCollider;
        [SerializeField] internal Animator animator;
        
        [Header("Data")]
        public Waypoint activePoint;
    }
}