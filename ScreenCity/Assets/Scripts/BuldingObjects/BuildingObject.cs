using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingObject : MonoBehaviour {
    public Transform parent;
    public abstract BuildingObject GetPreviewObject();
    public abstract GameObject InstantiateFromPreview(GameObject preview);

    public virtual Vector3 PositionOnSurface(RaycastHit hit) {
        return hit.point + hit.normal * transform.localScale.y / 2;
    }
    public virtual Quaternion RotationOnSurface(RaycastHit hit) {
        return Quaternion.identity;
    }

    public virtual void SetPreviewPosition(RaycastHit hit) {
        transform.position = PositionOnSurface(hit);
        transform.rotation = RotationOnSurface(hit);
    }

    public static void ToFadeMode(Material material) {
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

    protected void SetTransparent(GameObject g) {
        MeshRenderer mr = g.GetComponent<MeshRenderer>();
        if (mr == null) return;

        BuildingObject.ToFadeMode(mr.material);

        Color c = mr.material.color;
        g.GetComponent<MeshRenderer>().material.color = new Color(c.r, c.g, c.b, 0.5f);
    }
}
