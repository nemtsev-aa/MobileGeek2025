using UnityEngine;
using System;

[Serializable]
public class DescriptionsConfig {
    [field: SerializeField] public GenderTypes Gender { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public AudioClip Voice { get; private set; }
}

