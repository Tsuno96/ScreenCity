using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    public GameObject prefab;

    public int numberToCreate;

    private Material[] materials;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Populate()
    {
        Clear();
        materials = MaterialMGR.Instance.GetCustomMaterials();

        Debug.Log(materials.Length);

        GameObject newObj;

        for (int i = 0; i < materials.Length; i++)
        {
            newObj = Instantiate(prefab, transform);
            newObj.GetComponent<Image>().color = materials[i].color;
            newObj.GetComponentInChildren<SphereCollider>().gameObject.GetComponent<Renderer>().material = materials[i];
            newObj.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = i.ToString();
        }
    }

    private void Clear()
    {
        foreach (Transform tchild in transform)
        {
            Destroy(tchild.gameObject);
        }
    }


}