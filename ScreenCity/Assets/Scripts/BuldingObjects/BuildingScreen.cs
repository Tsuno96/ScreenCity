using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScreen : BuildingObject {
    public override GameObject GetPreviewObject() {
        GameObject previewScreen = GameObject.Instantiate(gameObject, parent);
        GameObject screenFace = previewScreen.transform.GetChild(0).gameObject;
        GameObject screenBack = previewScreen.transform.GetChild(1).gameObject;
        
        screenFace.layer = LayerMask.NameToLayer("Ignore Raycast");
        screenBack.layer = LayerMask.NameToLayer("Ignore Raycast");

        SetTransparent(screenFace);
        SetTransparent(screenBack);

        return previewScreen;
    }

    public override GameObject InstantiateFromPreview(GameObject preview) {
        GameObject newScreen = GameObject.Instantiate(gameObject, preview.transform.position, Quaternion.identity, parent);
        newScreen.transform.localScale = preview.transform.localScale;
        newScreen.transform.rotation = preview.transform.rotation;
        return newScreen;
    }

    public override Vector3 PositionOnSurface(RaycastHit hit) {
        return hit.point + hit.normal * GameManager.EPSILON;
    }
    public override Quaternion RotationOnSurface(RaycastHit hit) {
        return Quaternion.Euler(hit.normal.y * 90, hit.normal.x * -90, hit.normal.z * 90);
    }
}
