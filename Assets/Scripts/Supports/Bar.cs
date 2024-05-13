using UnityEngine;
using UnityEngine.UI;

namespace ScrollShooter.Supports
{
    public class Bar : MonoBehaviour
    {
        public Image bar;

        public void SetValueBar(float currentValue, float maxValue)
        {
            bar.fillAmount = currentValue / maxValue;
        }
    }
}