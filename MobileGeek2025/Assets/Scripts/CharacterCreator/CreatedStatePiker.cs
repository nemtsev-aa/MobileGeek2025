using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatedStatePiker : Piker {
    public event Action<CreateStateTypes> CurrentStateChanged;

    [SerializeField] private CharacterCreateStateView _stateViewPrefab;
    [SerializeField] private RectTransform _viewParent;

    private List<CharacterCreateStateView> _views;

    private CreateStateConfigs _configs;
    private CreateStateConfig _currentConfig;

    private CharacterCreateStateView _currentView;

    public void SetConfig(CreateStateConfigs configs) {
        _configs = configs;
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true && _views == null)
            CreateViews();
    }

    private void CreateViews() {
        if (_configs == null || _configs.Configs.Count == 0)
            return;

        _views = new List<CharacterCreateStateView>();
        foreach (CreateStateConfig iConfig in _configs.Configs) {
            CharacterCreateStateView view = Instantiate(_stateViewPrefab, _viewParent);
            view.Init(iConfig);
            view.CharacrerCreateStateViewSelected += OnCharacterCreateStateViewSelected;

            _views.Add(view);
        }
    }

    private void OnCharacterCreateStateViewSelected(CharacterCreateStateView item) {
        if (_currentView != null && _currentView.Equals(item) != true) {
            _currentView.Activate(false);
        }

        _currentView = item;
        _currentConfig = _configs.GetConfigByType(_currentView.Config.Type);

        CurrentStateChanged?.Invoke(_currentView.Config.Type);
    }
}

