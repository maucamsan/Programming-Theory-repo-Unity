using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class XPManager : MonoBehaviour
{
    public static Action OnLevelUp;
    [SerializeField] Image xpBarImage;
    [SerializeField] float _baseResistanceIncreaseFactor = 0.02f;
    [SerializeField] float _baseDamageIncreaseFactor = 0.02f;
    public void AddExperience(double valueToAdd)
    {
        xpBarImage.fillAmount += (float) valueToAdd;
        if (xpBarImage.fillAmount >= 1.0f)
        {
            xpBarImage.fillAmount = 0.0f;
            LevelUp();
        }
    }
    
    private void LevelUp()
    {
        OnLevelUp?.Invoke();
        var characterStats = GetComponent<CharacterStats>();
        characterStats.BaseResistance +=_baseResistanceIncreaseFactor;
        characterStats.BaseDamage +=_baseDamageIncreaseFactor;
    }
    
}
