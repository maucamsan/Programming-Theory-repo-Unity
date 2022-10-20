using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Core.Scriptables;

namespace Core.Damage
{
    public class DamageBehavior : MonoBehaviour
    {   
        public event Action<float> OnDamageTaken;
        private Image healthBar;
        [SerializeField] private float invincibilityTime = 0.4f; 
        private float _damage;
        private bool _takeDamage = false;

        private FloatReference _floatVarible;

        void Awake()
        {
            _floatVarible = GetComponent<FloatReference>();
            healthBar = GetComponentInChildren<Image>();
        }


        void TakeDamage()
        {
            // Mathf.InverseLerp(0, );
        }

        void ReduceHealthBarAmount(float damage)
        {
            if (healthBar.fillAmount < 0)
                return;
            healthBar.fillAmount -= damage/100; 
            OnDamageTaken?.Invoke(-1 * damage);
        }

        IEnumerator DamageTakenCoroutine(float _damage)
        {
            while (_takeDamage)
            {
                ReduceHealthBarAmount(_damage);
                yield return new WaitForSeconds(invincibilityTime);
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
                    // Debug.Log(_damage);
                    _takeDamage = true;
                    StartCoroutine(DamageTakenCoroutine(_damage));
                        
                    
                }
            }

            // Modify compared tag since the player does not make damage by contact
            if (other.CompareTag("Player"))
            {
                
            }
        }
    
      

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                _takeDamage = false;
                StopCoroutine("DamageTakenCoroutine");
            }
        }
    }

}