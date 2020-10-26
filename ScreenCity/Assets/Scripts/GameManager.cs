using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject cube;
    public GameObject screen;
    public GameObject buildingsGameObject;

    public static readonly float EPSILON = 0.01f;
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
        mode = new Move_Mode(this);
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
        if (mode.MODE == GameModes.Add_Cube) {
            GameObject.Destroy(((AddCube_Mode)mode).previewCube);
        }
        if (mode.MODE == GameModes.Add_Screen) {
            GameObject.Destroy(((AddScreen_Mode)mode).previewScreen);
        }

        switch (m) {
            case GameModes.Add_Cube:
                mode = new AddCube_Mode(this, cube, buildingsGameObject);
                break;
            case GameModes.Add_Screen:
                mode = new AddScreen_Mode(this, screen, buildingsGameObject);
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

    public GameManager.GameModes MODE { get; set; }
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
        MODE = GameManager.GameModes.Move;
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
        MODE = GameManager.GameModes.Remove;
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

    private GameObject buildingsGameObject;
    private GameObject screen;

    public GameObject previewScreen;

    public AddScreen_Mode(GameManager _manager, GameObject _screen, GameObject _buildingsGameObject) {
        manager = _manager;
        MODE = GameManager.GameModes.Add_Screen;

        screen = _screen;
        buildingsGameObject = _buildingsGameObject;
        previewScreen = GameObject.Instantiate(_screen, _buildingsGameObject.transform);
        previewScreen.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        previewScreen.transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public override void OnMouseClick(int buttonIndex) {
        RaycastHit hit;
        if (CursorRaycast(out hit) && Input.GetMouseButtonDown(buttonIndex)) {
            GameObject go = GameObject.Instantiate(screen, previewScreen.transform.position, Quaternion.identity, buildingsGameObject.transform);
            go.transform.localScale = previewScreen.transform.localScale;
            go.transform.rotation = previewScreen.transform.rotation;
        }
    }
    public override void OnCursorRaycast() {
        RaycastHit hit;
        if (CursorRaycast(out hit)) {
            previewScreen.SetActive(true);
            previewScreen.transform.position = hit.point + hit.normal * GameManager.EPSILON;
            previewScreen.transform.rotation = Quaternion.Euler(hit.normal.y * 90, hit.normal.x * 90, hit.normal.z * 90);
        } else {
            previewScreen.SetActive(false);
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

public class AddCube_Mode : Game_Mode {

    private GameManager manager;
    private GameObject cube;
    public GameObject previewCube { get; set; }
    private GameObject buildingsGameObject;

    public AddCube_Mode(GameManager _manager, GameObject _cube, GameObject _buildingsGameObject) {
        MODE = GameManager.GameModes.Add_Cube;

        manager = _manager;
        cube = _cube;
        buildingsGameObject = _buildingsGameObject;
        previewCube = GameObject.Instantiate(_cube, _buildingsGameObject.transform);
        previewCube.layer = LayerMask.NameToLayer("Ignore Raycast");

        Color c = previewCube.GetComponent<MeshRenderer>().material.color;
        previewCube.GetComponent<MeshRenderer>().material.color = new Color(c.r, c.g, c.b, 0.5f);
    }

    public override void OnMouseClick(int buttonIndex) {
        RaycastHit hit;
        if (CursorRaycast(out hit) && Input.GetMouseButtonDown(buttonIndex)) {
            GameObject go = GameObject.Instantiate(cube, previewCube.transform.position, Quaternion.identity, buildingsGameObject.transform);
            go.transform.localScale = previewCube.transform.localScale;

            //manager.SetGameMode(GameManager.GameModes.Move);
        }
    }
    public override void OnCursorRaycast() {
        RaycastHit hit;
        if (CursorRaycast(out hit)) {
            previewCube.SetActive(true);
            previewCube.transform.position = hit.point + hit.normal * previewCube.transform.localScale.y / 2;
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