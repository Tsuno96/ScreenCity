using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialMGR : MonoBehaviour
{

    public List<Material> lstMaterials;
    public Material currentMaterial;
    public Slider SliderR, SliderG, SliderB;
    public Color colmat;
    // Start is called before the first frame update
    void Start()
    {
       // ChooseMaterial(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChooseMaterial(int iMat)
    {
        currentMaterial = new Material(lstMaterials[iMat]);
        colmat = currentMaterial.color;
        Debug.Log(colmat.r + "," + colmat.g + ","+colmat.b + ",");
    }

    public void colorMaterial()
    {
        Debug.Log(" color");
        Color newCol = new Color(SliderR.value, SliderG.value, SliderB.value, currentMaterial.color.a);
        currentMaterial.color = newCol;
        colmat = currentMaterial.color;
    }
 }
