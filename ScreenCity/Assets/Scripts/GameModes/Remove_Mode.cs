using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove_Mode : Game_Mode {
    private GameManager manager;

    public Remove_Mode(GameManager _manager) {
        manager = _manager;
        MODE = GameManager.GameModes.Remove;
    }

    public override void OnMouseClick(int buttonIndex) {
        if (Input.GetMouseButtonDown(buttonIndex)) {
            RaycastHit hit;
            if (CursorRaycast(out hit)) {
                GameObject.Destroy(hit.transform.gameObject);
            }
        }
    }
    public override void OnCursorRaycast() {
    }
    public override bool CursorRaycast(out RaycastHit hit, float maxDistance = 100) {
        if (base.CursorRaycast(out hit, maxDistance)) {

            if (hit.transform.tag == GameManager.BUILDING_TAG_NAME) { // Cubes
                return true;
            } else if (hit.transform.parent != null && hit.transform.parent.tag == GameManager.BUILDING_TAG_NAME) { // Screens
                return true;
            }
        }
        return false;
    }
}
