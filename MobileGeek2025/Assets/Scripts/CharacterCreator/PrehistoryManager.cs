using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrehistoryManager : CustomizationManager {
    public event Action<PrehistoryTypes> PrehistoryIsSet;

    [SerializeField] private PrehistoryConfigs _configs;
    [SerializeField] private PrehistoryPiker _prehistoryPiker;
    [SerializeField] private AudioSource _audioSource;
    [Space(5)]
    [SerializeField] private DeviceConfigs _deviceConfigs;
    [SerializeField] private Transform _devicesParent;

    private List<Device> _devices;
    private Device _currentDevice;

    private PrehistoryConfig CurrentConfig => _prehistoryPiker.CurrentConfig;

    public override void Init(BodyPartSelectionManager selectionManager, Piker piker) {
        base.Init(selectionManager, piker);

        _prehistoryPiker = (PrehistoryPiker)piker;
        _prehistoryPiker.SetConfig(_configs);

        CreateDevices();
    }

    public override void AddListeners() {
        base.AddListeners();

        _prehistoryPiker.CurrentPrehistoryChanged += SetPrehistory;
        _prehistoryPiker.VoiceStatusChanged += OnVoiceStatusChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _prehistoryPiker.CurrentPrehistoryChanged -= SetPrehistory;
        _prehistoryPiker.VoiceStatusChanged += OnVoiceStatusChanged;
    }

    public void SetPrehistory(PrehistoryTypes type) {
        ShowDeviceBytType(type);

        PrehistoryIsSet?.Invoke(type);
    }

    private void OnVoiceStatusChanged(bool status) {

        if (status == false) {
            _audioSource.Stop();
            return;
        }

        GenderTypes gender = CharacterManager.Instance.Character.Gender;
        DescriptionsConfig description = CurrentConfig.GetDescriptionsConfigByGenderType(gender);

        _audioSource.PlayOneShot(description.Voice);
    }

    private void CreateDevices() {
        if (_deviceConfigs.Configs.Count == 0)
            return;

        _devices = new List<Device>();
        foreach (var iConfig in _deviceConfigs.Configs) {
            Device newDevice = Instantiate(iConfig.Prefab, _devicesParent);
            newDevice.Init(iConfig.Type);

            _devices.Add(newDevice);
        }
    }

    private void ShowDeviceBytType(PrehistoryTypes type) {
        if (_currentDevice != null) { 
            _currentDevice.Show(false);
        }

        Device device = _devices.FirstOrDefault(d => d.PrehistoryTypes == type);
        if (device != null) {
            _currentDevice = device;
            _currentDevice.Show(true);
        }
    }
}
