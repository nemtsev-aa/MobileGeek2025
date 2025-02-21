using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BasicParameterViewConfigs), menuName = "Configs/" + nameof(BasicParameterViewConfigs))]
public class BasicParameterViewConfigs : ScriptableObject {
    [field: SerializeField] public List<BasicParameterViewConfig> Configs { get; private set; }

    public BasicParameterViewConfig GetConfigByType(BasicParameterTypes type) {
        return Configs.FirstOrDefault(t => t.Type == type);
    }
}
