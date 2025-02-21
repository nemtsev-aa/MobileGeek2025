using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BasicParametersPiker : Piker {
    public event Action<BasicParameterTypes, int> BasicParameterValueChanged;

    [SerializeField] private BasicParameterView _viewPrefab;
    [SerializeField] private RectTransform _viewParent;

    private List<BasicParameterView> _views;

    private BasicParameterViewConfigs _configs;
    private BasicParameters _basicParameters;
    private BasicParameterView _currentView;

    public void SetConfig(BasicParameterViewConfigs configs, BasicParameters basicParameters) {
        _configs = configs;
        _basicParameters = basicParameters;
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true && _views == null) {
            CreateViews();
            SetCurrentBasicParameterValue();
        }
    }

    public void SetCurrentBasicParameterValue() {
        if (_views == null || _views.Count == 0)
            return;

        foreach (BasicParameter iParameter in _basicParameters.Parameters) {
            GetViewByType(iParameter.Type).SetSliderParameter(iParameter.Max, iParameter.Min, iParameter.Value);
            GetViewByType(iParameter.Type).ShowValue(iParameter.Value);
        }
    }

    private void CreateViews() {
        if (_configs == null || _configs.Configs.Count == 0)
            return;

        _views = new List<BasicParameterView>();
        foreach (BasicParameterViewConfig iConfig in _configs.Configs) {
            BasicParameterView view = Instantiate(_viewPrefab, _viewParent);
            view.Init(iConfig);
            view.CurrentValueChanged += OnCurrentValueChanged;

            _views.Add(view);
        }
    }

    private BasicParameterView GetViewByType(BasicParameterTypes type) {
        return _views.FirstOrDefault(v => v.Type == type);
    }

    private void OnCurrentValueChanged(BasicParameterTypes type, int value) =>
        BasicParameterValueChanged?.Invoke(type, value);
    
}


