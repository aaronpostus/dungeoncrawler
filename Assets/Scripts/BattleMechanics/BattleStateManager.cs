using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateManager
{
    public static bool IsBattleActive { get; private set; }

    public static void StartBattle()
    {
        IsBattleActive = true;
    }

    public static void EndBattle()
    {
        IsBattleActive = false;
    }
}
