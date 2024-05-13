using System;
using TMPro;
using UnityEngine;

namespace ScrollShooter.Supports
{
    public class ScoreCounter : MonoBehaviour
    {
        public TextMeshProUGUI textCounter;
        private float _score;

        public void SetScore(float score)
        {
            if (score > 0)
            {
                _score += score;
                try
                {
                    textCounter.text = Convert.ToInt32(_score).ToString();
                }
                catch
                {
                    Debug.LogError($"textCounter has not assigned");
                }
            }
        }
    }
}