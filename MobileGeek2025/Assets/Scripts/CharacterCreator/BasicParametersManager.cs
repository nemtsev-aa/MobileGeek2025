using System;
using UnityEngine;

public class BasicParametersManager : CustomizationManager {
    public event Action<BasicParameterTypes, int> BasicParametersChanged;

    [SerializeField] private BasicParameterViewConfigs _configs;
    [SerializeField] private BasicParametersPiker _piker;

    private Character _currentCharacter;

    public override void Init(BodyPartSelectionManager selectionManager, Piker piker) {
        base.Init(selectionManager, piker);

        _currentCharacter = CharacterManager.Instance.Character;
        
        _piker = (BasicParametersPiker)piker;
        _piker.SetConfig(_configs, _currentCharacter.BasicParameters);
    }

    public override void AddListeners() {
        base.AddListeners();

        _piker.BasicParameterValueChanged += OnBasicParameterValueChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _piker.BasicParameterValueChanged -= OnBasicParameterValueChanged;
    }

    private void OnBasicParameterValueChanged(BasicParameterTypes type, int value) {
        BasicParametersChanged?.Invoke(type, value);
    }
}
