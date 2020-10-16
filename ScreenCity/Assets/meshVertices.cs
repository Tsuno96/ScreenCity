using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class meshVertices : MonoBehaviour
{
    public List<Vector3> mr;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshFilter>().mesh.vertices.ToList<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
