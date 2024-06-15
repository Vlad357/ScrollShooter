using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScrollShooter.Supports
{
    public class ScoreCounterDataSave
    {
        public void SaveToJsonOnPlayerPrefs(ScoreCountData _scoreCountData)
        {
            string json = JsonUtility.ToJson(_scoreCountData);
            PlayerPrefs.SetString("Count", json);
        }
    }
}