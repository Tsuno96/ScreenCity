public class Material_Mode : Game_Mode
{
    private GameManager manager;

    public Material_Mode(GameManager _manager)
    {
        manager = _manager;
        MODE = GameManager.GameModes.Move;

        MaterialMGR.Instance.showPanel();
    }

    public override void OnMouseClick()
    {
    }

    public override void OnCursorRaycast()
    {
    }
}