using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterCreateStateView : UICompanent, IPointerClickHandler {
    public event Action<CharacterCreateStateView> CharacrerCreateStateViewSelected;

    [SerializeField] private Image _background;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameLabel;

    [SerializeField] private Color _selectionColor;
    [SerializeField] private Color _defaultColor;

    public bool IsSelected { get; private set; }
    public CreateStateConfig Config { get; private set; }

    public void Init(CreateStateConfig config) {
        Config = config;

        UpdateContent();
    }

    public override void Activate(bool status) {
        if (status == true) {
            Select();
        } 
        else {
            UpdateContent();
        }     
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (IsSelected == false) {
            Select();
            _background.transform.position += Vector3.left * -50;
        }
    }

    public override void UpdateContent() {
        _icon.sprite = Config.Icon;
        _icon.color = _defaultColor;
        _nameLabel.text = Config.Name;

        if (IsSelected == true)
            _background.transform.position += Vector3.left * 50;

        IsSelected = false;
    }

    public override void Select() {
        IsSelected = true;
        _icon.color = _selectionColor;

        CharacrerCreateStateViewSelected?.Invoke(this);
    }
}

