using System;
using GenericScriptableArchitecture;
using Nacho.FINITE_STATE_MACHINE;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nacho._Projects.SCRIPTS.Player.Input
{
    public class InputListener : MonoBehaviour
    {
        private PlayerInput _input;

        #region Input Variables
        [Space(20)]
        [Header("INPUT VARIABLES")]
        [Header("Settings /movement input")] 
        public Variable<Vector2> currentMovementInput;
        public Vector3 currentMovement;
        
        [Space(15)]
        
        [Header("Settings /movement applied")]
        public Variable<float> appliedMovementX;
        public Variable<float> appliedMovementY;
        public Variable<float> appliedMovementZ;
        
        [Space(25)]
        
        [Header("Settings /roll input")]
        public Variable<Vector2> currentRollInput;
        public Vector3 currentRoll;
        
        [Space(15)]
        
        [Header("Settings /roll applied")]
        public Variable<float> appliedRollX;
        public Variable<float> appliedRollZ;
        
        #endregion
        
        #region Input Info

        [Space(40)]
        [Header("INPUT INFO")]
        [Header("Settings /movement")] 
        public Variable<bool> movementButtonPressed;
    
        [Space(25)]
        
        [Header("Settings /roll")] 
        public Variable<bool> rollButtonPressed;
        
        [Space(25)]
        
        [Header("Settings /attack")]
        public Variable<bool> basicAttackButtonPressed;
        
        

        #endregion

        #region Built-in Funcs

        private void Awake()
        {
            _input = new PlayerInput();
            
            AddListener();
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        #endregion

        #region Priv Funcs

        private void AddListener()
        {
            _input.Player.Move.started += MoveInputCallback;
            _input.Player.Move.performed += MoveInputCallback;
            _input.Player.Move.canceled += MoveInputCallback;

            _input.Player.BasicAttack.started += BasicAttackInputCallback;
            _input.Player.BasicAttack.performed += BasicAttackInputCallback;
            _input.Player.BasicAttack.canceled += BasicAttackInputCallback;
        }

        #endregion

        #region Listeners

        private void MoveInputCallback(InputAction.CallbackContext ctx)
        {
            currentMovementInput.Value = ctx.ReadValue<Vector2>();
        }

        private void BasicAttackInputCallback(InputAction.CallbackContext ctx)
        {
            basicAttackButtonPressed.Value = ctx.ReadValueAsButton();
        }

        #endregion
        
    }
}