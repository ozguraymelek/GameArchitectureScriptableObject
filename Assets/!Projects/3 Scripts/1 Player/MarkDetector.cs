using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GenericScriptableArchitecture;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class MarkDetector : MonoBehaviour
{
    [Header("Components /marks")] 
    public GameObject suspectedMark;
    public GameObject revealedMark;
    
    [Space(20)]
    
    [Header("Components /marks")] 
    public Material suspectedMarkMat;
    public Material revealedMarkMat;
    
    [Space(20)]
    
    [Header("Settings /player")]
    public Variable<bool> isSuspected;
    public Variable<bool> isDetected;
    
    [Space(20)]
    
    [Header("Settings /suspected mark")]
    public float scaleSuspectedMarkDelay;
    public float shakeSuspectedDelay;
    public float shakeSuspectedPosStrength;
    public int shakeSuspectedPosVibration;
    
    [Space(20)]
        
    [Header("Settings /revealed mark")]
    public float scaleRevealedMarkDelay;
    public float shakeRevealedDelay;
    public float shakeRevealedPosStrength;
    public int shakeRevealedPosVibration;
    
    [Space(20)]
    
    [Header("Settings /detect")]
    public Variable<float> detectableTimer;

    private void Start()
    {
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

        this.ObserveEveryValueChanged(_ => suspectedMark.activeSelf).Where(_ => suspectedMark.activeSelf == true)
            .Subscribe(unit =>
            {
                DOVirtual.Float(suspectedMarkMat.GetFloat("_Progress_Border"), -1.1f, detectableTimer.Value,
                    v => suspectedMarkMat.SetFloat("_Progress_Border", v));
            });
        
        // this.ObserveEveryValueChanged(_ => suspectedMark.activeSelf).Where(_ => suspectedMark.activeSelf == false)
        //     .Subscribe(unit =>
        //     {
        //         DOVirtual.Float(suspectedMarkMat.GetFloat("_Progress_Border"), 1.1f, .1f,
        //             v => suspectedMarkMat.SetFloat("_Progress_Border", v));
        //     });
    }
    
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
