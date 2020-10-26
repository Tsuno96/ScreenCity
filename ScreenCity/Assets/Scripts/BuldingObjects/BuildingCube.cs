using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCube : BuildingObject {
    public override GameObject GetPreviewObject() {
        GameObject previewCube = GameObject.Instantiate(gameObject, parent);
        previewCube.layer = LayerMask.NameToLayer("Ignore Raycast");
        previewCube.name = "Preview Cube";

        SetTransparent(previewCube);

        return previewCube;
    }

    public override GameObject InstantiateFromPreview(GameObject preview) {
        GameObject newCube = GameObject.Instantiate(gameObject, preview.transform.position, Quaternion.identity, parent);
        newCube.transform.localScale = preview.transform.localScale;
        return newCube;
    }
}
