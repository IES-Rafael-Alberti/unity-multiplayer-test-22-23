using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    private GameObject myPlayer;
    private bool gameEnd = false;
    [SerializeField] private GameObject doors;
    [SerializeField] private GameObject choicesSprites;
    
    // Start is called before the first frame update
    void Start()
    {
        EventManagerCustom.endGameEvent += OnEndGame;
        EventManagerCustom.choiceEvent += OnChoice;
        EventManagerCustom.gameDecisionEvent += OnGameDecision;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnChoice(string choice, string player)
    {
        GameManager.Choice(choice, player);
    }
    
    void OnEndGame()
    {
        doors.SetActive(false);
        choicesSprites.SetActive(false);
    }

    void OnGameDecision()
    {
        if (!gameEnd)
        {
            gameEnd = true;
            GameObject go = GameObject.Find("id");
            
            go.GetComponent<TMPro.TextMeshProUGUI>().text = 
                winResult(GameManager.choices, "P" + GameManager.playerId);
        }
    }

    string winResult(Dictionary<string, string> choices, string player)
    {
        if (choices["P1"] == choices["P2"])
            return "EMPATE!!!";
        string winner = "P1";
        switch (choices["P1"])
        {
            case "piedra":
                if (choices["P2"] == "papel") winner = "P2";
                break;
            case "papel":
                if (choices["P2"] == "tijeras") winner = "P2";
                break;
            case "tijeras":
                if (choices["P2"] == "piedra") winner = "P2";
                break;
        }

        if (winner == player) return "VICTORIA!!!!";
        return "PAQUETE, NO SABES JUGAR NI A ESTO, MANCO!!!!";
    }
}
