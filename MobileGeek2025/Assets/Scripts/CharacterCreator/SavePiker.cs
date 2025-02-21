using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SavePiker : Piker {
    public event Action<string> Saved;

    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private Button _saveButton;

    public override void Init() {
        base.Init();
    }

    public override void Show(bool status) {
        base.Show(status);

    }

    public override void AddListeners() {
        base.AddListeners();

        _saveButton.onClick.AddListener(SaveButtonClick);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _saveButton.onClick.RemoveListener(SaveButtonClick);
    }

    public void ShowName(string name) {
        _nameInput.text = name;
    }

    private void SaveButtonClick() => Saved?.Invoke(_nameInput.text);

}
