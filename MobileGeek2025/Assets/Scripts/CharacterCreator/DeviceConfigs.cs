using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(DeviceConfigs), menuName = "Configs/" + nameof(DeviceConfigs))]
public class DeviceConfigs : ScriptableObject {
    [field: SerializeField] public List<DeviceConfig> Configs { get; private set; }

    public DeviceConfig GetConfigByType(PrehistoryTypes type) {
        return Configs.FirstOrDefault(c => c.Type == type);
    }
}
