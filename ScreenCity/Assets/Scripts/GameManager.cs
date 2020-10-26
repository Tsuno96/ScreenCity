using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public BuildingObject cube;
    public BuildingObject screen;
    public GameObject buildingsGameObject;

    public static readonly float EPSILON = 0.01f;
    public static readonly string PLAN_NAME = "Plan";
    public static readonly string BUILDING_TAG_NAME = "Building";

    public enum GameModes {
        Move,
        Add_Object,
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
        SetGameMode(GameModes.Add_Object, cube);
    }
    public void SetGameMode_AddScreen() {
        SetGameMode(GameModes.Add_Object, screen);
    }
    public void SetGameMode_Remove() {
        SetGameMode(GameModes.Remove);
    }

    public void SetGameMode(GameModes m, BuildingObject buildObj = null) {
        /*if (mode.MODE == GameModes.Add_Cube) {
            GameObject.Destroy(((AddCube_Mode)mode).previewCube);
        }
        if (mode.MODE == GameModes.Add_Screen) {
            GameObject.Destroy(((AddScreen_Mode)mode).previewScreen);
        }*/

        switch (m) {
            case GameModes.Add_Object:
                mode = new AddObject_Mode(this, buildObj, buildingsGameObject.transform);
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
            
            if(hit.transform.tag == GameManager.BUILDING_TAG_NAME) { // Cubes
                return true;
            } else if (hit.transform.parent != null && hit.transform.parent.tag == GameManager.BUILDING_TAG_NAME) { // Screens
                return true;
            }
        }
        return false;
    }
}

public class AddObject_Mode : Game_Mode {

    private readonly GameManager manager;
    private readonly BuildingObject buildingObject;
    private readonly Transform parent;
    public GameObject previewObject;

    public AddObject_Mode(GameManager _manager, BuildingObject _buildingObject, Transform _parent) {
        MODE = GameManager.GameModes.Add_Object;

        manager = _manager;
        buildingObject = _buildingObject;
        parent = _parent;
        buildingObject.parent = parent;
        previewObject = _buildingObject.GetPreviewObject();
    }

    public override void OnMouseClick(int buttonIndex) {
        RaycastHit hit;
        if (CursorRaycast(out hit) && Input.GetMouseButtonDown(buttonIndex)) {
            buildingObject.InstantiateFromPreview(previewObject);
        }
    }
    public override void OnCursorRaycast() {
        RaycastHit hit;
        if (CursorRaycast(out hit)) {
            previewObject.transform.position = buildingObject.PositionOnSurface(hit);
            previewObject.transform.rotation = buildingObject.RotationOnSurface(hit);
            previewObject.SetActive(true);
        } else {
            previewObject.SetActive(false);
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