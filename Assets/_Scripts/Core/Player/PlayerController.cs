using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Core.Scriptables;
using Core.Enemy;
namespace Core.Player
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject playerReference;
        public static event Action<GameObject> PlayerToFollow;
        private PlayerInput _playerInput;
        private CharacterStats _characterStats;
        private InputAction _moveAction;
        private InputAction _attitudeAction;
        private FloatReference _floatReference;

        UnityEngine.Gyroscope gyroscope;

        public AttackDefinition BaseAttack;

        // public void AttackTarget (GameObject target)
        // {
        //     var attack = BaseAttack.CreateAttack(_characterStats, target.GetComponent<CharacterStats>()); // stats, target getcomponent characterstats
        //     var attackables = target.GetComponentsInChildren( typeof ( IAttackable ));

        //     foreach (IAttackable attackable in attackables)
        //     {
        //         attackable.OnAttack(gameObject, attack);
        //     }
        // }
        // Character stats stats
        
        private void Awake() 
        {
            // Get stats
            _floatReference = GetComponentInChildren<FloatReference>();
            _playerInput = GetComponent<PlayerInput>();
            _characterStats = GetComponentInChildren<CharacterStats>();
            _moveAction = _playerInput.actions["Move"];
            _attitudeAction = _playerInput.actions["AttitudeMovement"];
            // currPlayerRef = gameObject;
            
        }
        // private static GameObject currPlayerRef;
        // public static GameObject PlayersReference()
        // {
        //     return currPlayerRef;
        // }

        void Start()
        {
            PlayerToFollow?.Invoke(playerReference);  
            gyroscope = Input.gyro;
            Enemy.Enemy.OnPlayerReferenceNeeded += PlayerReference;
        }
        private GameObject PlayerReference()
        {
            return playerReference;
        }
        void Update()
        {
            Vector2 movementInput = _moveAction.ReadValue<Vector2>();  
            transform.Translate(new Vector2(movementInput.x * _floatReference.floatVariableDict[((int)CharacterStatsVariables.SPEED)].Value * Time.deltaTime, movementInput.y *  _floatReference.floatVariableDict[((int)CharacterStatsVariables.SPEED)].Value * Time.deltaTime));  

            // InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
            #if PLATFORM_ANDROID
                gyroscope.enabled = true;
                
                Quaternion moveInput = gyroscope.attitude;
                Vector3 worldDirection = moveInput * Vector3.forward;
                transform.Translate(new Vector2 (worldDirection.x * _floatReference.floatVariableDict[(int)CharacterStatsVariables.SPEED].Value * Time.deltaTime, worldDirection.y * _floatReference.floatVariableDict[(int)CharacterStatsVariables.SPEED].Value * Time.deltaTime));
            #endif
        }

        
    }
}
