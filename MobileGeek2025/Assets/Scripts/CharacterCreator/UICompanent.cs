using UnityEngine;
using System;

[Serializable]
public abstract class UICompanent : MonoBehaviour, IDisposable {
    public virtual void Activate(bool status) {
        if (status == true)
            Select();
        else
            UpdateContent();
    }

    public virtual void Show(bool status) {
        gameObject.SetActive(status);
    }

    public virtual void Select() {
        
    }

    public virtual void UpdateContent() {
        
    }

    public virtual void AddListeners() {
       
    }

    public virtual void RemoveListeners() {
        
    }

    public virtual void Dispose() {
        RemoveListeners();
    }
}
