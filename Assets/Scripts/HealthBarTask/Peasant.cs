using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Peasant : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private float _health;

    private Animator _animator;

    private float _maxHealth;

    public float Health => _health;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _maxHealth = _health;
    }

    private void Update()
    {
        _healthText.text = $"{_health} / {_maxHealth}";
        _healthBar.value = Mathf.MoveTowards(_healthBar.value, _health / _maxHealth, Time.deltaTime);

        if (_health == 0 && _healthBar.value == 0)
        {
            Peasant peasant = gameObject.GetComponent<Peasant>();

            _animator.SetTrigger(AnimatorTriggers.Killed);

            Destroy(peasant);
        }
    }

    public void TakeDamage(float damageValue)
    {
        _animator.SetTrigger(AnimatorTriggers.Damaged);

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
        _animator.SetTrigger(AnimatorTriggers.Healed);

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
