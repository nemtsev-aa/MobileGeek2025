using System;
using UnityEngine;
using UnityEngine.UI;

public class ShapePiker : Piker {
    public event Action<ShapeTypes> ShapeTypesChanged;

    [SerializeField] private Button _switcher1;
    [SerializeField] private Button _switcher2;
    [SerializeField] private Button _switcher3;

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
        _switcher3.onClick.AddListener(Type3SelectorButtonClick);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _switcher1.onClick.RemoveListener(Type1SelectorButtonClick);
        _switcher2.onClick.RemoveListener(Type2SelectorButtonClick);
        _switcher3.onClick.RemoveListener(Type3SelectorButtonClick);
    }


    private void Type1SelectorButtonClick() => ShapeTypesChanged?.Invoke(ShapeTypes.Cube);
    private void Type2SelectorButtonClick() => ShapeTypesChanged?.Invoke(ShapeTypes.Cylinder);
    private void Type3SelectorButtonClick() => ShapeTypesChanged?.Invoke(ShapeTypes.Capsule);

}
