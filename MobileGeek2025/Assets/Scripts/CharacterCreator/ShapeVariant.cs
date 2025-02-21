using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ShapeVariant : MonoBehaviour {
    [field: SerializeField] public ShapeTypes Type { get; private set; }
    [field: SerializeField] public List<MeshRenderer> MeshRenderers { get; private set; }

    [ContextMenu(nameof(GetMeshRenderers))]
    public void GetMeshRenderers() {
        MeshRenderers = new List<MeshRenderer>();

        if (transform.childCount == 0) {
            MeshRenderers.Add(transform.GetComponent<MeshRenderer>());
            return;
        }

        MeshRenderers.Add(transform.GetComponent<MeshRenderer>());
        for (int i = 0; i < transform.childCount; i++) {
            MeshRenderer iMesh = transform.GetChild(i).GetComponent<MeshRenderer>();
            MeshRenderers.Add(iMesh);
        }
    }
}
