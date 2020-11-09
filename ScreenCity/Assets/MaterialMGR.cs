using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialMGR : MonoBehaviour
{

    public List<Material> lstMaterials;
    public Material currentMaterial;
    public Slider SliderR, SliderG, SliderB, SliderA, SliderM, SliderS;
    public Color colmat;

    public GameObject GOExemple;
    // Start is called before the first frame update
    void Start()
    {
        ChooseMaterial(0);
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
}
