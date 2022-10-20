using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Enemy
{

    public class MinionEnemy : Enemy, IPooledObject
    {
        
        public void OnObjectSpawn()
        {
            this.GetComponent<RectTransform>().localScale = new Vector3(1 ,1, 1);
        }
        void FixedUpdate()
        {
            FollowCharacter();
        }
    
    }
}
