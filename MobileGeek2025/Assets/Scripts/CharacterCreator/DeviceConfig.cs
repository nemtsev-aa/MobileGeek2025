using System;
using UnityEngine;

[Serializable]
public class DeviceConfig {
    [field: SerializeField] public PrehistoryTypes Type { get; private set; }
    [field: SerializeField] public Device Prefab { get; private set; }
}
