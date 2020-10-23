using Assets.Script.Create_Cube;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using TreeEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CubeController : MonoBehaviour
{
    public List<Vector3> lst_vec3Vertices;

    Face FTop;
    Face FBot;
    Face FFor;
    Face FBack;
    Face FRight;
    Face FLeft;
    public List<int> lst_nBackVert;
    public List<int> lst_nForVert;
    public List<int> lst_nBotVert;
    public List<int> lst_nTopVert;
    public List<int> lst_nRightVert;
    public List<int> lst_nLeftVert;
    Vector3 center;

    // Start is called before the first frame update
    void Start()
    {

        lst_vec3Vertices = GetComponent<MeshFilter>().mesh.vertices.ToList<Vector3>();
        InitArrayVertices();
        center = Vector3.zero;
        FTop = new Face(lst_nTopVert, Vector3.up);
        FBot = new Face(lst_nBotVert, Vector3.down);
        FFor = new Face(lst_nForVert, Vector3.forward);
        FBack = new Face(lst_nBackVert, Vector3.back);
        FRight = new Face(lst_nRightVert, Vector3.right);
        FLeft = new Face(lst_nLeftVert, Vector3.left);


        //transform.localRotation = Quaternion.Euler(0, 90, 90);
        //GetCenter();
        //transform.RotateAround(GetComponent<MeshFilter>().mesh.bounds.center, Vector3.up, 90);
    
    }

    /*Vector3 GetCenter()
    {
        foreach(Vector3 v in lst_vec3Vertices)
        {
            center += v;
        }
        center /= lst_vec3Vertices.Count;
        return center;
    }*/

    void InitArrayVertices()
    {
        lst_vec3Vertices = GetComponent<MeshFilter>().mesh.vertices.ToList<Vector3>();

        for (int i = 0; i < lst_vec3Vertices.Count; i++)
        {
            if (lst_vec3Vertices[i].x == 0.5)
            {
                lst_nRightVert.Add(i);
            }
            else
            {
                lst_nLeftVert.Add(i);
            }
            if (lst_vec3Vertices[i].y == 0.5)
            {
                lst_nTopVert.Add(i);
            }
            else
            {
                lst_nBotVert.Add(i);
            }
            if (lst_vec3Vertices[i].z == 0.5)
            {
                lst_nForVert.Add(i);
            }
            else
            {
                lst_nBackVert.Add(i);
            }
        }


    }

    void ChangeScale(Face f, float add)
    {

        foreach (int i in f.iVertices)
        {
            lst_vec3Vertices[i] += f.axes * add;        
        }
        GetComponent<MeshFilter>().mesh.vertices = lst_vec3Vertices.ToArray<Vector3>();
        GetComponent<MeshFilter>().mesh.RecalculateBounds();
    }





    // Update is called once per frame
    void Update()
    {
    }
}
