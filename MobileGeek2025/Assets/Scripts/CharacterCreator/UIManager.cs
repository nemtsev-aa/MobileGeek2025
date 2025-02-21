using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour, IDisposable {
    public event Action<Character> CharacterShared;
    public event Action<Character> CharacterModify;

    [SerializeField] private List<UIPanel> _panels;

    private MenuCategoryPanel _menuCategoryPanel;
    private GalleryPanel _galleryPanel;
    private CreatePanel _createPanel;
    private AboutPanel _aboutPanel;
    private SaveManager _saveManager;

    private UIPanel _currentPanel;

    public CreatePanel CreatePanel => _createPanel;

    public void Init(SaveManager saveManager) {
        _saveManager = saveManager;

        InitPanels();
        AddListeners();

        SwichPanel(_menuCategoryPanel);
    }

    public T GetPanelByType<T>() where T : UIPanel {
        return (T)_panels.FirstOrDefault(panel => panel is T);
    }

    public void SwichPanel(UIPanel panel) {
        if (_currentPanel != null)
            _currentPanel.Show(false);
        
        _currentPanel = panel;
        _currentPanel.Show(true);
    }

    private void InitPanels() {
        if (_panels.Count == 0)
            return;

        _menuCategoryPanel = GetPanelByType<MenuCategoryPanel>();
        _menuCategoryPanel.Init();

        _galleryPanel = GetPanelByType<GalleryPanel>();

        if (_saveManager.GetCharacterList(out List<Character> list) == true) {
            _galleryPanel.Init(list);
        }

        _createPanel = GetPanelByType<CreatePanel>();
        _createPanel.Init();

        _aboutPanel = GetPanelByType<AboutPanel>();
        _aboutPanel.Init();
    }

    private void AddListeners() {
        _menuCategoryPanel.GameModesSelected += OnGameModesSelected;
        _menuCategoryPanel.GallerySelected += OnGallerySelected;
        _menuCategoryPanel.AboutDialogSelected += OnAboutDialogSelected;
        _menuCategoryPanel.QuitButtonSelected += OnQuitButtonSelected;

        _galleryPanel.BackToMainClicked += OnBackToMainClicked;
        _galleryPanel.ModifyCharacter += OnModifyCharacter;
        _galleryPanel.ShareCharacter += OnShareCharacter;

        _createPanel.BackToMainClicked += OnBackToMainClicked;
        _aboutPanel.BackToMainClicked += OnBackToMainClicked;
    }

    private void RemoveListeners() {
        _menuCategoryPanel.GameModesSelected -= OnGameModesSelected;
        _menuCategoryPanel.AboutDialogSelected -= OnAboutDialogSelected;
        _menuCategoryPanel.GallerySelected -= OnGallerySelected;
        _menuCategoryPanel.QuitButtonSelected -= OnQuitButtonSelected;

        _galleryPanel.BackToMainClicked -= OnBackToMainClicked;
        _galleryPanel.ModifyCharacter -= OnModifyCharacter;
        _galleryPanel.ShareCharacter -= OnShareCharacter;

        _createPanel.BackToMainClicked -= OnBackToMainClicked;
        _aboutPanel.BackToMainClicked -= OnBackToMainClicked;
    }


    #region MenuCategory Panel Events

    private void OnGameModesSelected() =>
        SwichPanel(_createPanel);


    private void OnGallerySelected() {
        if (_saveManager.GetCharacterList(out List<Character> list) == true) {
            _galleryPanel.ShowCharacters(list);
        }
        
        SwichPanel(_galleryPanel);
    }

    private void OnAboutDialogSelected() =>
        SwichPanel(_aboutPanel);

    private void OnQuitButtonSelected() {
        Application.Quit();
    }

    #endregion

    #region Gallery Panel Events

    private void OnBackToMainClicked() =>
        SwichPanel(_menuCategoryPanel);

    private void OnModifyCharacter(Character character) {
        Debug.Log($"OnModifyCharacter: {character}");

        SwichPanel(_createPanel);
        CharacterModify?.Invoke(character);
    }

    private void OnShareCharacter(Character character) {
        Debug.Log($"OnShareCharacter: {character.Name}");

        CharacterShared?.Invoke(character);
    }

    #endregion

    #region Create Panel Events

    private void OnSaveCurrentCharacter() {
        
    }

    #endregion
    
    public void Dispose() {
        RemoveListeners();
    }
}
