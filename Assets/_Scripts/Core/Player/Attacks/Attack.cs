using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Attack
{
    private readonly int _damage;
    private readonly bool _critical;
    public Attack (int damage, bool critical)
    {
        _damage = damage;
        _critical = critical;
    }  

    public int Damage 
    {
        get {return _damage;}
    }

    public bool IsCritical
    {
        get {return _critical;}
    }

   
}
