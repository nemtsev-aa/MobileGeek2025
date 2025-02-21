using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterViews : Piker {
    public event Action<Character> CurrentCharacterChanged;

    [SerializeField] private CharacterView _characterViewPrefab;
    [SerializeField] private RectTransform _viewParent;

    private List<Character> _characters;
    private PrehistoryConfigs _prehistoryConfigs;

    private List<CharacterView> _views;

    private Character _currentCharacter;
    private CharacterView _currentView;

    public void Init(List<Character> characters, PrehistoryConfigs prehistoryConfigs) {
        _characters = characters;
        _prehistoryConfigs = prehistoryConfigs;
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true && _views == null)
            CreateViews();
    }

    public void ShowCharacters(List<Character> characters) {
        if (characters.Count == 0)
            return;

        if (_views != null)
            RemoveViews();

        _views = new List<CharacterView>();
        foreach (Character iCharacter in characters) {
            Sprite sprite = GetSpriteByPrehistoryType(iCharacter.Prehistory);

            CharacterView view = Instantiate(_characterViewPrefab, _viewParent);
            view.Init(iCharacter, sprite);
            view.CharacterViewSelected += OnCharacterViewSelected;

            _views.Add(view);
        }
    }

    private void CreateViews() {
        if (_characters == null || _characters.Count == 0)
            return;

        _views = new List<CharacterView>();
        foreach (Character iCharacter in _characters) {
            Sprite sprite = GetSpriteByPrehistoryType(iCharacter.Prehistory);

            CharacterView view = Instantiate(_characterViewPrefab, _viewParent);
            view.Init(iCharacter, sprite);
            view.CharacterViewSelected += OnCharacterViewSelected;

            _views.Add(view);
        }
    }

    private void OnCharacterViewSelected(CharacterView item) {
        if (_currentView != null && _currentView.Equals(item) != true) {
            _currentView.Activate(false);
        }

        _currentCharacter = item.Character;
        _currentView = item;
        _currentView.Activate(true);

        CurrentCharacterChanged?.Invoke(_currentCharacter);
    }

    private Sprite GetSpriteByPrehistoryType(PrehistoryTypes type) {
        return _prehistoryConfigs.Configs.FirstOrDefault(t => t.Type == type).Icon;
    }


    private void RemoveViews() { 
        foreach (var item in _views) {
            Destroy(item.gameObject);
        }

        _views.Clear();
    }
}
