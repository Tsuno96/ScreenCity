using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Mode : Game_Mode {

    private GameManager manager;

    public Move_Mode(GameManager _manager) {
        manager = _manager;
        MODE = GameManager.GameModes.Move;
    }

    public override void OnMouseClick(int buttonIndex) {
    }
    public override void OnCursorRaycast() {
    }
}
