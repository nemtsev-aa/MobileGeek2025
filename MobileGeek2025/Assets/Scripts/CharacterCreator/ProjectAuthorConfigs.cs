using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ProjectAuthorConfigs), menuName = "Configs/" + nameof(ProjectAuthorConfigs))]
public class ProjectAuthorConfigs: ScriptableObject {
    [field: SerializeField] public List<ProjectAuthorConfig> Configs { get; private set; }

    public ProjectAuthorConfig GetConfigByType(ProjectAuthorTypes type) {
        return Configs.FirstOrDefault(c => c.Type == type);
    }
}

