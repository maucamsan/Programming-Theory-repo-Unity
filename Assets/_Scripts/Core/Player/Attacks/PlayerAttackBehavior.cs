using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttackBehavior : MonoBehaviour
{
    [SerializeField] SpellProjectile _spellBaseProjectile;
    [SerializeField] private LayerMask _layerMask;
    public float enemiesCircleDetectionRadius = 10.0f;
    GameObject target;
    private bool _hasTarget = false;
    [SerializeField] private float _waitTime = 3.0f;

    void FixedUpdate()
    {
        if (!_hasTarget)
            GetclosestEnemy();
    }
    public void GetclosestEnemy()
    {
        // get the closest enemy to be attacked
        RaycastHit2D closestEnemyHit =  Physics2D.CircleCast(transform.position, 10, Vector2.up, 2, _layerMask);
        if (closestEnemyHit.collider != null)
        {
            _hasTarget = true;
            AttackTarget(closestEnemyHit.collider.gameObject);
            StartCoroutine(nameof ( WaitUntilNextAttack), _waitTime);
        }
    }
    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 10);
    }
    #endif

    public void AttackTarget(GameObject target)
    {
        _spellBaseProjectile.Cast(gameObject, transform.position, target.transform.position);
    }

    IEnumerator WaitUntilNextAttack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _hasTarget = false;
    }
}
