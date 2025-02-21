using System;
using UnityEngine;

public class GenderManager : CustomizationManager {
    public event Action<GenderTypes> GenderIsSet;

    [SerializeField] private ParticleSystem _maleAura;
    [SerializeField] private ParticleSystem _femaleAura;

    private GenderPiker _genderPiker;

    public GenderTypes GenderType { get; private set; }

    public override void Init(BodyPartSelectionManager selectionManager, Piker piker) {
        base.Init(selectionManager, piker);

        _genderPiker = (GenderPiker)piker;
    }

    public override void AddListeners() {
        base.AddListeners();

        _genderPiker.GenderTypesChanged += SetGenderType;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _genderPiker.GenderTypesChanged -= SetGenderType;
    }

    public void SetGenderType(GenderTypes type) {
        GenderType = type;

        _maleAura.gameObject.SetActive(type == GenderTypes.Male);
        _femaleAura.gameObject.SetActive(type == GenderTypes.Female);

        GenderIsSet?.Invoke(GenderType);
    }
}
