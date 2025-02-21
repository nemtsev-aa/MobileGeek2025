using System;
using UnityEngine;

[Serializable]
public class BodyPartDescription {
    public BodyPartDescription(BodyPartTypes type, ShapeTypes shape, Color color) {
        Type = type;
        Shape = shape;
        Color = color;
    }

    [field: SerializeField] public BodyPartTypes Type { get; private set; }
    [field: SerializeField] public ShapeTypes Shape { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
}
