using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Create_Cube
{
    public class Face
    {
        public List<int> iVertices;
        public Vector3 axes;

        public Face(List<int> lstVec, Vector3 _axes)
        {
            iVertices = lstVec;
            axes = _axes;
        }
    }
}