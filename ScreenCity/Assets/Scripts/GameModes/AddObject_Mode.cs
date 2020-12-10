using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObject_Mode : Game_Mode {

    private readonly GameManager manager;
    private readonly BuildingObject buildingObject;
    private readonly Transform parent;
    public BuildingObject previewObject;

    public AddObject_Mode(GameManager _manager, BuildingObject _buildingObject, Transform _parent) {
        MODE = GameManager.GameModes.Add_Object;

        manager = _manager;
        buildingObject = _buildingObject;
        parent = _parent;
        buildingObject.parent = parent;
        previewObject = _buildingObject.GetPreviewObject();
    }

    public override void OnMouseClick() {
        RaycastHit hit;
        if (CursorRaycast(out hit) && Input.GetMouseButtonDown(0)) {
            buildingObject.InstantiateFromPreview(previewObject.gameObject);
        }
    }

    /*protected virtual void SetPreviewObjectPosition(RaycastHit hit) {
        previewObject.transform.position = buildingObject.PositionOnSurface(hit);
        previewObject.transform.rotation = buildingObject.RotationOnSurface(hit);
    }*/

    public override void OnCursorRaycast() {
        RaycastHit hit;
        if (CursorRaycast(out hit)) {
            previewObject.SetPreviewPosition(hit);
            previewObject.gameObject.SetActive(true);
        } else {
            previewObject.gameObject.SetActive(false);
        }
    }
    public override bool CursorRaycast(out RaycastHit hit, float maxDistance = 100) {
        if (base.CursorRaycast(out hit, maxDistance)) {
            if (hit.transform.name == GameManager.PLAN_NAME || hit.transform.tag == GameManager.BUILDING_TAG_NAME) {
                return true;
            }
        }
        return false;
    }
}