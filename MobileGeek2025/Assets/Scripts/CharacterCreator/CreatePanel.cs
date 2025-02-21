using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanel : UIPanel {
    public event Action BackToMainClicked;
    public event Action<CreateStateTypes> CurrentPikerChanged;
    public event Action SaveCurrentCharacter;

    [SerializeField] private Button _closeButton;
    [SerializeField] private CreateStateConfigs _configs;
    [SerializeField] private List<Piker> _pikers;

    private CreatedStatePiker _createdStatePiker;
    private GenderPiker _genderPiker;
    private ShapePiker _shapePiker;
    private ColorPiker _colorPiker;
    private BasicParametersPiker _basicParametersPiker;
    private PrehistoryPiker _prehistoryPiker;
    private SavePiker _savePiker;

    private Piker _currentPiker;

    public void Init() {
        _createdStatePiker = GetPikerByType<CreatedStatePiker>();
        _createdStatePiker.Init();
        _createdStatePiker.SetConfig(_configs);
        _createdStatePiker.CurrentStateChanged += OnCurrentStateChanged;
        _createdStatePiker.Show(true);

        _genderPiker = GetPikerByType<GenderPiker>();
        _genderPiker.Init();

        _shapePiker = GetPikerByType<ShapePiker>();
        _shapePiker.Init();

        _colorPiker = GetPikerByType<ColorPiker>();
        _colorPiker.Init();

        _basicParametersPiker = GetPikerByType<BasicParametersPiker>();
        _basicParametersPiker.Init();

        _prehistoryPiker = GetPikerByType<PrehistoryPiker>();
        _prehistoryPiker.Init();

        _savePiker = GetPikerByType<SavePiker>();
        _savePiker.Init();

        //OnCurrentStateChanged(CreateStateTypes.Gender);

        AddListeners();
        
    }

    public override void AddListeners() {
        base.AddListeners();

        _closeButton.onClick.AddListener(CloseButtonClick);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _closeButton.onClick.RemoveListener(CloseButtonClick);
    }

    private void CloseButtonClick() => BackToMainClicked?.Invoke();
    

    public T GetPikerByType<T>() where T : Piker {
        return (T)_pikers.FirstOrDefault(panel => panel is T);
    }

    private void OnCurrentStateChanged(CreateStateTypes type) {
        if (_currentPiker != null)
            _currentPiker.Show(false);

        switch (type) {
            case CreateStateTypes.Gender:
                _currentPiker = _genderPiker;
                break;

            case CreateStateTypes.Shape:
                _currentPiker = _shapePiker;
                break;

            case CreateStateTypes.Color:
                _currentPiker = _colorPiker;
                break;

            case CreateStateTypes.BasicParameters:
                _currentPiker = _basicParametersPiker;
                break;

            case CreateStateTypes.Prehistory:
                _currentPiker = _prehistoryPiker;
                break;

            case CreateStateTypes.Save:
                _currentPiker = _savePiker;
                break;

            default:
                break;
        }

        _currentPiker.Show(true);
        CurrentPikerChanged?.Invoke(type);
    }
}
