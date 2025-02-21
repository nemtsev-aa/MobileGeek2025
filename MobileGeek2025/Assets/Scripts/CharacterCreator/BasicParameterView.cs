using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasicParameterView : UICompanent {
    public event Action<BasicParameterTypes, int> CurrentValueChanged;
    
    private BasicParameterViewConfig _config;

    public BasicParameterTypes Type => _config.Type;

    [field: SerializeField] public Slider Slider { get; private set; }
    [field: SerializeField] public TextMeshProUGUI HeaderText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ValueText { get; private set; }
    [field: SerializeField] public Image Icon { get; private set; }

    public void Init(BasicParameterViewConfig config) {
        _config = config;

        AddListeners();
        —onfigure—omponents();
    }

    public void SetSliderParameter(int max, int min, int current) {
        Slider.maxValue = max;
        Slider.minValue = min;
        Slider.value = current;
    }

    public void ShowValue(int value) {
        ValueText.text = $"{value}";
        Slider.value = value;
    } 

    public override void AddListeners() {
        base.AddListeners();

        Slider.onValueChanged.AddListener(SliderValueChanged);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        Slider.onValueChanged.RemoveListener(SliderValueChanged);
    }

    private void SliderValueChanged(float value) {
        int currentValue = (int)value;
        ShowValue(currentValue);

        CurrentValueChanged?.Invoke(Type, currentValue);
    }

    private void —onfigure—omponents() {
        HeaderText.text = $"{_config.Name}";
        Icon.sprite = _config.Icon;
    }
}
