using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BodyPartManager : MonoBehaviour {
    [field: SerializeField] public List<BodyPart> BodyParts { get; private set; }

    public BodyPart GetBodyPartByType(BodyPartTypes type) {
        return BodyParts.FirstOrDefault(b => b.Type == type);
    }

    public BodyDescription GetBodyDescription() {
        return new BodyDescription(
            GetBodyPartByType(BodyPartTypes.Head).GetDescription(),
            GetBodyPartByType(BodyPartTypes.Body).GetDescription(), 
            GetBodyPartByType(BodyPartTypes.Arms).GetDescription(), 
            GetBodyPartByType(BodyPartTypes.Legs).GetDescription());
    }

    public BodyPartDescription GetBodyPartDescriptionByType(BodyPartTypes type) {
        return GetBodyPartByType(type).GetDescription();
    }

    public void SetBodyDescription(BodyDescription bodyDescription) {
        foreach (var iPart in BodyParts) {
            var description = bodyDescription.GetBodyPartDescriptionByType(iPart.Type);
            
            iPart.SetShapeType(description.Shape);
            iPart.SetColor(description.Color);
        }
    }
}
