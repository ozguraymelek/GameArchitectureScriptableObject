using System;
using System.Collections;
using System.Collections.Generic;
using GenericScriptableArchitecture;
using Nacho.Enemy.FINITE_STATE_MACHINE;
using UnityEngine;

namespace Nacho.Controller
{
    public abstract class Enemy : MonoBehaviour
    {
        public EnemyBaseState CurrentState { get; set; }
        
        [Header("State Data Holder")] 
        [SerializeField] protected EnemyBaseState initialState;

        [Space(20)] [Header("Objects Holder")] 
        public Collider[] suspicionObjects = new Collider[1];
        public Collider[] detectedObjects;
        public Collider[] attackableObjects;
        
        [Space(20)]
        
        [Header("Components")] 
        [SerializeField] internal Rigidbody rb;
        [SerializeField] internal CapsuleCollider capsuleCollider;
        [SerializeField] internal Animator animator;
        
        [Space(20)]
        
        [Header("Data /ref")]
        public Point activePoint;
        public Player activePlayer;
        
        [Space(20)]

        [Header("Settings /marks")] 
        public GameObject questionMark;
        public GameObject exclamationMark;
    }
}