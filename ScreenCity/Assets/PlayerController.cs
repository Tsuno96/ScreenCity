using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public CharacterController controller;
    public Camera camera;
    public float speed = 10f;

    void Start() {
        
    }

    void Update() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + camera.transform.forward * z;

        controller.Move(move * Time.deltaTime * speed);
    }
}
