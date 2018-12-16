using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeController : MonoBehaviour {

    public bool ToggleQuickMode;
    public static bool isQuickMode;

    void Start()
    {
        isQuickMode = ToggleQuickMode;
    }

}
