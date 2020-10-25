using Assets.Script.Create_Cube;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


    public CharacterController controller;
    public float speed = 10f;

    public GameManager manager;


    void Update() {

        #region Movement manager

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z + transform.up * -Input.mouseScrollDelta.y * 3;

        controller.Move(move * Time.deltaTime * speed);

        #endregion

        manager.mode.OnCursorRaycast();

        manager.mode.OnMouseClick(0);

        /*if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit, 100)) {
                if (hit.transform.name == PLAN_NAME || hit.transform.tag == BUILDING_TAG_NAME) {
                    Vector3 cubePos = hit.point;
                    GameObject go = Instantiate(cube, cubePos + new Vector3(0, previewCube.transform.localScale.y / 2, 0), Quaternion.identity, buildingsGameObject.transform);
                    go.transform.localScale = previewCube.transform.localScale;
                }
            }
        } else if (Input.GetMouseButtonDown(1)) {
            if (Physics.Raycast(ray, out hit, 100)) {
                if (hit.transform.tag == BUILDING_TAG_NAME) {
                    CubeController cc = hit.transform.gameObject.GetComponent<CubeController>();
                    if (cc != null) {
                        Face f = cc.GetFaceWithNormal(hit.normal);
                        cc.ChangeScale(f, 0.5f);
                    }
                }
            }
        }*/
    }

    private void UpdateCubeSize(Vector3 size) {
        //previewCube.transform.localScale = size;
    }
    public void Slider_changeCubeSize(Slider slider) {
        UpdateCubeSize(new Vector3(slider.value, slider.value, slider.value));
    }
    public void Button_SetCubeSmall() {
        UpdateCubeSize(new Vector3(0.5f, 0.5f, 0.5f));
    }
    public void Button_SetCubeNormal() {
        UpdateCubeSize(new Vector3(1f, 1f, 1f));
    }
    public void Button_SetCubeLarge() {
        UpdateCubeSize(new Vector3(2f, 2f, 2f));
    }
}
