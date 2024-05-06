using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Image bar; 

    public void SetValueBar(float currentValue, float maxValue)
    {
        bar.fillAmount = currentValue/maxValue;
    }
}
