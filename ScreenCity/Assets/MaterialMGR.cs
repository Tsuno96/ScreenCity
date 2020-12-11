using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MaterialMGR : MonoBehaviour
{
    private static MaterialMGR pInstance = null;
    public static MaterialMGR Instance { get { return pInstance; } }

    public List<Material> lstMaterials;
    public Material currentMaterial;
    public Slider SliderR, SliderG, SliderB, SliderA, SliderM, SliderS;
    public Color colmat;

    public GameObject GOExemple;

    public GameObject GOCanvasMat;
    public GameObject GOGrid;


    public Material[] customMaterials;
    private int idcustomMat;

    private void Awake()
    {
        if(pInstance == null)
        {
            pInstance = this;
        }
        else if(pInstance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        ChooseMaterial(0);
        showPanel();
        showGrid();

        GetCustomMaterials();
    }

    // Update is called once per frame
    private void Update()
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
            ClearUI();
            GOCanvasMat.SetActive(true);
        }
    }

    public void CreateMaterial()
    {
        Material material = new Material(currentMaterial);
        AssetDatabase.CreateAsset(material, "Assets/resources/CustomAssets/customMat" + idcustomMat + ".mat");
        idcustomMat++;
        // Print the path of the created asset
        Debug.Log(AssetDatabase.GetAssetPath(material));
        GetCustomMaterials();
    }

    public Material[] GetCustomMaterials()
    {
        customMaterials = System.Array.ConvertAll(Resources.LoadAll("CustomAssets", typeof(Material)), o =>(Material)o);

        Debug.Log(customMaterials.Length + " Assets");

        /*foreach (Object o in customMaterials)
        {
            Debug.Log(o);
        }*/
        idcustomMat = customMaterials.Length;

        return customMaterials;
    }


    public void showGrid()
    {
        if (GOGrid.activeSelf)
        {
            GOGrid.SetActive(false);
        }
        else
        {
            ClearUI();
            GOGrid.SetActive(true);
            GOGrid.GetComponentInChildren<PopulateGrid>().Populate();
        }

    }
    
    void ClearUI()
    {
        GOCanvasMat.SetActive(false);
        GOGrid.SetActive(false);
    }
}