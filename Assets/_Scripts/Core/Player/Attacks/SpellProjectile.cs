using System.Collections;
using System.Collections.Generic;
using Core.Damage;
using Core.Pool;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/Spell")]
public class SpellProjectile : AttackDefinition
{
    [SerializeField] GameObject _objectToRequest;
    public float ProjectileSpeed;
    private IGenericPoolObject _projectile;
    public void Cast(GameObject caster, Vector3 origin, Vector3 target)
    {

        var projGO = PoolInstance.Instance[PoolObjects.BASICATTACK].RequestNewInstance( PoolObjects.BASICATTACK, _objectToRequest );
        _projectile = projGO.GetComponent<IGenericPoolObject>();
        _projectile.GetCurrentGameObject().transform.position = origin;
        _projectile.GetCurrentGameObject().GetComponent<BaseProjectileAttack>().Fire(caster, target, ProjectileSpeed, Range);
        // projectile.gameObject.layer = layer;
        _projectile.GetCurrentGameObject().GetComponent<IGenericPoolObject>().SetInUseObject(true);
        _projectile.GetCurrentGameObject().GetComponent<BaseProjectileAttack>().OnProjectileCollision += OnProjectileCollided;
    }
   


    private void OnProjectileCollided(GameObject caster, GameObject target)
    {
        var takeDamage = target.GetComponent<DamageBehavior>();
        if (caster == null || target == null)
            return;
        var casterStats = caster.GetComponent<CharacterStats>(); // TODO: Check stats location
        var targetStats = target.GetComponent<CharacterStats>();
        var attack = CreateAttack(casterStats, targetStats);
        takeDamage.EnemyTakenDamage(attack.Damage);
        _projectile.GetCurrentGameObject().GetComponent<BaseProjectileAttack>().OnProjectileCollision -= OnProjectileCollided;
    }
}
