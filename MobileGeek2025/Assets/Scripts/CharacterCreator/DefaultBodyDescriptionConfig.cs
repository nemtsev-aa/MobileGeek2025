using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(DefaultBodyDescriptionConfig), menuName = "Configs/" + nameof(DefaultBodyDescriptionConfig))]
public class DefaultBodyDescriptionConfig : ScriptableObject {
    [field: SerializeField] public List<BodyPartDescription> PartDescriptions { get; private set; }

    public BodyPartDescription GetBodyPartDescriptionByType(BodyPartTypes type) {
        return PartDescriptions.FirstOrDefault(t => t.Type == type);
    }
}
