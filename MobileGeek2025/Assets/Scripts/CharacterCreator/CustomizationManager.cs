using System;
using UnityEngine;

public abstract class CustomizationManager : MonoBehaviour {
    protected BodyPartSelectionManager SelectionManager { get; private set; }
    protected BodyPart CurrentBodyPart { get; private set; }

    public virtual void Init(BodyPartSelectionManager selectionManager, Piker piker) {
        SelectionManager = selectionManager;
    }

    public virtual void Activate(bool status) {
        if (status) 
            AddListeners();
        else
            RemoveListeners();
    }

    public virtual void AddListeners() {
        SelectionManager.BodyPartSelected += OnBodyPartSelected;
    }

    public virtual void RemoveListeners() {
        SelectionManager.BodyPartSelected -= OnBodyPartSelected;
    }

    private void OnBodyPartSelected(BodyPart part) {
        CurrentBodyPart = part;
    }

    public virtual void Dispose() {
        RemoveListeners();
    }
}
