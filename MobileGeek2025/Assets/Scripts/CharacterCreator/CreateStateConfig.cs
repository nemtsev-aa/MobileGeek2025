using System;
using UnityEngine;

[Serializable]
public class CreateStateConfig {
    [field: SerializeField] public CreateStateTypes Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

}

