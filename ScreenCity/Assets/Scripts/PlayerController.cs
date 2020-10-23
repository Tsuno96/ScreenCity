using Assets.Script.Create_Cube;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private const string PLAN_NAME = "Plan";
    private const string BUILDING_TAG_NAME = "Building";


    public GameObject cube;

    public CharacterController controller;
    public GameObject buildingsGameObject;
    public Camera camera;
    public float speed = 10f;

    private GameObject previewCube;

    void Start() {
        previewCube = Instantiate(cube, buildingsGameObject.transform);
        previewCube.layer = LayerMask.NameToLayer("Ignore Raycast");

        Color c = previewCube.GetComponent<MeshRenderer>().material.color;
        previewCube.GetComponent<MeshRenderer>().material.color = new Color(c.r, c.g, c.b, 0.5f);
    }

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        #region Cube Preview
        if (Physics.Raycast(ray, out hit, 100) && (hit.transform.name == PLAN_NAME || hit.transform.tag == BUILDING_TAG_NAME)) {
            previewCube.SetActive(true);
            previewCube.transform.position = hit.point + new Vector3(0, previewCube.transform.localScale.y / 2, 0);
        } else {
            previewCube.SetActive(false);
        }
        #endregion

        // Do not read user input when the Cursor isn't locked
        //if (Cursor.lockState == CursorLockMode.Locked) {

            #region Movement manager

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z + transform.up * -Input.mouseScrollDelta.y * 3;

            controller.Move(move * Time.deltaTime * speed);

            #endregion

            #region Click manager
            if (Input.GetMouseButtonDown(0)) {
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
            }
            #endregion

        //}
    }

    private void UpdateCubeSize(Vector3 size) {
        previewCube.transform.localScale = size;
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
