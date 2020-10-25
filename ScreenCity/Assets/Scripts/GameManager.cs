using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject cube;
    public GameObject buildingsGameObject;

    public static readonly string PLAN_NAME = "Plan";
    public static readonly string BUILDING_TAG_NAME = "Building";

    public enum GameModes {
        Move,
        Add_Cube,
        Add_Screen,
        Remove
    }
    public Game_Mode mode;

    private void Start() {
        SetGameMode(GameModes.Move);
    }

    public void SetGameMode_Move() {
        SetGameMode(GameModes.Move);
    }
    public void SetGameMode_AddCube() {
        SetGameMode(GameModes.Add_Cube);
    }
    public void SetGameMode_AddScreen() {
        SetGameMode(GameModes.Add_Screen);
    }
    public void SetGameMode_Remove() {
        SetGameMode(GameModes.Remove);
    }

    public void SetGameMode(GameModes m) {
        if (m == GameModes.Add_Cube) {
            mode = new AddCube_Mode(this, cube, buildingsGameObject);
        }
        switch (m) {
            case GameModes.Add_Cube:
                mode = new AddCube_Mode(this, cube, buildingsGameObject);
                break;
            case GameModes.Add_Screen:
                mode = new AddScreen_Mode(this);
                break;
            case GameModes.Remove:
                mode = new Remove_Mode(this);
                break;
            default:
                mode = new Move_Mode(this);
                break;
        }
    }
    public Game_Mode GetGameMode() {
        return mode;
    }
}


public abstract class Game_Mode {

    private GameManager manager;

    public abstract void OnMouseClick(int buttonIndex);
    public abstract void OnCursorRaycast();
    public virtual bool CursorRaycast(out RaycastHit hit, float maxDistance = 100) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, maxDistance);
    }
}

public class Move_Mode : Game_Mode {

    private GameManager manager;

    public Move_Mode(GameManager _manager) {
        manager = _manager;
    }

    public override void OnMouseClick(int buttonIndex) {
    }
    public override void OnCursorRaycast() {
    }
}

public class Remove_Mode : Game_Mode {

    private GameManager manager;

    public Remove_Mode(GameManager _manager) {
        manager = _manager;
    }

    public override void OnMouseClick(int buttonIndex) {
        if (Input.GetMouseButtonDown(buttonIndex)) {
            RaycastHit hit;
            if(CursorRaycast(out hit)) {
                GameObject.Destroy(hit.transform.gameObject);
            }
        }
    }
    public override void OnCursorRaycast() {
    }
    public override bool CursorRaycast(out RaycastHit hit, float maxDistance = 100) {
        if (base.CursorRaycast(out hit, maxDistance)) {
            if (hit.transform.tag == GameManager.BUILDING_TAG_NAME) {
                return true;
            }
        }
        return false;
    }
}

public class AddScreen_Mode : Game_Mode {

    private GameManager manager;

    public AddScreen_Mode(GameManager _manager) {
        manager = _manager;
    }

    public override void OnMouseClick(int buttonIndex) {
    }
    public override void OnCursorRaycast() {
    }
}

public class AddCube_Mode : Game_Mode {

    private GameManager manager;
    private GameObject cube;
    private GameObject previewCube;
    private GameObject buildingsGameObject;

    public AddCube_Mode(GameManager _manager, GameObject _cube, GameObject _buildingsGameObject) {
        manager = _manager;
        cube = _cube;
        buildingsGameObject = _buildingsGameObject;
        previewCube = GameObject.Instantiate(_cube, _buildingsGameObject.transform);
        previewCube.layer = LayerMask.NameToLayer("Ignore Raycast");

        Color c = previewCube.GetComponent<MeshRenderer>().material.color;
        previewCube.GetComponent<MeshRenderer>().material.color = new Color(c.r, c.g, c.b, 0.5f);
    }

    public override void OnMouseClick(int buttonIndex) {
        if (Input.GetMouseButtonDown(buttonIndex)) {
            GameObject go = GameObject.Instantiate(cube, previewCube.transform.position, Quaternion.identity, buildingsGameObject.transform);
            go.transform.localScale = previewCube.transform.localScale;

            manager.SetGameMode(GameManager.GameModes.Move);
        }
    }
    public override void OnCursorRaycast() {
        RaycastHit hit;
        if (CursorRaycast(out hit) && (hit.transform.name == GameManager.PLAN_NAME || hit.transform.tag == GameManager.BUILDING_TAG_NAME)) {
            previewCube.SetActive(true);
            previewCube.transform.position = hit.point + new Vector3(0, previewCube.transform.localScale.y / 2, 0);
        } else {
            previewCube.SetActive(false);
        }
    }
    public override bool CursorRaycast(out RaycastHit hit, float maxDistance = 100) {
        if(base.CursorRaycast(out hit, maxDistance)) {
            if (hit.transform.name == GameManager.PLAN_NAME || hit.transform.tag == GameManager.BUILDING_TAG_NAME) {
                return true;
            }
        }
        return false;
    }
}