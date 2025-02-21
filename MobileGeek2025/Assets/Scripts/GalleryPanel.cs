using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryPanel : UIPanel {
    public event Action BackToMainClicked;
    public event Action<Character> ModifyCharacter;
    public event Action<Character> ShareCharacter;

    [SerializeField] private PrehistoryConfigs _prehistoryConfigs;
    [SerializeField] private CharacterViews _characterViews;

    [SerializeField] private Button _backButton;
    [SerializeField] private Button _modifyButton;
    [SerializeField] private Button _shareButton;
    
    private Character _currentCharacter;

    public void Init(List<Character> characters) {
        _characterViews.Init(characters, _prehistoryConfigs);

        SetButtonStatus(false);
    }

    public void ShowCharacters(List<Character> characters) {
        _characterViews.ShowCharacters(characters);
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true) {
            _characterViews.Show(true);
            AddListeners();
        }      
        else
            RemoveListeners();
    }

    public override void AddListeners() {
        base.AddListeners();

        _backButton.onClick.AddListener(BackButtonClick);
        _modifyButton.onClick.AddListener(ModifyButtonClick);
        _shareButton.onClick.AddListener(ShareButtonClick);

        _characterViews.CurrentCharacterChanged += OnCurrentCharacterChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _backButton.onClick.RemoveListener(BackButtonClick);
        _modifyButton.onClick.RemoveListener(ModifyButtonClick);
        _shareButton.onClick.RemoveListener(ShareButtonClick);

        _characterViews.CurrentCharacterChanged -= OnCurrentCharacterChanged;
    }

    private void SetButtonStatus(bool status) {
        _modifyButton.gameObject.SetActive(status);
        _shareButton.gameObject.SetActive(status);
    }

    private void OnCurrentCharacterChanged(Character character) {
        _currentCharacter = character;

        SetButtonStatus(true);
    }

    private void BackButtonClick() => BackToMainClicked?.Invoke();

    private void ModifyButtonClick() => ModifyCharacter?.Invoke(_currentCharacter);

    private void ShareButtonClick() => ShareCharacter?.Invoke(_currentCharacter);

}
