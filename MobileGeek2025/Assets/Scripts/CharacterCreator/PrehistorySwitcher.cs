using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PrehistorySwitcher : UICompanent, IPointerClickHandler {
    public event Action<PrehistorySwitcher> PrehistorySelected;

    [SerializeField] private Image _background;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameLabel;

    [SerializeField] private Color _selectionColor;
    [SerializeField] private Color _defaultColor;

    public bool IsSelected { get; private set; }
    public PrehistoryConfig Config { get; private set; }

    public void Init(PrehistoryConfig config) {
        Config = config;

        UpdateContent();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (IsSelected == false) {
            Select();
            _background.transform.localScale += Vector3.one * 0.2f;
        }
    }

    public override void UpdateContent() {
        _icon.sprite = Config.Icon;
        _icon.color = _defaultColor;
        _nameLabel.text = Config.Name;

        if (IsSelected == true)
            _background.transform.localScale = Vector3.one;

        IsSelected = false;
    }

    public override void Select() {
        IsSelected = true;
        _icon.color = _selectionColor;

        PrehistorySelected?.Invoke(this);
    }
}


