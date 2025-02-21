using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CreateStateConfigs), menuName = "Configs/" + nameof(CreateStateConfigs))]
public class CreateStateConfigs : ScriptableObject {
    [field: SerializeField ] public List<CreateStateConfig> Configs { get; private set; }

    public CreateStateConfig GetConfigByType(CreateStateTypes type) { 
        return Configs.FirstOrDefault(c => c.Type ==  type);
    }
}

