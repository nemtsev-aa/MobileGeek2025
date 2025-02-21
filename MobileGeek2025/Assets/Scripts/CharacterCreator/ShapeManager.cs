using System;
using System.Collections.Generic;

public class ShapeManager : CustomizationManager {
    public event Action<BodyPartTypes, ShapeTypes> ShapeIsSet;

    private ShapePiker _shapePiker;

    public IReadOnlyList<BodyPart> BodyPart => SelectionManager.BodyParts;

    public override void Init(BodyPartSelectionManager selectionManager, Piker piker) {
        base.Init(selectionManager, piker);

        _shapePiker = (ShapePiker)piker;
    }

    public override void AddListeners() {
        base.AddListeners();

        _shapePiker.ShapeTypesChanged += SetShapeVariant;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _shapePiker.ShapeTypesChanged -= SetShapeVariant;
    }

    public void SetShapeVariant(ShapeTypes type) {
        if (CurrentBodyPart != null) {
            ShapeIsSet?.Invoke(CurrentBodyPart.Type, type);
            return;
        }

        ShapeIsSet?.Invoke(BodyPartTypes.All, type);
    }
}
