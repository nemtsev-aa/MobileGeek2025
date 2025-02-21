using System;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartSelectionManager : MonoBehaviour {
    public event Action<BodyPart> BodyPartSelected;

    [Space(10), Header("Outline Settings")]
    [SerializeField] private Color _outlineColor;
    [SerializeField] private float _outlineWidth;

    private BodyPartManager _bodyPartManager;
    public IReadOnlyList<BodyPart> BodyParts => _bodyPartManager.BodyParts;

    public BodyPart CurrentBodyPart { get; private set; }

    public void Init(BodyPartManager bodyPartManager) {
        _bodyPartManager = bodyPartManager;

        CreateOutline();
    }

    public void SetCurrentBodyPart(BodyPart bodyPart) {
        if (CurrentBodyPart != null)
            ShowOutline(false);

        CurrentBodyPart = bodyPart;
        ShowOutline(true);

        BodyPartSelected?.Invoke(CurrentBodyPart);
    }

    private void LateUpdate() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red);

                if (hit.collider.attachedRigidbody.TryGetComponent(out BodyPart bodyPart)) {
                    SetCurrentBodyPart(bodyPart);
                    return;
                }

                if (CurrentBodyPart != null)
                    ShowOutline(false);

            }

            CurrentBodyPart = null;
        }
    }

    private void CreateOutline() {
        foreach (BodyPart iPart in BodyParts) {
            foreach (MeshRenderer iRenderer in iPart.MeshRenderers) {
                iRenderer.gameObject.AddComponent<Outline>();

                Outline outline = iRenderer.gameObject.GetComponent<Outline>();
                outline.OutlineColor = _outlineColor;
                outline.OutlineWidth = _outlineWidth;

                outline.enabled = false;
            }
        }
    }

    private void ShowOutline(bool status) {
        Outline[] outlines = CurrentBodyPart.transform.GetComponentsInChildren<Outline>();

        if (outlines.Length >= 0) {
            foreach (Outline iOutline in outlines) {
                iOutline.enabled = status;
            }
        }
    }
}
