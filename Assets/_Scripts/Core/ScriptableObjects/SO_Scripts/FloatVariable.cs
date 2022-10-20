using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Scriptables

{
    [CreateAssetMenu (fileName = "Variables", menuName ="Scriptable Objects/Variables")]
    public class FloatVariable : ScriptableObject
    {
        public float Value;
       
    }
}