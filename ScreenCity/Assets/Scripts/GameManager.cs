using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public BuildingObject cube;
    public BuildingObject screen;
    public List<BuildingObject> props;
    public GameObject buildingsGameObject;
    [Space]
    public GameObject propsListItem;

    public static readonly float EPSILON = 0.01f;
    public static readonly string PLAN_NAME = "Plan";
    public static readonly string BUILDING_TAG_NAME = "Building";

    public int indexProps { get; set; }

    public enum GameModes {
        Move,
        Add_Object,
        Remove,
        Edit
    }
    public Game_Mode mode;

    private void Start() {
        mode = new Move_Mode(this);

        Transform scrollView = GameObject.Find("Content").transform.Find("ListProps");
        foreach (BuildingObject b in props) {
            Instantiate(propsListItem, scrollView);
        }
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
    public void SetGameMode_AddProps() {
        SetGameMode(GameModes.Add_Object, props[indexProps]);
    }
    public void SetGameMode_Remove() {
        SetGameMode(GameModes.Remove);
    }
    public void SetGameMode_Edit() {
        SetGameMode(GameModes.Edit);
    }

    public void SetGameMode(GameModes m, BuildingObject buildObj = null) {
        if (mode.MODE == GameModes.Add_Object) {
            GameObject.Destroy(((AddObject_Mode)mode).previewObject);
        }

        switch (m) {
            case GameModes.Add_Object:
                mode = new AddObject_Mode(this, buildObj, buildingsGameObject.transform);
                break;
            case GameModes.Remove:
                mode = new Remove_Mode(this);
                break;
            case GameModes.Edit:
                mode = new Edit_Mode(this);
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