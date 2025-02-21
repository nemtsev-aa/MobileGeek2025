using System;
using UnityEngine;

[Serializable]
public class Character {
    public Character() {
    }

    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public GenderTypes Gender { get; private set; }
    [field: SerializeField] public BodyDescription BodyDescription { get; private set; }
    [field: SerializeField] public BasicParameters BasicParameters { get; private set; }
    [field: SerializeField] public PrehistoryTypes Prehistory { get; private set; }

    public void SetName(string name) {
        Name = name;
    }

    public void SetGender(GenderTypes gender) {
        Gender = gender;
    }

    public void SetBasicParameters(BasicParameters parameters) {
        BasicParameters = parameters;
    }

    public void SetPrehistory(PrehistoryTypes prehistory) {
        Prehistory = prehistory;
    }

    public void SetBodyDescription(BodyDescription bodyDescription) {
        BodyDescription = bodyDescription;
    }

    public void SetBodyPartDescription(BodyPartDescription partDescription) {
        BodyDescription.SetBodyPartDescription(partDescription);
    }
}
