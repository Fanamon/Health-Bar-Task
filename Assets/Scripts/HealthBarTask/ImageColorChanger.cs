using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorChanger : MonoBehaviour
{
    [SerializeField] private Color _lowValueColor;

    private Image _image;
    private Color _maxValueColor;

    private void Awake()
    {
        _image = GetComponent<Image>();

        LoadFromColor();
    }

    public void SetPosition(float position)
    {
        _image.color = Color.Lerp(_lowValueColor, _maxValueColor, position);
    }

    [ContextMenu("Load From Color")]
    private void LoadFromColor()
    {
        _maxValueColor = _image.color;
    }
}