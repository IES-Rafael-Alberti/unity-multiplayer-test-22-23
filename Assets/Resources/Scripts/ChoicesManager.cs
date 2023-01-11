using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicesManager : MonoBehaviour
{
    [SerializeField] public string choice;
    [SerializeField] public string player;

    // Start is called before the first frame update
    void Start()
    {
        string[] data = gameObject.name.Split(" ");
        choice = data[0];
        player = data[1];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EventManagerCustom.ChoiceEvent(other.gameObject, choice, player);
    }
}
