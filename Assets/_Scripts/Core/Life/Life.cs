using UnityEngine;
using System;
using Core.Damage;
using Core.Scriptables;
using Core.Pool;

namespace Core.Health
{
    public class Life : MonoBehaviour
    {
        public event Action OnDeath;
        FloatReference _statsValues;
        DamageBehavior _damageBehavior;
        float _maxBaseLife;
        float _currentBaseLife = 50;
        private LifeHUD _lifeHud;
        private IKillable _characterTokill;
        public float CurrentBaseLife
        {
            get {return _currentBaseLife;}
            private set{}
        }

        public void ManageLife(float valueToModify)
        {
            _currentBaseLife += valueToModify;

            _lifeHud.ChangeSpriteFiller(_maxBaseLife, _currentBaseLife);

            if (_currentBaseLife >= _maxBaseLife) _currentBaseLife = _maxBaseLife;
            if (_currentBaseLife > 0) return;

            if( gameObject.CompareTag("Player") )
            {
                OnDeath?.Invoke();
                // gameover
                // Display gameover UI and stats of the run
                // if first death, display rewarded ad to revive
                // Stop game -> notiy to gameover manager or class
                // Save data
            }
            
            _characterTokill.ManageDeath(gameObject.GetComponent<IGenericPoolObject>());
            _currentBaseLife = _maxBaseLife;
        }
        void OnDisable()
        {
            _maxBaseLife = _statsValues.floatVariableDict[(int) CharacterStatsVariables.HEALTH].Value;
            _currentBaseLife = _maxBaseLife;
        }
        void Awake()
        {
            _damageBehavior = GetComponent<DamageBehavior>();
            _statsValues = GetComponent<FloatReference>();
            _lifeHud = GetComponent<LifeHUD>();
            _maxBaseLife = _statsValues.floatVariableDict[(int) CharacterStatsVariables.HEALTH].Value;
            _currentBaseLife = _maxBaseLife;
            _characterTokill = gameObject.GetComponent<IKillable>();
        }
    }
}

