using System.Collections;
using System.Collections.Generic;
using Core.Pool;
using UnityEngine;


namespace Core.Damage
{

    public interface IKillable 
    {
        public void ManageDeath(IGenericPoolObject genericPoolObject);
    }

}