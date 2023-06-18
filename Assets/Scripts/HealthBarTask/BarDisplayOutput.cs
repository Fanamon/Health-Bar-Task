using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class BarDisplayOutput : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Slider _healthBar;

    private Animator _animator;

    private bool _isCurrentBarValueNull;

    public bool IsCurrentBarValueNull => _isCurrentBarValueNull;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _isCurrentBarValueNull = _healthBar.value == 0;
    }

    public void SetAnimatorTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }

    public void WriteBarText(float currentValue, float maxValue)
    {
        _healthText.text = $"{currentValue} / {maxValue}";
    }

    public void ChangeBarValue(float currentValuePercentage, float timeDelta)
    {
        _healthBar.value = Mathf.MoveTowards(_healthBar.value, currentValuePercentage, timeDelta);
    }
}
