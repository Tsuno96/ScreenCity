using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        RaycastHit hit;


        // Do nothing when the Cursor isn't locked
        if (Cursor.lockState == CursorLockMode.None)
            return;


        #region Cube Preview
        if (Physics.Raycast(ray, out hit, 100) && (hit.transform.name == PLAN_NAME || hit.transform.tag == BUILDING_TAG_NAME)) {
            previewCube.SetActive(true);
            previewCube.transform.position = hit.point + new Vector3(0, previewCube.transform.localScale.y / 2, 0);
        } else {
            previewCube.SetActive(false);
        }
        #endregion

        #region Movement manager

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + camera.transform.forward * z;

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
        }
        #endregion
    }

    private void UpdateCubeSize(Vector3 size) {
        previewCube.transform.localScale = size;
    }

    public void SetCubeSmall() {
        Debug.Log("Small");
        UpdateCubeSize(new Vector3(0.5f, 0.5f, 0.5f));
    }
    public void SetCubeNormal() {
        Debug.Log("Normal");
        UpdateCubeSize(new Vector3(1f, 1f, 1f));
    }
    public void SetCubeLarge() {
        Debug.Log("Large");
        UpdateCubeSize(new Vector3(2f, 2f, 2f));
    }
}
