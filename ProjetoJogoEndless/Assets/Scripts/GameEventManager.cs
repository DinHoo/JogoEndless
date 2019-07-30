using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventManager
{
    public delegate void GameEvent();
    public static event GameEvent gameStart, gameEnd;

    public static void TriggerGameStart()
    {
        if (gameStart != null)
            gameStart();
    }
}
