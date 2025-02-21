using System;
using UnityEngine;

[Serializable]
public class BasicParameter {
    public BasicParameter(BasicParameterTypes type, string name, int max, int min, int value) {
        Type = type;
        Name = name;
        Max = max;
        Min = min;
        Value = value;
    }

    [field: SerializeField] public BasicParameterTypes Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Max { get; private set; } = 5;
    [field: SerializeField] public int Min { get; private set; } = 1;
    [field: SerializeField] public int Value { get; private set; }

    public void SetType(BasicParameterTypes type) {
        Type = type;
    }

    public void SetValue(int value) {
        Value = value;
    }
}
