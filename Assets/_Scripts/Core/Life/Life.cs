using UnityEngine;
using System;
using Core.Damage;
using Core.Scriptables;

namespace Core.Health
{
    public class Life : MonoBehaviour, IKillable
    {
        public event Action OnDeath;
        FloatReference _statsValues;
        DamageBehavior _damageBehavior;
        float _maxBaseLife;
        float _currentBaseLife = 50;
        private LifeHUD _lifeHud;
        public float CurrentBaseLife
        {
            get {return _currentBaseLife;}
            private set{}
        }

        public void ManageLife(float valueToModify)
        {
            _currentBaseLife += valueToModify;
            _lifeHud.ChangeSpriteFiller(_maxBaseLife, _currentBaseLife);
            if (_currentBaseLife >= _maxBaseLife)
                _currentBaseLife = _maxBaseLife;
            if (_currentBaseLife <= 0 && gameObject.CompareTag("Player"))
            {
                Debug.Log("Player's death");
                _currentBaseLife = 0;
                ManageDeath();
                // gameover
                // Display gameover UI and stats of the run
                // if first death, display rewarded ad to revive
                // Stop game -> notiy to gameover manager or class
                // Save data
            }
            else if (_currentBaseLife <= 0)
            {
                // Kill Enemy
                Debug.Log("Enemy's death");
                _currentBaseLife = 0;
            }
        }
        void Awake()
        {
            _damageBehavior = GetComponent<DamageBehavior>();
            _statsValues = GetComponent<FloatReference>();
            _lifeHud = GetComponent<LifeHUD>();
            _maxBaseLife = _statsValues.floatVariableDict[(int) Variables.HEALTH].Value;
            _currentBaseLife = _maxBaseLife;
            
        }

        

        public void ManageDeath()
        {
            OnDeath?.Invoke();
        }
    }
}

