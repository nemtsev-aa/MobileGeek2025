using System;
using UnityEngine;
using UnityEngine.UI;

public class AboutPanel : UIPanel {
    public event Action BackToMainClicked;
    [SerializeField] private ProjectAuthorConfigs _confifs;
    [SerializeField] private ProjectAuthorViews _authorViews;
    [SerializeField] private Button _backButton;

    public void Init() {
        _authorViews.Init(_confifs);
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true) {
            _authorViews.Show(true);
            AddListeners();
        }
        else
            RemoveListeners();
    }

    public override void AddListeners() {
        base.AddListeners();

        _backButton.onClick.AddListener(BackButtonClick);

    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _backButton.onClick.RemoveListener(BackButtonClick);
    }

    private void BackButtonClick() => BackToMainClicked?.Invoke();
}


