using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MaterialMGR : MonoBehaviour
{

    public List<Material> lstMaterials;
    static Material currentMaterial;
    public Slider SliderR, SliderG, SliderB, SliderA, SliderM, SliderS;
    public Color colmat;

    public GameObject GOExemple;

    public GameObject GOCanvasMat;

    List<Material> assets;

    // Start is called before the first frame update
    void Start()
    {
        ChooseMaterial(0);
        showPanel();
        //PrintAssets();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChooseMaterial(int iMat)
    {
        currentMaterial = new Material(lstMaterials[iMat]);
        colmat = currentMaterial.color;

        SliderR.SetValueWithoutNotify(colmat.r);
        SliderG.SetValueWithoutNotify(colmat.g);
        SliderB.SetValueWithoutNotify(colmat.b);
        SliderA.SetValueWithoutNotify(colmat.a);

        SliderR.GetComponentInChildren<SliderValueTotext>().ShowSliderValue();
        SliderG.GetComponentInChildren<SliderValueTotext>().ShowSliderValue();
        SliderB.GetComponentInChildren<SliderValueTotext>().ShowSliderValue();
        SliderA.GetComponentInChildren<SliderValueTotext>().ShowSliderValue();

        SliderM.SetValueWithoutNotify(currentMaterial.GetFloat("_Metallic"));
        SliderM.GetComponentInChildren<SliderValueTotext>().ShowSliderValue();

        SliderS.SetValueWithoutNotify(currentMaterial.GetFloat("_Glossiness"));
        SliderS.GetComponentInChildren<SliderValueTotext>().ShowSliderValue();

        GOExemple.GetComponent<Renderer>().material = currentMaterial;
    }

    public void colorMaterial()
    {
        Color newCol = new Color(SliderR.value, SliderG.value, SliderB.value, SliderA.value);
        currentMaterial.color = newCol;
        colmat = currentMaterial.color;
    }

    public void metallicSmoothnessMaterial()
    {
        currentMaterial.SetFloat("_Metallic", SliderM.value);
        currentMaterial.SetFloat("_Glossiness", SliderS.value);
    }

    public void showPanel()
    {
        if (GOCanvasMat.activeSelf)
        {
            GOCanvasMat.SetActive(false);
        }
        else
        {
            GOCanvasMat.SetActive(true);
        }
    }

    public  void CreateMaterial()
    {
        // Create a simple material asset

        Material material = currentMaterial;
        AssetDatabase.CreateAsset(material, "Assets/CustomAssets/CustomMat.mat");

        // Print the path of the created asset
        Debug.Log(AssetDatabase.GetAssetPath(material));
    }


    void readAssets()
    {

    }

    [MenuItem("AssetDatabase/LoadAllAssetsAtPath")]
    private static void PrintAssets()
    {

        Object[] data = Resources.LoadAll("", typeof(Material));

        Debug.Log(data.Length + " Assets");

        foreach (Object o in data)
        {
            Debug.Log(o);
        }

        // outputs:
        //  5 Assets
        //  MySpriteTexture (UnityEngine.Texture2D)
        //  MyTexture_0 (UnityEngine.Sprite)
        //  MyTexture_1 (UnityEngine.Sprite)
        //  MyTexture_2 (UnityEngine.Sprite)
        //  MyTexture_3 (UnityEngine.Sprite)
    }

}
