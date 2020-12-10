using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCube : BuildingObject {

    public override BuildingObject GetPreviewObject() {
        GameObject previewCube = GameObject.Instantiate(gameObject, parent);
        previewCube.layer = LayerMask.NameToLayer("Ignore Raycast");
        previewCube.name = "Preview Cube";

        SetTransparent(previewCube);

        return previewCube.GetComponent<BuildingObject>();
    }

    /*private void OnTriggerStay(Collider other) {
        try {
            SphereCollider s = (SphereCollider)other;
            if (other.gameObject != gameObject && other.CompareTag("Building"))
                collide++;
        } catch (System.InvalidCastException e) {}
    }
    private void FixedUpdate() {
        collide = 0;
    }*/

    public override void SetPreviewPosition(RaycastHit hit) {

        SphereCollider[] colliders = GetComponents<SphereCollider>();

        // For debug
        if (Input.GetKeyDown(KeyCode.M))
            foreach (SphereCollider collider in colliders) {
                if (collider.isTrigger) {
                    Debug.Log("collide");
                }
            }

        transform.position = PositionOnSurface(hit);
        transform.rotation = RotationOnSurface(hit);
    }

    public override Vector3 PositionOnSurface(RaycastHit hit) {
        return base.PositionOnSurface(hit);
    }

    public override GameObject InstantiateFromPreview(GameObject preview) {
        GameObject newCube = GameObject.Instantiate(gameObject, preview.transform.position, Quaternion.identity, parent);
        newCube.transform.localScale = preview.transform.localScale;
        return newCube;
    }
}
