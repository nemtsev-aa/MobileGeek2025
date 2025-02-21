using System;
using UnityEngine;

public class CharacterCustomManager : MonoBehaviour, IDisposable {

    [Space(10), Header("CustomizationManagers")]
    [SerializeField] private GenderManager _genderManager;
    [SerializeField] private ShapeManager _shapeManager;
    [SerializeField] private ColorManager _colorManager;
    [SerializeField] private BasicParametersManager _basicParametersManager;
    [SerializeField] private PrehistoryManager _prehistoryManager;
    [SerializeField] private SaveManager _saveManager;

    [Space(10), Header("OtherManagers")]
    [SerializeField] private BodyPartManager _bodyPartManager;
    [SerializeField] private BodyPartSelectionManager _selectionManager;

    private UIManager _uiManager;
    private CustomizationManager _currentManager;

    public CharacterManager CharacterManager { get; private set; }
    public SaveManager SaveManager => _saveManager;
    public Character Character => CharacterManager.Character;

    private CreatePanel _createPanel => _uiManager.CreatePanel;
    private GenderPiker _genderPiker => _createPanel.GetPikerByType<GenderPiker>();
    private ShapePiker _shapePiker => _createPanel.GetPikerByType<ShapePiker>();
    private ColorPiker _colorPiker => _createPanel.GetPikerByType<ColorPiker>();
    private BasicParametersPiker _basicParametersPiker => _createPanel.GetPikerByType<BasicParametersPiker>();
    private PrehistoryPiker _prehistoryPiker => _createPanel.GetPikerByType<PrehistoryPiker>();
    private SavePiker _savePiker => _createPanel.GetPikerByType<SavePiker>();
    private BodyPart _currentBodyPart => _selectionManager.CurrentBodyPart;

    public void Init(CharacterManager character, UIManager uiManager) {
        CharacterManager = character;
        _uiManager = uiManager;

        _selectionManager.Init(_bodyPartManager);
        _genderManager.Init(_selectionManager, _genderPiker);
        _shapeManager.Init(_selectionManager, _shapePiker);
        _colorManager.Init(_selectionManager, _colorPiker);
        _basicParametersManager.Init(_selectionManager, _basicParametersPiker);
        _prehistoryManager.Init(_selectionManager, _prehistoryPiker);
        _saveManager.Init(_selectionManager, _savePiker);

        AddListeners();
        ShowDefaultBodyDescription();
    }

    private void AddListeners() {
        _createPanel.CurrentPikerChanged += OnCurrentPikerChanged;

        _genderManager.GenderIsSet += OnGenderIsSet;
        _shapeManager.ShapeIsSet += OnShapeIsSet;
        _colorManager.ColorIsSet += OnColorIsSet;
        _basicParametersManager.BasicParametersChanged += OnBasicParametersChanged;
        _prehistoryManager.PrehistoryIsSet += OnPrehistoryIsSet;
        _saveManager.IsSaved += OnIsSaved;

        _uiManager.CharacterModify += OnCharacterModify;
        _uiManager.CharacterShared += OnCharacterShared;
    }

    private void RemoveListeners() {
        _createPanel.CurrentPikerChanged -= OnCurrentPikerChanged;

        _genderManager.GenderIsSet -= OnGenderIsSet;
        _shapeManager.ShapeIsSet -= OnShapeIsSet;
        _colorManager.ColorIsSet -= OnColorIsSet;
        _basicParametersManager.BasicParametersChanged -= OnBasicParametersChanged;
        _prehistoryManager.PrehistoryIsSet -= OnPrehistoryIsSet;
        _saveManager.IsSaved -= OnIsSaved;

        _uiManager.CharacterModify -= OnCharacterModify;
        _uiManager.CharacterShared -= OnCharacterShared;
    }

    private void OnCurrentPikerChanged(CreateStateTypes type) {
        if (_currentManager != null && _currentManager.Equals(type) == false)
            _currentManager.Activate(false);

        switch (type) {
            case CreateStateTypes.Gender:
                _currentManager = _genderManager;
                break;

            case CreateStateTypes.Shape:
                _currentManager = _shapeManager;
                _bodyPartManager.gameObject.SetActive(true);
                //Character.SetBodyDescription(_bodyPartManager.GetBodyDescription());

                break;

            case CreateStateTypes.Color:
                _currentManager = _colorManager;
                break;

            case CreateStateTypes.BasicParameters:
                _currentManager = _basicParametersManager;
                break;

            case CreateStateTypes.Prehistory:
                _currentManager = _prehistoryManager;
                break;

            case CreateStateTypes.Save:
                _currentManager = _saveManager;
                break;

            default:
                break;
        }

        _currentManager.Activate(true);
    }

    private void ShowDefaultBodyDescription() {
        _shapeManager.SetShapeVariant(Character.BodyDescription.All.Shape);
        _colorManager.SetColor(Character.BodyDescription.All.Color);
        _bodyPartManager.gameObject.SetActive(false);
    }

    private void OnGenderIsSet(GenderTypes gender) {
        //Debug.Log($"Gender: {gender}");

        Transform head = _bodyPartManager.GetBodyPartByType(BodyPartTypes.Head).transform;
        Transform body = _bodyPartManager.GetBodyPartByType(BodyPartTypes.Body).transform;

        if (gender == GenderTypes.Female) {
            head.localScale = Vector3.one * 0.9f;
            body.localEulerAngles = Vector3.back * 180f;
        }

        if (gender == GenderTypes.Male) {
            head.localScale = Vector3.one;
            body.localEulerAngles = Vector3.zero;
        }

        Character.SetGender(gender);
    }

    private void OnShapeIsSet(BodyPartTypes bodyPartType, ShapeTypes shape) {
        //Debug.Log($"CurrentBodyPart: {bodyPartType}, Color {shape}");

        if (bodyPartType == BodyPartTypes.All) {
            foreach (BodyPart iPart in _bodyPartManager.BodyParts) {
                iPart.SetShapeType(shape);
            }

            //Character.SetBodyDescription(_bodyPartManager.GetBodyDescription());
            return;
        }

        _currentBodyPart.SetShapeType(shape);


        Character.SetBodyPartDescription(_bodyPartManager.GetBodyPartDescriptionByType(_currentBodyPart.Type));
    }

    private void OnColorIsSet(BodyPartTypes bodyPartType, Color color) {
        //Debug.Log($"CurrentBodyPart: {bodyPartType}, Color {color}");

        if (bodyPartType == BodyPartTypes.All) {
            foreach (BodyPart iPart in _bodyPartManager.BodyParts) {
                iPart.SetColor(color);
            }

            //Character.SetBodyDescription(_bodyPartManager.GetBodyDescription());
            return;
        }

        _currentBodyPart.SetColor(color);
    }

    private void OnPrehistoryIsSet(PrehistoryTypes type) {
        Character.SetPrehistory(type);
    }

    private void OnBasicParametersChanged(BasicParameterTypes type, int value) {
        Character.BasicParameters.SetBasicParameterValueByType(type, value);
    }

    private void OnIsSaved() {
        Debug.Log($"CharacterCustomManager: {Character}");
        CharacterManager.SaveCharacter(Character);
    }

    private void OnCharacterModify(Character character) {
        CharacterManager.Character = character;

        _genderManager.SetGenderType(character.Gender);
        _bodyPartManager.SetBodyDescription(character.BodyDescription);
        _prehistoryManager.SetPrehistory(character.Prehistory);

        OnShapeIsSet(BodyPartTypes.All, character.BodyDescription.Head.Shape);
        OnColorIsSet(BodyPartTypes.All, character.BodyDescription.Head.Color);
    }

    private void OnCharacterShared(Character character) {
        CharacterManager.ShareCharacter(character);
    }

    public void Dispose() {
        RemoveListeners();
    }
}
