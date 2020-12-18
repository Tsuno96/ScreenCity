using Assets.Script.Create_Cube;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CubeController : MonoBehaviour
{
    public List<Vector3> lst_vec3Vertices;

    public enum Face_Index
    {
        Top = 0,
        Bottom = 1,
        Front = 2,
        Back = 3,
        Right = 4,
        Left = 5,
    }

    /*Face FTop;
    Face FBot;
    Face FFor;
    Face FBack;
    Face FRight;
    Face FLeft;*/
    private Dictionary<Face_Index, Face> faces;

    public List<int> lst_nBackVert;
    public List<int> lst_nForVert;
    public List<int> lst_nBotVert;
    public List<int> lst_nTopVert;
    public List<int> lst_nRightVert;
    public List<int> lst_nLeftVert;
    private Vector3 center;

    private void Start()
    {
        lst_vec3Vertices = GetComponent<MeshFilter>().mesh.vertices.ToList<Vector3>();
        InitArrayVertices();
        center = Vector3.zero;
        faces = new Dictionary<Face_Index, Face>();

        faces.Add(Face_Index.Top, new Face(lst_nTopVert, Vector3.up));
        faces.Add(Face_Index.Bottom, new Face(lst_nBotVert, Vector3.down));
        faces.Add(Face_Index.Front, new Face(lst_nForVert, Vector3.forward));
        faces.Add(Face_Index.Back, new Face(lst_nBackVert, Vector3.back));
        faces.Add(Face_Index.Right, new Face(lst_nRightVert, Vector3.right));
        faces.Add(Face_Index.Left, new Face(lst_nLeftVert, Vector3.left));

        /*FTop = new Face(lst_nTopVert, Vector3.up);
        FBot = new Face(lst_nBotVert, Vector3.down);
        FFor = new Face(lst_nForVert, Vector3.forward);
        FBack = new Face(lst_nBackVert, Vector3.back);
        FRight = new Face(lst_nRightVert, Vector3.right);
        FLeft = new Face(lst_nLeftVert, Vector3.left);*/
    }

    private void InitArrayVertices()
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

    public void ChangeScale(Face f, float add)
    {
        foreach (int i in f.iVertices)
        {
            lst_vec3Vertices[i] += f.axes * add;
        }
        GetComponent<MeshFilter>().mesh.vertices = lst_vec3Vertices.ToArray<Vector3>();
        GetComponent<MeshFilter>().mesh.RecalculateBounds();
        Destroy(GetComponent<BoxCollider>());
        gameObject.AddComponent(typeof(BoxCollider));
    }

    public Face GetFaceWithNormal(Vector3 normal)
    {
        foreach (KeyValuePair<Face_Index, Face> f in faces)
        {
            if (normal == f.Value.axes)
            {
                return f.Value;
            }
        }
        return null;
    }
}