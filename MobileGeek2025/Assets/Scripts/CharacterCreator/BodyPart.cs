using System;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour {
    private ShapeTypes _currentShape;
    private Color _currentColor;

    [field: SerializeField] public BodyPartTypes Type { get; private set;}
    [field: SerializeField] public Transform Transform { get; private set; }
    [field: SerializeField] public List<ShapeVariant> ShapeVariants { get; private set; }

    public List<MeshRenderer> MeshRenderers { 
        get {
            List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

            foreach (ShapeVariant iVariant in ShapeVariants) {
                foreach (MeshRenderer iRendered in iVariant.MeshRenderers) {
                    meshRenderers.Add(iRendered);
                }
            }

            return meshRenderers;
        }
    }

    public void SetColor(Color color) {
        foreach (MeshRenderer renderer in MeshRenderers) {
            renderer.materials[0].color = color;
            Material material = new Material(renderer.materials[0]);

            renderer.materials = new Material[1] { material };
        }

        _currentColor = color;
    }

    public void SetShapeType(ShapeTypes type) {
        foreach (ShapeVariant iVariant in ShapeVariants) {
            iVariant.gameObject.SetActive(iVariant.Type == type);     
        }

        _currentShape = type;
    }

    public BodyPartDescription GetDescription() {
        return new BodyPartDescription(Type, _currentShape, _currentColor);
    }
}
