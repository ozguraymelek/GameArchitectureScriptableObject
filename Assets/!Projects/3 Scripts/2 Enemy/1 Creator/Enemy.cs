using System;
using System.Collections;
using System.Collections.Generic;
using Nacho.Enemy.FINITE_STATE_MACHINE;
using UnityEngine;

namespace Nacho.Controller
{
    public abstract class Enemy : MonoBehaviour
    {
        public EnemyBaseState CurrentState { get; set; }
        
        [Header("State Data Holder")] 
        [SerializeField] protected EnemyBaseState initialState;
        
        [Space(20)]
        
        [Header("Objects Holder")]
        public Collider[] suspicionObjects;
        public Collider[] detectedObjects;
        
        [Space(20)]
        
        [Header("Components")] 
        [SerializeField] internal Rigidbody rb;
        [SerializeField] internal CapsuleCollider capsuleCollider;
        [SerializeField] internal Animator animator;
        
        [Space(20)]
        
        [Header("Data")]
        public Point activePoint;
        
        [Space(20)]

        [Header("Settings")] 
        public GameObject questionMark;
        public GameObject exclamationMark;
    }
}