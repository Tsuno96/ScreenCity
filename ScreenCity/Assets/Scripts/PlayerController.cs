using System.Collections;
using System.Collections.Generic;
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

    void Start() {
        
    }

    void Update() {

        #region Movement manager

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + camera.transform.forward * z;

        controller.Move(move * Time.deltaTime * speed);

        #endregion

        #region Click manager
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) {
                if (hit.transform.name == PLAN_NAME) {
                    
                    Vector3 cubePos = hit.point;
                    //cubePos.y = mousePosition.y;
                    GameObject go = Instantiate(cube, cubePos, Quaternion.identity, buildingsGameObject.transform);
                    Debug.Log(go.transform.localScale);
                } else if (hit.transform.tag == BUILDING_TAG_NAME) {

                }
            }
        }
        #endregion
    }
}
