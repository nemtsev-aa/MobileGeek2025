using System;
using UnityEngine;

public class ColorManager : CustomizationManager {
    public event Action<BodyPartTypes, Color> ColorIsSet;

    private ColorPiker _colorPiker;

    public override void Init(BodyPartSelectionManager selectionManager, Piker piker) {
        base.Init(selectionManager, piker);

        _colorPiker = (ColorPiker)piker;
    }

    public override void AddListeners() {
        base.AddListeners();

        _colorPiker.ColorChanged += SetColor;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _colorPiker.ColorChanged -= SetColor;
    }

    public void SetColor(Color color) {
        if (CurrentBodyPart != null) {
            CurrentBodyPart.SetColor(color);
            ColorIsSet?.Invoke(CurrentBodyPart.Type, color);
            return;
        }

        ColorIsSet?.Invoke(BodyPartTypes.All, color);
    }
}
