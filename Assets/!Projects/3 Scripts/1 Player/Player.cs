using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using GenericScriptableArchitecture;
using Nacho.FINITE_ATTACK_MACHINE;
using Nacho.FINITE_STATE_MACHINE;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Nacho.Controller
{
    public class Player : MonoBehaviour
    {
        [Header("State Data Holder")] 
        [SerializeField] private PlayerBaseState initialState;
        [SerializeField] private PlayerBaseAttackState initialAttackState;
        
        public PlayerBaseState CurrentState { get; set; }
        public PlayerBaseAttackState CurrentAttackState { get; set; }
        
        [Header("Components")] 
        [SerializeField] internal Transform weaponTr;
        [SerializeField] internal Animator animator;
        [SerializeField] internal Rigidbody rb;
        [SerializeField] internal CinemachineVirtualCamera playerVirtualCamera;

        public GladiusVFX activeVFX;
        
        [Header("Settings /marks")] 
        public GameObject suspectedMark;
        public GameObject revealedMark;
        
        [Header("Settings /player")]
        public PlayerDirection playerCurrentDirection;

        #region Built-in Event Funcs

        private void Awake()
        {
            CurrentState = initialState;
            CurrentAttackState = initialAttackState;
        }

        private void Start()
        {
            this.ObserveEveryValueChanged(_ => CurrentState).Subscribe(
                _ =>
                {
                    CurrentState.Onset(this);
                });
            
            this.ObserveEveryValueChanged(_ => CurrentAttackState).Subscribe(
                _ =>
                {
                    CurrentAttackState.Onset(this);
                });
        }

        private void FixedUpdate()
        {
            CurrentState.Updating(this);
            CurrentAttackState.Updating(this);
        }

        #endregion
    }

    public enum PlayerDirection
    {
        Idle,
        ForwardWalk,
        BackwardWalk,
        LeftWalk,
        RightWalk,
        BackwardLeftWalk,
        BackwardRightWalk,
        
        ForwardRun,
        RightRun,
        LeftRun
    }
}