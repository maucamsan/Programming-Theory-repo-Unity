using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Core.Scriptables;
namespace Core.Player
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject playerReference;
        public static event Action<GameObject> PlayerToFollow;
        private PlayerInput _playerInput;
    
        private InputAction _moveAction;

        private FloatReference _floatReference;

        
        private void Awake() 
        {
            _floatReference = GetComponentInChildren<FloatReference>();
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions["Move"];
        }

        void Start()
        {
            PlayerToFollow?.Invoke(playerReference);  
        }
        
        void Update()
        {
            Vector2 movementInput = _moveAction.ReadValue<Vector2>();  
            transform.Translate(new Vector2(movementInput.x * _floatReference.floatVariableDict[((int)Variables.SPEED)].Value * Time.deltaTime, movementInput.y *
                                 _floatReference.floatVariableDict[((int)Variables.SPEED)].Value * Time.deltaTime));  
        }

        
    }
}
