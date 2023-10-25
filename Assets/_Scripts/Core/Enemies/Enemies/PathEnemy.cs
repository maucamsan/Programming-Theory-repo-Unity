using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Scriptables;

namespace Core.Enemy
{

    public class PathEnemy : Enemy
    {
        [SerializeField] float _timeInGame = 3.0f;
        Vector3 _direction = Vector3.zero;
        private bool _hasSpawned;
        protected override void FollowCharacter()
        {
            //base.FollowCharacter();
            if (_direction != Vector3.zero)
                transform.Translate(_direction *  _floatVaribles.floatVariableDict[ (int) CharacterStatsVariables.SPEED ].Value   * Time.deltaTime, Space.World);
            else
                _direction = (PlayerGO.transform.position - transform.position).normalized;
            if ((PlayerGO.transform.position - transform.position).sqrMagnitude > 625)
                StartCoroutine(nameof (DisableAfterTime));

        }
        void FixedUpdate()
        {
            FollowCharacter();
        }

        IEnumerator DisableAfterTime()
        {
            yield return new WaitForSeconds(_timeInGame);
            SetInUseObject(false);
            yield return null;
        }
    }

}