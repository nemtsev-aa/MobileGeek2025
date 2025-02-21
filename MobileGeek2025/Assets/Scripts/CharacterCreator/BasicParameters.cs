using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class BasicParameters {
    public BasicParameters(List<BasicParameter> parameters) {
        Parameters = parameters;
    }

    [field: SerializeField] public List<BasicParameter> Parameters { get; private set; }

    public void SetBasicParameterValueByType(BasicParameterTypes type, int value) {
        var parameter = GetBasicParameterByType(type);
        parameter.SetValue(value);
    }

    public BasicParameter GetBasicParameterByType(BasicParameterTypes type) {
        return Parameters.FirstOrDefault(p => p.Type == type);
    }
}
