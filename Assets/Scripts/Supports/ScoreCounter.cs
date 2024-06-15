using System;
using TMPro;
using UnityEngine;

namespace ScrollShooter.Supports
{
    public class ScoreCounter : MonoBehaviour
    {
        public TextMeshProUGUI textCounter;

        private ScoreCountData _scoreCountData;
        private ScoreCounterDataSave _scoreCounterDataSave;

        private void Start()
        {
            _scoreCountData = new ScoreCountData();

            string json = PlayerPrefs.GetString("Count");
            if(!json.Equals(String.Empty, StringComparison.Ordinal))
            {
                _scoreCountData = JsonUtility.FromJson<ScoreCountData>(json);
            }

            _scoreCounterDataSave = new ScoreCounterDataSave();

            textCounter.text = _scoreCountData.ScoreCount.ToString();
        }

        public void SetScore(float score)
        {
            if (score > 0)
            {
                _scoreCountData.ScoreCount += score;
                try
                {
                    textCounter.text = Convert.ToInt32(_scoreCountData.ScoreCount).ToString();
                    _scoreCounterDataSave.SaveToJsonOnPlayerPrefs(_scoreCountData);
                }
                catch
                {
                    Debug.LogError($"textCounter has not assigned");
                }
            }
        }
    }
}