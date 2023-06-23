using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class BarDisplayOutput : MonoBehaviour
{
    [SerializeField] private Peasant _character;
    [SerializeField] private TMP_Text _barText;
    [SerializeField] private Image _barImage;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Color _lowValueColor;
    [SerializeField] private UnityEvent _damaged;
    [SerializeField] private UnityEvent _healed;
    [SerializeField] private UnityEvent _killed;

    private Color _maxValueColor;

    private Coroutine _barValueDisplayer;

    private float _currentValuePercentage;

    private void Awake()
    {
        LoadFromColor();
    }

    private void Start()
    {
        DisplayBarChanges();
    }

    [ContextMenu("Load From Color")]
    private void LoadFromColor()
    {
        _maxValueColor = _barImage.color;
    }

    public void SetColorPosition(float position)
    {
        _barText.color = Color.Lerp(_lowValueColor, _maxValueColor, position);
        _barImage.color = Color.Lerp(_lowValueColor, _maxValueColor, position);
    }

    public void Damage()
    {
        if (_character != null)
        {
            _damaged.Invoke();

            DisplayBarChanges();

            if (_character.Health == 0)
            {
                _killed.Invoke();
            }
        }
    }

    public void Heal()
    {
        if (_character != null)
        {
            _healed.Invoke();

            DisplayBarChanges();
        }
    }

    private void DisplayBarChanges()
    {
        _currentValuePercentage = _character.Health / _character.MaxHealth;

        WriteBarText(_character.Health, _character.MaxHealth);

        if (_barValueDisplayer != null)
        {
            StopCoroutine(_barValueDisplayer);
        }

        _barValueDisplayer = StartCoroutine(ChangeBarValue(_currentValuePercentage));
    }

    private void WriteBarText(float currentValue, float maxValue)
    {
        _barText.text = $"{currentValue} / {maxValue}";
    }

    private IEnumerator ChangeBarValue(float currentValuePercentage)
    {
        while (_healthBar.value != currentValuePercentage)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, currentValuePercentage, Time.deltaTime);

            yield return null;
        }
    }
}
