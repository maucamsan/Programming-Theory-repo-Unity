using UnityEngine;
using UnityEngine.UI;
using Core.Player;
using Core.Scriptables;
namespace Core.Enemy
{

    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private float _minStopDistance = 1;
       
        private FloatReference _floatVaribles;

        private GameObject Player;

        protected float waitTime = 30.0f;
        protected Vector3 upwardVector;
        protected Vector3 differenceVector;

        private Image healthBar;

        void Awake()
        {
            _floatVaribles = GetComponent<FloatReference>();
            healthBar = GetComponentInChildren<Image>();
            PlayerController.PlayerToFollow += ReferencePlayer;

        }

        void OnEnable()
        {
            PlayerController.PlayerToFollow += ReferencePlayer;
        }
        void OnDisable()
        {
            PlayerController.PlayerToFollow -= ReferencePlayer;
        }
        protected void FollowCharacter()
        {
            if (Vector3.Distance(Player.transform.position, transform.position) < _minStopDistance)
                return;
            // if (MoveSpeed.AssignVarible((int) Variables.SPEED) == -1)
            //     return;
            this.transform.Translate(this.transform.up *  _floatVaribles.floatVariableDict[((int) Variables.SPEED)].Value   * Time.deltaTime, Space.World);
            transform.Rotate(0.0f, 0.0f, CalculateAngle());
            // if (transform.position.x == Player.transform.position.x - 1 || transform.position.y == Player.transform.position.y - 1)
            //     transform.position = Player.transform.position - new Vector3(1, 1, 0);

        }

        protected float CalculateAngle()
        {
            Vector3 upwardVector = this.transform.up;
            Vector3 differenceVector = (Player.transform.position - this.transform.position).normalized;
            float unityAngle = Vector3.SignedAngle(upwardVector, differenceVector, this.transform.forward);
            return unityAngle;
        }

        
    
        protected void ReferencePlayer(GameObject player)
        {
            if (!Player)
                Player = player;
        }
        
    }

}