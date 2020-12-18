using UnityEngine;

public class ContentController : MonoBehaviour
{
    public void ChangeCurrentMaterial()
    {
        MaterialMGR.Instance.currentMaterial = GetComponentInChildren<SphereCollider>().GetComponent<Renderer>().material;
    }
}