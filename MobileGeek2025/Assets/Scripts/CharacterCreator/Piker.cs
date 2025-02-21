using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Piker : MonoBehaviour, IDisposable {
    public event Action Reversed;
    public event Action Closed;

    [SerializeField] protected Button CloseButton;
    [SerializeField] protected Button BackButton;

    public virtual void Init() {
        AddListeners();
        Show(false);
    }

    public virtual void Show(bool status) {
        gameObject.SetActive(status);
    }

    public virtual void AddListeners() {
        if (CloseButton != null)
            CloseButton.onClick.AddListener(CloseButtonClick);

        if (BackButton != null)
            BackButton.onClick.AddListener(BackButtonClick);
    }

    public virtual void RemoveListeners() {
        if (CloseButton != null)
            CloseButton.onClick.RemoveListener(CloseButtonClick);

        if (BackButton != null)
            BackButton.onClick.RemoveListener(BackButtonClick);
    }

    private void BackButtonClick() => Closed?.Invoke();

    private void CloseButtonClick() => Reversed?.Invoke();

    public virtual void Dispose() {
        RemoveListeners();
    }
}
