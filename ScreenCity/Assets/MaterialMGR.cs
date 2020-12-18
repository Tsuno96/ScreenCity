using System;
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

    private SortedDictionary<int, Material> dictCustomMat;

    public GameObject GOPanelCreate;
    public GameObject GOGridCustom;

    public Material[] customMaterials;
    private int idcustomMat;

    private void Awake()
    {
        if (pInstance == null)
        {
            pInstance = this;
        }
        else if (pInstance != this)
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

    public void ChooseMaterial(int iMat)
    {
        currentMaterial = new Material(lstMaterials[iMat]);

        UpdateUI();
    }

    private void UpdateUI()
    {
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
        if (GOPanelCreate.activeSelf)
        {
            GOPanelCreate.SetActive(false);
        }
        else
        {
            ClearUI();
            GOPanelCreate.SetActive(true);
            UpdateUI();
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
        customMaterials = System.Array.ConvertAll(Resources.LoadAll("CustomAssets", typeof(Material)), o => (Material)o);

        Debug.Log(customMaterials.Length + " Assets");

        dictCustomMat = new SortedDictionary<int, Material>();

        foreach (Material m in customMaterials)
        {
            string strId = "";
            foreach (char c in m.name)
            {
                if (Char.IsDigit(c))
                {
                    strId += c;
                }
            }
            int id = Int32.Parse(strId);
            dictCustomMat.Add(id, m);
        }
        List<Material> customMatSorted = new List<Material>();
        foreach (KeyValuePair<int, Material> kvp in dictCustomMat)
        {
            customMatSorted.Add(kvp.Value);
        }

        idcustomMat = customMaterials.Length;

        return customMatSorted.ToArray();
    }

    public void showGrid()
    {
        if (GOGridCustom.activeSelf)
        {
            GOGridCustom.SetActive(false);
        }
        else
        {
            ClearUI();
            GOGridCustom.SetActive(true);
            GOGridCustom.GetComponentInChildren<PopulateGrid>().Populate();
        }
    }

    public void ClearUI()
    {
        GOPanelCreate.SetActive(false);
        GOGridCustom.SetActive(false);
    }
}