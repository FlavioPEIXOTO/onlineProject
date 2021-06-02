using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBut : MonoBehaviour
{
    public Button button;
    public Text buttonText;

    private GameController gameController;

    public void setButton()
    {
        buttonText.text = gameController.GetGameForm();
        button.interactable = false;
        gameController.EndTurn();
    }

    public void GameControllerInfos(GameController controller)
    {
        gameController = controller;
    }
}
