using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuCategoryPanel : UIPanel {
    public event Action GameModesSelected;
    public event Action GallerySelected;
    public event Action AboutDialogSelected;
    public event Action QuitButtonSelected;

    [SerializeField] private Button _gameModesButton;
    [SerializeField] private Button _galleryButton;
    [SerializeField] private Button _aboutButton;
    [SerializeField] private Button _quitButton;

    public void Init() {
        AddListeners();
    }

    public override void Show(bool value) {
        base.Show(value);

    }

    public override void AddListeners() {
        base.AddListeners();

        _gameModesButton.onClick.AddListener(GameModesButtonClick);
        _galleryButton.onClick.AddListener(GalleryButtonClick);
        _aboutButton.onClick.AddListener(AboutButtonClick);
        _quitButton.onClick.AddListener(QuitButtonClick);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _gameModesButton.onClick.RemoveListener(GameModesButtonClick);
        _galleryButton.onClick.RemoveListener(GalleryButtonClick);
        _aboutButton.onClick.RemoveListener(AboutButtonClick);
        _quitButton.onClick.RemoveListener(QuitButtonClick);
    }

    private void GameModesButtonClick() => GameModesSelected?.Invoke();

    private void GalleryButtonClick() => GallerySelected?.Invoke();

    private void AboutButtonClick() => AboutDialogSelected?.Invoke();

    private void QuitButtonClick() => QuitButtonSelected?.Invoke();

}
