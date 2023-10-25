using UnityEngine;
using UnityEngine.UI;
using Core.Player;
using Core.Scriptables;
using Core.Pool;
using System;
namespace Core.Enemy
{

    public abstract class Enemy : MonoBehaviour, IGenericPoolObject, IAttackable
    {
        public static event Func<GameObject> OnPlayerReferenceNeeded;
        [SerializeField] private float _minStopDistance = 1;
       
        protected FloatReference _floatVaribles;

        public GameObject PlayerGO;

        protected float waitTime = 30.0f;
        protected Vector3 upwardVector;
        protected Vector3 differenceVector;

        private Image healthBar;
    
        private PoolInstance _poolManagerInstance;
        private bool _inUse = false;
        public bool InUse { get => _inUse; set => _inUse = value; }
        public PoolObjects PoolObject { get => EnemyPoolObject; set => EnemyPoolObject = value; }
        
        public PoolObjects EnemyPoolObject = PoolObjects.MINIONENEMY;
        void Awake()
        {
            _floatVaribles = GetComponent<FloatReference>();
            healthBar = GetComponentInChildren<Image>();
            PlayerController.PlayerToFollow += ReferencePlayer;

        }

        void OnEnable()
        {
            PlayerController.PlayerToFollow += ReferencePlayer;
            //PlayerGO = FindObjectOfType<PlayerController>().gameObject;
        }
        void OnDisable()
        {
            PlayerController.PlayerToFollow -= ReferencePlayer;
        }
        protected virtual void FollowCharacter()
        {
            
            if (!PlayerGO)
                PlayerGO = OnPlayerReferenceNeeded?.Invoke();
            //  FindObjectOfType<PlayerController>().gameObject;
            if (Vector3.Distance(PlayerGO.transform.position, transform.position) < _minStopDistance)
                return;
            this.transform.Translate(this.transform.up *  _floatVaribles.floatVariableDict[ (int) CharacterStatsVariables.SPEED ].Value   * Time.deltaTime, Space.World);
            transform.Rotate(0.0f, 0.0f, CalculateAngle());
            

        }

        protected float CalculateAngle()
        {
            // if (!PlayerGO)
            //     PlayerGO = PlayerController.PlayersReference();
            Vector3 upwardVector = this.transform.up;
            Vector3 differenceVector = (PlayerGO.transform.position - this.transform.position).normalized;
            float unityAngle = Vector3.SignedAngle(upwardVector, differenceVector, this.transform.forward);
            return unityAngle;
        }

        
    
        protected void ReferencePlayer(GameObject player)
        {
            if (!PlayerGO)
                PlayerGO = player;
        }

        public void SetObjectPosition(Vector3 playerPosition)
        {
            throw new System.NotImplementedException();
        }

        public void SetInUseObject(bool _isActive)
        {
            _inUse = _isActive;
            gameObject.SetActive(_inUse);
        }

        public GameObject GetCurrentGameObject()
        {
            return this.gameObject;
        }

        public void  Action(PoolInstance _poolInstance, Vector3 position, Quaternion rotation, Transform possibleParent = null)
        {
            // gameObject.SetActive(true);
            // _inUse = true;
            _poolManagerInstance = _poolInstance;
            SetInUseObject(true);
            transform.SetPositionAndRotation(position, rotation);
            
        }

        public void OnAttack(GameObject attacker, Attack attack)
        {
            // throw new System.NotImplementedException();
        }

        [ContextMenu("Disable")]
        public void DisableEnemy()
        {
            InUse = false;
            gameObject.SetActive(false);
        }
    }

}