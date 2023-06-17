using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _lowValueColor;

    private TMP_Text _text;
    private Color _maxValueColor;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();

        LoadFromColor();
    }

    public void SetPosition(float position)
    {
        _text.color = Color.Lerp(_lowValueColor, _maxValueColor, position);
    }

    [ContextMenu("Load From Color")]
    private void LoadFromColor()
    {
        _maxValueColor = _text.color;
    }
}
