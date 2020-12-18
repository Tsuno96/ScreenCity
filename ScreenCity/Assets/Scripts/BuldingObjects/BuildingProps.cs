using UnityEngine;

public class BuildingProps : BuildingObject
{
    public override GameObject GetPreviewObject()
    {
        GameObject previewProps = GameObject.Instantiate(gameObject, parent);
        previewProps.layer = LayerMask.NameToLayer("Ignore Raycast");
        previewProps.name = "Preview " + transform.name;

        SetTransparent(previewProps);

        return previewProps;
    }

    public override Quaternion RotationOnSurface(RaycastHit hit)
    {
        return Quaternion.Euler(PlayerController.previewRotation);
    }

    public override Vector3 PositionOnSurface(RaycastHit hit)
    {
        return hit.point;
    }

    public override GameObject InstantiateFromPreview(GameObject preview)
    {
        GameObject newProps = GameObject.Instantiate(gameObject, preview.transform.position, Quaternion.identity, parent);
        newProps.transform.localScale = preview.transform.localScale;
        newProps.transform.rotation = preview.transform.rotation;
        newProps.tag = "Building";
        newProps.layer = 0; //WOOOOOOOOOW
        Debug.Log(newProps.name);
        return newProps;
    }
}