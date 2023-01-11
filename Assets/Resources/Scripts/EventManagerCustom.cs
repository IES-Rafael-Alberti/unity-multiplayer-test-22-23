using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerCustom : MonoBehaviour
{
    public static Action<string, string> choiceEvent;
    public static Action endGameEvent;
    public static Action gameDecisionEvent;

    // Extra: event calling
    public static void ChoiceEvent(GameObject gameObject, string choice, string player)
    {
        choiceEvent?.Invoke(choice, player);
    }
    
    public static void EndGameEvent()
    {
        endGameEvent?.Invoke();
    }
    
    public static void GameDecisionEvent()
    {
        gameDecisionEvent?.Invoke();
    }
}
