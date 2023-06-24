using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Peasant : MonoBehaviour
{
    [SerializeField] private float _health;

    public UnityAction<float> OnHealthChanged;

    private Animator _animator;

    public float MaxHealth { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        MaxHealth = _health;
    }

    public void TakeDamage(float damageValue)
    {
        _health -= damageValue;

        _animator.SetTrigger("Damaged");

        ValidateValue();

        if (_health == 0 )
        {
            BeKilled();
        }
    }

    public void TakeHeal(float healValue)
    {
        _health += healValue;

        _animator.SetTrigger("Healed");

        ValidateValue();
    }

    private void BeKilled()
    {
        OnHealthChanged?.Invoke(_health);

        _animator.SetTrigger("Killed");

        Destroy(this);
    }

    private void ValidateValue()
    {
        _health = Mathf.Clamp(_health, 0f, MaxHealth);

        OnHealthChanged?.Invoke(_health);
    }
}
