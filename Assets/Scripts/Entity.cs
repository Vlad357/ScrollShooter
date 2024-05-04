using System;
using System.Collections;
using System.Collections.Generic;
using ScrollShooter;
using UnityEngine;

[RequireComponent(typeof(EntityHealth))]
public class Entity : MonoBehaviour
{
    private float _scoreEntity = 10f;
    private EntityHealth _health;
    private void Start()
    {
        _health = new EntityHealth();
        _health.deathEvent += Death;
        _health.Regeneration();
    }

    private void Death(GameObject killer)
    {
        killer.TryGetComponent(out ScoreCounter counter);
        counter.SetScore(_scoreEntity);
    }
}
