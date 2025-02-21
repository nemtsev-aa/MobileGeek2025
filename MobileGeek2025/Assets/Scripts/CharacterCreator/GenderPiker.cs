using System;
using UnityEngine;
using UnityEngine.UI;


public class GenderPiker : Piker {
    public event Action<GenderTypes> GenderTypesChanged;
    
    [SerializeField] private Button _switcher1;
    [SerializeField] private Button _switcher2;

    public override void Init() {
        base.Init();
    }

    public override void Show(bool status) {
        base.Show(status);

    }

    public override void AddListeners() {
        base.AddListeners();

        _switcher1.onClick.AddListener(Type1SelectorButtonClick);
        _switcher2.onClick.AddListener(Type2SelectorButtonClick);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _switcher1.onClick.RemoveListener(Type1SelectorButtonClick);
        _switcher2.onClick.RemoveListener(Type2SelectorButtonClick);
    }

    private void Type1SelectorButtonClick() => GenderTypesChanged?.Invoke(GenderTypes.Male);
    private void Type2SelectorButtonClick() => GenderTypesChanged?.Invoke(GenderTypes.Female);

}
