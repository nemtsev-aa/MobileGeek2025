using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrehistoryPiker : Piker {
    public event Action<PrehistoryTypes> CurrentPrehistoryChanged;
    public event Action<bool> VoiceStatusChanged;

    [SerializeField] private PrehistorySwitcher _switcherPrefab;
    [SerializeField] private RectTransform _switcherParent;

    [SerializeField] private PrehistoryView _viewPrefab;
    [SerializeField] private RectTransform _viewParent;

    private PrehistoryConfigs _configs;

    private List<PrehistoryView> _views;
    private List<PrehistorySwitcher> _switchers;

    private PrehistoryView _currentView;
    private PrehistorySwitcher _currentSwitcher;

    public PrehistoryConfig CurrentConfig { get; private set; }

    public void SetConfig(PrehistoryConfigs configs) {
        _configs = configs;
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true && _views == null) {
            CreateSwitchers();
            CreateViews();
        }
            
    }

    private void CreateSwitchers() {
        if (_configs == null || _configs.Configs.Count == 0)
            return;

        _switchers = new List<PrehistorySwitcher>();
        foreach (PrehistoryConfig iConfig in _configs.Configs) {
            PrehistorySwitcher switcher = Instantiate(_switcherPrefab, _switcherParent);
            
            switcher.Init(iConfig);
            switcher.PrehistorySelected += OnPrehistoryViewSelected;

            _switchers.Add(switcher);
        }
    }

    private void CreateViews() {
        if (_configs == null || _configs.Configs.Count == 0)
            return;

        _views = new List<PrehistoryView>();
        foreach (PrehistoryConfig iConfig in _configs.Configs) {
            PrehistoryView view = Instantiate(_viewPrefab, _viewParent);
            
            view.Init(iConfig);
            view.VoiceStatusChanged += OnVoiceStatusChanged;
            view.Show(false);

            _views.Add(view);
        }
    }

    private void OnPrehistoryViewSelected(PrehistorySwitcher item) {
        if (_currentSwitcher != null && _currentSwitcher.Equals(item) != true) {
            _currentSwitcher.Activate(false);
        }
        _currentSwitcher = item;

        var newView = GetViewByType(item.Config.Type);
        if (_currentView != null && _currentView.Equals(newView) != true) {
            _currentView.Activate(false);
            _currentView.Show(false);
        }

        _currentView = newView;
        _currentView.Show(true);

        var currentPrehistoryType = _currentSwitcher.Config.Type;
        CurrentConfig = _configs.GetConfigByType(currentPrehistoryType);

        CurrentPrehistoryChanged?.Invoke(currentPrehistoryType);
    }

    private PrehistoryView GetViewByType(PrehistoryTypes type) {
        return _views.FirstOrDefault(v => v.Config.Type == type);
    }

    private void OnVoiceStatusChanged(bool status) => VoiceStatusChanged?.Invoke(status);
}


