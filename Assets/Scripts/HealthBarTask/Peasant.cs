using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Peasant : MonoBehaviour
{
    [SerializeField] private float _health;

    private Animator _animator;

    public event UnityAction<float> HealthChanged;

    public float MaxHealth { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        MaxHealth = _health;
    }

    public void TakeDamage(float damageValue)
    {
        _health -= damageValue;

        _animator.SetTrigger(PeasantAnimatorTrigger.Damaged);

        ValidateValue();

        if (_health == 0 )
        {
            BeKilled();
        }
    }

    public void TakeHeal(float healValue)
    {
        _health += healValue;

        _animator.SetTrigger(PeasantAnimatorTrigger.Healed);

        ValidateValue();
    }

    private void BeKilled()
    {
        HealthChanged?.Invoke(_health);

        _animator.SetTrigger(PeasantAnimatorTrigger.Killed);

        Destroy(this);
    }

    private void ValidateValue()
    {
        _health = Mathf.Clamp(_health, 0f, MaxHealth);

        HealthChanged?.Invoke(_health);
    }
}
