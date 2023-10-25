using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Core.Scriptables
{
    public enum CharacterStatsVariables
    {
        DAMAGE, 
        HEALTH,
        SPEED,
        RESISTANCE,
    }
    [System.Serializable]
    public class FloatReference : MonoBehaviour
    {
        // TODO: Implement multiton to manage different instances of the static dictionary so that it is not created more than once!
        [System.Serializable]
        public class VariableType
        {
            public CharacterStatsVariables SelectedVariable;
            public bool UseConstant;
            public float ConstantValue;
            public FloatVariable Variable;

            public float Value
            {
                get{ return UseConstant ? ConstantValue : Variable.Value; }
            }

        }
      
        [SerializeField] public VariableType[] floatVariableDict;
        

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