using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Game_Mode {
        Move,
        Add_Cube,
        Remove_Cube
    }
    public Game_Mode mode;
}
