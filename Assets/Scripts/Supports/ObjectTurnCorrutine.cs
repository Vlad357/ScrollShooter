using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScrollShooter.Supports
{
    public class ObjectTurnCorrutine : MonoBehaviour
    {
        public GameObject objectTurn;

        [SerializeField] private float timeWait = 0.1f;
        public void TurnObject()
        {
            StartCoroutine(Turn());
        }

        private IEnumerator Turn()
        {
            yield return new WaitForSeconds(timeWait);
            objectTurn.SetActive(true);
        }
    }
}