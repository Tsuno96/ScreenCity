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
    public Camera playerCamera;

    public GameManager manager;
    
    void Update() {

        #region Movement manager

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z + playerCamera.transform.forward * Input.mouseScrollDelta.y * 5;

        if (Input.GetKey(KeyCode.Space)) {
             move += transform.up * 0.33f;
        } else if (Input.GetKey(KeyCode.LeftShift)) {
            move -= transform.up * 0.33f;
        }

        controller.Move(move * Time.deltaTime * speed);

        #endregion

        manager.mode.OnCursorRaycast();

        manager.mode.OnMouseClick();

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
