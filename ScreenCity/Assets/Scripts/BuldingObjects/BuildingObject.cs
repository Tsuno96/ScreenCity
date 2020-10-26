using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingObject : MonoBehaviour {
    public Transform parent;
    public abstract GameObject GetPreviewObject();
    public abstract GameObject InstantiateFromPreview(GameObject preview);

    public virtual Vector3 PositionOnSurface(RaycastHit hit) {
        return hit.point + hit.normal * transform.localScale.y / 2;
    }
    public virtual Quaternion RotationOnSurface(RaycastHit hit) {
        return Quaternion.identity;
    }

    protected void SetTransparent(GameObject g) {
        MeshRenderer mr = g.GetComponent<MeshRenderer>();
        if (mr == null) return;
        Color c = mr.material.color;
        g.GetComponent<MeshRenderer>().material.color = new Color(c.r, c.g, c.b, 0.5f);
    }
}
