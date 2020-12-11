using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsItem : MonoBehaviour {
    public void changeProps() {
        GameObject _preview = transform.Find("preview").gameObject;
        GameObject g = Instantiate(_preview, GameObject.Find("Buildings").transform);
        g.transform.localScale = new Vector3(1, 1, 1);
        GameManager.currentProps = g.GetComponent<BuildingObject>();
    }
}
