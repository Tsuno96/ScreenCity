using UnityEngine;

public class PropsItem : MonoBehaviour
{
    public void changeProps()
    {
        GameObject _preview = transform.Find("preview").gameObject;
        GameObject g = Instantiate(_preview, GameObject.Find("Buildings").transform);
        g.transform.localScale = new Vector3(1, 1, 1);

        GameObject gmGO = GameObject.Find("GameManager");
        GameManager.currentProps = g.GetComponent<BuildingObject>();
        if (gmGO != null)
        {
            GameManager gm = gmGO.GetComponent<GameManager>();
            gm.SetGameMode_AddProps();
        }
    }
}