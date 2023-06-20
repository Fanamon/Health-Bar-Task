using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class BarDisplayOutput : MonoBehaviour
{
    [SerializeField] private TMP_Text _barText;
    [SerializeField] private Image _barImage;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Color _lowValueColor;

    private Animator _animator;
    private Color _maxValueColor;

    private bool _isCurrentBarValueNull;

    public bool IsCurrentBarValueNull => _isCurrentBarValueNull;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        LoadFromColor();
    }

    [ContextMenu("Load From Color")]
    private void LoadFromColor()
    {
        _maxValueColor = _barImage.color;
    }

    public void SetPosition(float position)
    {
        _barText.color = Color.Lerp(_lowValueColor, _maxValueColor, position);
        _barImage.color = Color.Lerp(_lowValueColor, _maxValueColor, position);
    }

    public void SetAnimatorTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }

    public void WriteBarText(float currentValue, float maxValue)
    {
        _barText.text = $"{currentValue} / {maxValue}";
    }

    public void ChangeBarValue(float currentValuePercentage, float timeDelta)
    {
        _healthBar.value = Mathf.MoveTowards(_healthBar.value, currentValuePercentage, timeDelta);

        _isCurrentBarValueNull = _healthBar.value == 0;
    }
}
