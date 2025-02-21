using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrehistoryView : UICompanent {
    public event Action<bool> VoiceStatusChanged;

    [SerializeField] private TextMeshProUGUI _descriptionLabel;

    [SerializeField] private Button _voicePlay;
    [SerializeField] private Image _voiceImage;

    [SerializeField] private Sprite _playSprite;
    [SerializeField] private Sprite _stopSprite;

    public PrehistoryConfig Config { get; private set; }
    public bool IsPlay { get; private set; } = false;

    public void Init(PrehistoryConfig config) {
        Config = config;

        AddListeners();
        UpdateContent();
    }

    public override void AddListeners() {
        base.AddListeners();

        _voicePlay.onClick.AddListener(VoicePlayButtonClick);
    }

    public override void RemoveListeners() {
        base.AddListeners();

        _voicePlay.onClick.RemoveListener(VoicePlayButtonClick);
    }

    public override void UpdateContent() {
        _voiceImage.sprite = _playSprite;

        GenderTypes gender = CharacterManager.Instance.Character.Gender;
        DescriptionsConfig description = Config.GetDescriptionsConfigByGenderType(gender);

        _descriptionLabel.text = description.Description;
    }

    private void VoicePlayButtonClick() {
        IsPlay = !IsPlay;

        if (IsPlay == true) 
            _voiceImage.sprite = _stopSprite;
        else
            _voiceImage.sprite = _playSprite;

        VoiceStatusChanged?.Invoke(IsPlay);
    } 
}


