using UnityEngine;

public abstract class Game_Mode
{
    public GameManager.GameModes MODE { get; set; }
    private GameManager manager;

    public abstract void OnMouseClick();

    public abstract void OnCursorRaycast();

    public virtual bool CursorRaycast(out RaycastHit hit, float maxDistance = 100)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, maxDistance);
    }
}