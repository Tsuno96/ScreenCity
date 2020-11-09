using Assets.Script.Create_Cube;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edit_Mode : Game_Mode {
    private GameManager manager;

    private GameObject currentSelected;
    private Color originalMeshColor;

    public Edit_Mode(GameManager _manager) {
        manager = _manager;
        MODE = GameManager.GameModes.Edit;
    }

    public override void OnMouseClick() {
        RaycastHit hit;
        if (CursorRaycast(out hit) == false)
            return;
        CubeController cc = hit.transform.gameObject.GetComponent<CubeController>();
        Face f = cc.GetFaceWithNormal(hit.normal);
        if (Input.GetMouseButtonDown(0)) {
            cc.ChangeScale(f, 0.5f);
        } else if (Input.GetMouseButtonDown(1)) {
            cc.ChangeScale(f, -0.25f);
        }
    }

    private void UnFocusSelected() {
        if (currentSelected == null) return;
        currentSelected.GetComponent<MeshRenderer>().material.color = originalMeshColor;
        currentSelected = null;
    }

    private void FocusMesh(GameObject g) {
        MeshRenderer renderer = g.GetComponent<MeshRenderer>();
        originalMeshColor = renderer.material.color;
        currentSelected = g;

        Color hoverColor = new Color(1 - originalMeshColor.r, 1 - originalMeshColor.g, 1 - originalMeshColor.b);
        hoverColor = Color.Lerp(originalMeshColor, hoverColor, 0.33f);
        g.GetComponent<MeshRenderer>().material.color = hoverColor;
    }

    public override void OnCursorRaycast() {
        RaycastHit hit;
        if (CursorRaycast(out hit)) {
            UnFocusSelected();
            FocusMesh(hit.transform.gameObject);
        } else if (currentSelected != null) {
            UnFocusSelected();
        }
    }
    public override bool CursorRaycast(out RaycastHit hit, float maxDistance = 100) {
        if (base.CursorRaycast(out hit, maxDistance)) {
            CubeController cc = hit.transform.gameObject.GetComponent<CubeController>();
            if (cc != null && hit.transform.tag == GameManager.BUILDING_TAG_NAME) {
                return true;
            }
        }
        return false;
    }
}
