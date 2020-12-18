using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;
    public Camera playerCamera;

    public GameManager manager;

    public static Vector3 previewRotation = Vector3.zero;

    private void Update()
    {
        #region Movement manager

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z + playerCamera.transform.forward * Input.mouseScrollDelta.y * 5;

        if (Input.GetKey(KeyCode.Space))
        {
            move += transform.up * 0.33f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            move -= transform.up * 0.33f;
        }

        controller.Move(move * Time.deltaTime * speed);

        #endregion Movement manager

        manager.mode.OnCursorRaycast();

        manager.mode.OnMouseClick();

        if (Input.GetKeyDown(KeyCode.R))
        {
            previewRotation += new Vector3(0, 45, 0);
        }
    }

    private void UpdateCubeSize(Vector3 size)
    {
        //previewCube.transform.localScale = size;
    }

    public void Slider_changeCubeSize(Slider slider)
    {
        UpdateCubeSize(new Vector3(slider.value, slider.value, slider.value));
    }

    public void Button_SetCubeSmall()
    {
        UpdateCubeSize(new Vector3(0.5f, 0.5f, 0.5f));
    }

    public void Button_SetCubeNormal()
    {
        UpdateCubeSize(new Vector3(1f, 1f, 1f));
    }

    public void Button_SetCubeLarge()
    {
        UpdateCubeSize(new Vector3(2f, 2f, 2f));
    }
}