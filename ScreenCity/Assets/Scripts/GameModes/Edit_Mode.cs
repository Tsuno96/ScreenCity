using Assets.Script.Create_Cube;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edit_Mode : Game_Mode {
    private GameManager manager;

    public Edit_Mode(GameManager _manager) {
        manager = _manager;
        MODE = GameManager.GameModes.Edit;
    }

    public override void OnMouseClick(int buttonIndex) {
        if (Input.GetMouseButtonDown(buttonIndex)) {
            RaycastHit hit;
            if (CursorRaycast(out hit)) {
                CubeController cc = hit.transform.gameObject.GetComponent<CubeController>();
                Face f = cc.GetFaceWithNormal(hit.normal);
                cc.ChangeScale(f, 0.5f);
            }
        }
    }
    public override void OnCursorRaycast() {}
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
