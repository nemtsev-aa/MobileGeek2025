using System;
using UnityEngine;

[Serializable]
public class BasicParameterViewConfig : UICompanentConfig {
    [field: SerializeField] public BasicParameterTypes Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    public override void OnValidate() {
        if (Icon == null)
            throw new ArgumentNullException($"{Icon}: is null");
    }
}
