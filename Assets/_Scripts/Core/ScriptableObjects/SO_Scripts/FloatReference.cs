using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Scriptables
{
    public enum Variables
    {
        DAMAGE, 
        HEALTH,
        SPEED
    }
    [System.Serializable]
    public class FloatReference : MonoBehaviour
    {

        [System.Serializable]
        public class VaribleType
        {
            public Variables SelectedVarible;
            public bool UseConstant;
            public float ConstantValue;
            public FloatVariable Variable;

            public float Value
            {
                get{ return UseConstant ? ConstantValue : Variable.Value; }
            }

        }
      
        [SerializeField] public VaribleType[] floatVariableDict;

        // public float AssignVarible(int v)
        // {
        float varToReturn;
        // switch()
        // {
        //     case (int) Variables.DAMAGE:
        //         varToReturn = floatVariableDict[(int) Variables.DAMAGE].Value;
        //         break;
        //     case (int) Variables.HEALTH:
        //         varToReturn = floatVariableDict[(int) Variables.HEALTH].Value;
        //         break;
        //     case (int) Variables.SPEED:
        //         varToReturn = floatVariableDict[(int) Variables.SPEED].Value;
        //         break;
        //     default:
        //         varToReturn = -1;
        //         break;
        // }
        // return varToReturn;
    // }
    }
}