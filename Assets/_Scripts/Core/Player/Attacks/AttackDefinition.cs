using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/BaseAttack")]
public class AttackDefinition : ScriptableObject
{
    public float Cooldown;
    public float Range;
    public float MinDamage;
    public float MaxDamage;
    public float CriticalMultiplier;
    public float CriticalChance;

    public Attack CreateAttack(CharacterStats attackerStats, CharacterStats defenderStats)
    {
        float coreDamage =  attackerStats.GetDamage();
        coreDamage += Random.Range(MinDamage, MaxDamage);
        bool IsCritical = Random.value < CriticalChance;

        if (IsCritical)
            coreDamage *= CriticalMultiplier;
        if (defenderStats != null)
             coreDamage -= defenderStats.GetResistance();

        Attack attack = new((int)coreDamage, IsCritical);
        return attack;
    }


}
