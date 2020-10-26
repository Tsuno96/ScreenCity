using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.CompilerServices;

public class ScreenController : MonoBehaviour
{

    public Texture texture;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Apply();
    }

    [MenuItem("Example/Overwrite Texture")]
    public void Apply()
    {
        texture = GetComponent<Renderer>().material.mainTexture;
       /* if (texture == null)
        {
            EditorUtility.DisplayDialog("Select Texture", "You must select a texture first!", "OK");
            return;
        }*/

        string path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
        Debug.Log(path);
        if (path.Length != 0)
        {
            var fileContent = File.ReadAllBytes(path);
            var tex = new Texture2D(1, 1);
            tex.LoadImage(fileContent);

            GetComponent<Renderer>().material.mainTexture = tex;
        }
    }
}
