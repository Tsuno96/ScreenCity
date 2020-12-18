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

    public static BuildingObject currentProps { get; set; }

    public GameObject propsCanvas;
    public Transform propsListParent;

    public enum GameModes {
        Move,
        Add_Object,
        Remove,
        Edit,
        Material
    }
    public Game_Mode mode;

    private void Start() {
        currentProps = props[0];

        mode = new Move_Mode(this);


        for (int i = 0; i < props.Count; i ++) {
            GameObject listProps = Instantiate(propsListItem, propsListParent);

            Text t = listProps.GetComponentInChildren<Text>();

            //Transform preview = listProps.GetComponentInChildren<BuildingProps>().transform;

            Transform preview = listProps.transform.Find("preview");
            GameObject prevObj = Instantiate(props[i].gameObject, listProps.transform);
            prevObj.name = "preview";
            prevObj.transform.position = preview.position;
            prevObj.transform.rotation = preview.rotation;
            prevObj.transform.localScale = preview.localScale;

            Destroy(preview.gameObject);

            t.text = props[i].name;
        }
        propsCanvas.SetActive(false);
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
        SetGameMode(GameModes.Add_Object, currentProps);
        propsCanvas.SetActive(true);
    }
    public void SetGameMode_Remove() {
        SetGameMode(GameModes.Remove);
    }
    public void SetGameMode_Edit() {
        SetGameMode(GameModes.Edit);
    }
    public void SetGameMode_Material()
    {
        SetGameMode(GameModes.Material);
    }
    public void SetGameMode(GameModes m, BuildingObject buildObj = null) {
        propsCanvas.SetActive(false);
        if (mode.MODE == GameModes.Add_Object) {
            GameObject.Destroy(((AddObject_Mode)mode).previewObject);
        } else if (mode.MODE != GameModes.Material) {
            MaterialMGR.Instance.ClearUI();
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
            case GameModes.Material:
                mode = new Material_Mode(this);
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