using System;
using Events;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class Cell : MonoBehaviour
{
    public Image Image => _image;
    public Button Button => _button;

    [SerializeField] private Image _image;

    [SerializeField] private Image _background;

    [SerializeField] private Button _button;

    [SerializeField] private ColorProvider _colorProvider;

    private Action<Cell> _onCLicked;

    public void Initialize(Sprite gameItemView)
    {
        _image.sprite = gameItemView;
        _background.color = _colorProvider.GetRandomColor();
        _button.onClick.AddListener(Click);
    }

    private void Click()
    {
        _onCLicked?.Invoke(this);
    }

    public void SetClickCallBack(Action<Cell> onClicked)
    {
        _onCLicked = onClicked;
    }
}