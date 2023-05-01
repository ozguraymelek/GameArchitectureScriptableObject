using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
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
        
        [Header("Settings /suspected mark")]
        public float scaleSuspectedMarkDelay;
        public Vector3 rotateSuspectedMarkEndValue;
        public float rotateSuspectedMarkDelay;
        public float shakeSuspectedDelay;
        public float shakeSuspectedPosStrength;
        public int shakeSuspectedPosVibration;
        
        [Header("Settings /revealed mark")]
        public float scaleRevealedMarkDelay;
        public Vector3 rotateRevealedMarkEndValue;
        public float rotateRevealedMarkDelay;
        public float shakeRevealedDelay;
        public float shakeRevealedPosStrength;
        public int shakeRevealedPosVibration;
        
        [Header("Settings /marks")] 
        public GameObject suspectedMark;
        public GameObject revealedMark;
        
        [Header("Settings /player")]
        public PlayerDirection playerCurrentDirection;

        public Variable<bool> isSuspected;
        public Variable<bool> isDetected;

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

            
            this.ObserveEveryValueChanged(_ => isSuspected.Value).Subscribe(unit =>
            {
                if (isSuspected.Value == true)
                    ScalingSuspectedMark();
                else
                    UnscalingSuspectedMark();
            });
            
            this.ObserveEveryValueChanged(_ => isDetected.Value).Subscribe(unit =>
            {
                if (isDetected.Value == true)
                    ScalingRevealedMark();
                else
                    UnscalingRevealedMark();
            });
        }

        private void FixedUpdate()
        {
            CurrentState.Updating(this);
            CurrentAttackState.Updating(this);
        }

        #endregion

        #region Priv Funcs

        #region Suspected Mark

        private void ScalingSuspectedMark()
        {
            var seq = DOTween.Sequence();
            
            suspectedMark.SetActive(true);

            seq.Append(suspectedMark.transform.DOScale(new Vector3(1f, 1f, 1f), scaleSuspectedMarkDelay));
            
            seq.Append(
                    suspectedMark.transform.DOShakePosition(shakeSuspectedDelay, shakeSuspectedPosStrength, 
                        shakeSuspectedPosVibration))
                .SetLoops(-1);
        }
        
        private void UnscalingSuspectedMark()
        {
            var seq = DOTween.Sequence();

            seq.Append(suspectedMark.transform.DOScale(new Vector3(0f, 0f, 0f), scaleSuspectedMarkDelay * 15f));
            
            seq.AppendCallback(() =>
            {
                suspectedMark.SetActive(false);
            });
        }

        #endregion

        #region Revealed Mark

        private void ScalingRevealedMark()
        {
            var seq = DOTween.Sequence();
            
            revealedMark.SetActive(true);

            seq.Append(revealedMark.transform.DOScale(new Vector3(1f, 1f, 1f), scaleRevealedMarkDelay));
            
            seq.Append(
                    revealedMark.transform.DOShakePosition(shakeRevealedDelay, shakeRevealedPosStrength, 
                        shakeRevealedPosVibration))
                .SetLoops(-1);
        }
        
        private void UnscalingRevealedMark()
        {
            var seq = DOTween.Sequence();

            seq.Append(revealedMark.transform.DOScale(new Vector3(0f, 0f, 0f), scaleRevealedMarkDelay * 15f));
            
            seq.AppendCallback(() =>
            {
                revealedMark.SetActive(false);
            });
        }

        #endregion
        

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