using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Damage
{

    public interface IDamageable
    {
        public void TakeDamage(float damageToTake);
        public IEnumerator DamageTakenCoroutine(float damage);
    }

}