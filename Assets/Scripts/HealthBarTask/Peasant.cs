using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Peasant : MonoBehaviour
{
    [SerializeField] private float _health;

    private float _maxHealth;

    public float Health => _health;
    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        _maxHealth = _health;
    }

    public void TakeDamage(float damageValue)
    {
        _health -= damageValue;

        ValidateValue();
    }

    public void TakeHeal(float healValue)
    {
        _health += healValue;

        ValidateValue();
    }

    public void BeKilled()
    {
        Destroy(this);
    }

    private void ValidateValue()
    {
        _health = Mathf.Clamp(_health, 0f, _maxHealth);
    }
}
