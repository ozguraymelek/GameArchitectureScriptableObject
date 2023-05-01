using System;
using System.Collections;
using System.Collections.Generic;
using Nacho.Enemy.FINITE_STATE_MACHINE;
using UniRx;
using UnityEngine;

namespace Nacho.Controller.Enemies
{
    public class Warrok : Enemy
    {
        #region Built-in Event Funcs

        private void Awake()
        {
            CurrentState = initialState;
        }
        
        private void Start()
        {
            this.ObserveEveryValueChanged(_ => CurrentState).Subscribe(
                _ =>
                {
                    CurrentState.Onset(this);
                });
        }

        private void FixedUpdate()
        {
            CurrentState.Updating(this);
            print(CurrentState);
        }
        
        private void OnDrawGizmos()
        {
            // if (Application.isEditor == true) return;
            
            CurrentState.OnDrawingGizmosSelected(this);
        }
        
        #endregion
    }
}

