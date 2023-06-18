using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BarDisplayOutput))]
public class Peasant : MonoBehaviour
{
    [SerializeField] private float _health;

    private BarDisplayOutput _output;

    private float _maxHealth;
    private float _currentValuePercentage;

    private void Awake()
    {
        _output = GetComponent<BarDisplayOutput>();

        _maxHealth = _health;
    }

    private void Update()
    {
        _currentValuePercentage = _health / _maxHealth;

        _output.WriteBarText(_health, _maxHealth);
        _output.ChangeBarValue(_currentValuePercentage, Time.deltaTime);

        if (_health == 0 && _output.IsCurrentBarValueNull)
        {
            Peasant peasant = gameObject.GetComponent<Peasant>();

            _output.SetAnimatorTrigger(AnimatorTriggers.Killed);

            Destroy(peasant);
        }
    }

    public void TakeDamage(float damageValue)
    {
        _output.SetAnimatorTrigger(AnimatorTriggers.Damaged);

        if (_health - damageValue < 0)
        {
            _health = 0 ;
        }
        else
        {
            _health -= damageValue;
        }
    }

    public void TakeHeal(float healValue)
    {
        _output.SetAnimatorTrigger(AnimatorTriggers.Healed);

        if (_health + healValue > _maxHealth)
        {
            _health = _maxHealth;
        }
        else
        {
            _health += healValue;
        }
    }
}
