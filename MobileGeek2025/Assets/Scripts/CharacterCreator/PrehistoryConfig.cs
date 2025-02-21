using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class PrehistoryConfig {
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public PrehistoryTypes Type { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public List<DescriptionsConfig> Descriptions { get; private set; }

    public DescriptionsConfig GetDescriptionsConfigByGenderType(GenderTypes gender) {
        return Descriptions.FirstOrDefault(g => g.Gender == gender);
    }
}

