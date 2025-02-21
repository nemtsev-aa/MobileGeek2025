using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CharacterView : UICompanent, IPointerClickHandler {
    public event Action<CharacterView> CharacterViewSelected;

    [SerializeField] private Image _frame;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameLabel;

    public Character Character { get; private set; }
    private Sprite _sprite;

    public void Init(Character character, Sprite icon) {
        Character = character;
        _sprite = icon;

        Activate(false);
    }

    public void OnPointerClick(PointerEventData eventData) {
        Select();
        CharacterViewSelected?.Invoke(this);
    }

    public override void Select() {
        base.Select();

        _frame.gameObject.SetActive(true);
    }

    public override void UpdateContent() {
        base.UpdateContent();

        _frame.gameObject.SetActive(false);
        _nameLabel.text = Character.Name;
        _icon.sprite = _sprite;
    }
}
