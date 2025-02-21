using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(DefaultBasicParametersConfig), menuName = "Configs/" + nameof(DefaultBasicParametersConfig))]
public class DefaultBasicParametersConfig : ScriptableObject {
    [field: SerializeField] public List<BasicParameter> Parameters { get; private set; }
}
