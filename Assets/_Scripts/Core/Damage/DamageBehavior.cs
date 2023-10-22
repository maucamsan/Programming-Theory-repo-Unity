using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Core.Scriptables;
using Core.Health;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
namespace Core.Damage
{
    public class DamageBehavior : MonoBehaviour, IDamageable
    {   
        // private Image healthBar;
        [SerializeField] private float _invincibilityTime = 0.4f; 
        private float _damage;
        // private SpriteMask _healthBar;
        private bool _takeDamage = false;

        private FloatReference _floatVarible;
        private Life _life;
        public event Action<float> OnDamageTaken;
        void Awake()
        {
            _floatVarible = GetComponent<FloatReference>();
            // healthBar = GetComponentInChildren<Image>();
            // _healthBar = GetComponentInChildren<SpriteMask>();
            _life = GetComponent<Life>();
        }
        public void TakeDamage(float damageToTake)
        {
            _life.ManageLife(-damageToTake);
            // if (_healthBar.transform.localScale.y <= 0) return;
            // _healthBar.transform.localScale -=  new Vector3(0, damageToTake *  _healthBar.transform.localScale.y / _initialScale , 0);
            // OnDamageTaken?.Invoke(-1 * damageToTake);
        }
     

        public IEnumerator DamageTakenCoroutine(float _damage)
        {
            while (_takeDamage)
            {
                TakeDamage(_damage);
                yield return new WaitForSeconds(_invincibilityTime);
            }
            // Activate animation for invincibility
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (gameObject.CompareTag("Player"))
            {
                if (other.CompareTag("Enemy"))
                {
                    
                    _damage = other.GetComponent<FloatReference>().floatVariableDict[(int)Variables.DAMAGE].Value;
                    Debug.Log("Supposedly done damage by enemy: " + _damage);
                    _takeDamage = true;
                    StartCoroutine(nameof (DamageTakenCoroutine), _damage);
                }
            }

            // Modify compared tag since the player does not make damage by contact
            if (other.CompareTag("Player"))
            {
                _damage = other.GetComponent<FloatReference>().floatVariableDict[(int)Variables.DAMAGE].Value;
                    Debug.Log("Supposedly done damage by Player: " + _damage);
                _takeDamage = true;
                StartCoroutine(nameof (DamageTakenCoroutine), _damage);
            }
        }
    
      

        void OnTriggerExit2D(Collider2D other)
        {
            _takeDamage = false;
            StopCoroutine( nameof(DamageTakenCoroutine));
            if (other.CompareTag("Enemy"))
            {
            }
        }

    }

}