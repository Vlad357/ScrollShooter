using UnityEngine;
using UnityEngine.UI;

namespace ScrollShooter.Supports
{
    public class Bar : MonoBehaviour
    {
        public Image bar;

        private float _valueCurrent;
        private float _valueMax;

        public float ValueCurrent
        {
            get { return _valueCurrent; }
            set
            {
                _valueCurrent = value;
                SetValueBar(_valueCurrent, _valueMax);
            }
        }
        public float ValueMax
        {
            get { return _valueMax; }
            set
            {
                _valueMax = value;
                SetValueBar(_valueCurrent, _valueMax);
            }
        }

        public void SetValueBar(float currentValue, float maxValue)
        {
            bar.fillAmount = currentValue / maxValue;
        }
    }
}