using System;
using System.Collections.Generic;

public class SaveManager : CustomizationManager {
    public event Action IsSaved;

    private SavePiker _savePiker;
    private Character _currentCharacter;

    public override void Init(BodyPartSelectionManager selectionManager, Piker piker) {
        base.Init(selectionManager, piker);

        _savePiker = (SavePiker)piker;
        _currentCharacter = CharacterManager.Instance.Character;
        _savePiker.ShowName(_currentCharacter.Name);
    }

    public override void AddListeners() {
        base.AddListeners();

        _savePiker.Saved += OnSaved;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _savePiker.Saved -= OnSaved;
    }

    private void OnSaved(string name) {
        _currentCharacter.SetName(name);

        IsSaved?.Invoke();
    }

    public bool GetCharacterList(out List<Character> list) {
        list = new List<Character>();
        var characters = CharacterManager.Instance.LoadAllCharacters();

        if (characters.Count == 0) 
            return false;

        list.AddRange(characters);
        return true;
    }
}
