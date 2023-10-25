using System;
using System.Collections;
using System.Collections.Generic;
using Core.Pool;
using UnityEngine;

namespace Core.Damage

{
    public class Death : MonoBehaviour, IKillable
    {
        public TypeOfCharacter  currentTypeOfCharacter;

        public void ManageDeath(IGenericPoolObject poolObject)
        {
           switch(currentTypeOfCharacter)
           {
                case TypeOfCharacter.PLAYER:
                    // Game over
                    // Record score
                    // Record endured time
                    // display rewarded ad
                    break;
                case TypeOfCharacter.ENEMY:
                    poolObject.SetInUseObject(false);
                    break;
                case TypeOfCharacter.BOSS:
                    break;
                case TypeOfCharacter.PROP:
                    break;
                default:
                    throw new Exception("There is not this type of character");
           }
        }

        private void DeathMecanism()
        {
            // Particles effect
            // 
        }
    }
    public enum TypeOfCharacter
    {
        PLAYER, ENEMY, BOSS, PROP
    }

}
