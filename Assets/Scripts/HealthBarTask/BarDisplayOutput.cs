using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BarDisplayOutput : MonoBehaviour
{
    [SerializeField] private Peasant _player;
    [SerializeField] private TMP_Text _barText;
    [SerializeField] private Image _barImage;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Color _lowValueColor;

    private Color _maxValueColor;

    private Coroutine _barValueDisplayer;

    private void Awake()
    {
        LoadFromColor();
    }

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void Start()
    {
        StartCoroutine(ChangeBarValue(_player.MaxHealth));
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

    private void OnHealthChanged(float currentValue)
    {
        if (_barValueDisplayer != null)
        {
            StopCoroutine(_barValueDisplayer);
        }

        _barValueDisplayer = StartCoroutine(ChangeBarValue(currentValue));
    }

    private void WriteBarText(float currentValue, float maxValue)
    {
        _barText.text = $"{currentValue} / {maxValue}";
    }

    private IEnumerator ChangeBarValue(float currentValue)
    {
        float currentValuePercentage = currentValue / _player.MaxHealth;

        WriteBarText(currentValue, _player.MaxHealth);

        while (_healthBar.value != currentValuePercentage)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, currentValuePercentage, Time.deltaTime);

            yield return null;
        }
    }
}
