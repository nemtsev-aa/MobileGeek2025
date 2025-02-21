using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName =nameof(PrehistoryConfigs), menuName = "Configs/" + nameof(PrehistoryConfigs))]
public class PrehistoryConfigs : ScriptableObject {
    [field: SerializeField] public List<PrehistoryConfig> Configs { get; private set; }

    public PrehistoryConfig GetConfigByType(PrehistoryTypes type) {
        return Configs.FirstOrDefault(t => t.Type == type);
    }
}

