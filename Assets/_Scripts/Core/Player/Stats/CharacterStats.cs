using System.Collections;
using System.Collections.Generic;
using Core.Scriptables;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private FloatReference _floatReference;
    private float _maxLife;
    private float _baseSpeed;
    private float _baseDamage;
    private float _baseResistance;
    public float BaseDamage {get{return _baseDamage;} set { _baseDamage = value;}}
    public float BaseResistance {get{return _baseResistance;} set { _baseResistance = value;}}
    void Start()
    {
        _floatReference = GetComponent<FloatReference>();
        _maxLife =  (int) _floatReference.floatVariableDict[ (int) CharacterStatsVariables.HEALTH ].Value;
        _baseSpeed =  (int) _floatReference.floatVariableDict[ (int) CharacterStatsVariables.SPEED ].Value;
        _baseDamage =  (int) _floatReference.floatVariableDict[ (int) CharacterStatsVariables.DAMAGE ].Value;
        _baseResistance =  (int) _floatReference.floatVariableDict[ (int) CharacterStatsVariables.RESISTANCE ].Value;
    }
     public float GetDamage()
    {
        // character definition .current damage
        return _baseDamage;
        
    }

    public float GetResistance()
    {
        // characterdefinition current resistance
        return _baseResistance;
    }
}
