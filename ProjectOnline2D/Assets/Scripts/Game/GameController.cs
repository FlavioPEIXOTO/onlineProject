using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    private string playerForm;

    public GameObject LooseScreen;
    public Text LooseText;

    private int move;

    public GameObject restartButton;

    void Awake()
    {
        LooseScreen.SetActive(false);
        GameControllerInfosButtons();
        playerForm = "X";
        move = 0;

        restartButton.SetActive(false);
    }

    void GameControllerInfosButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GameBut>().GameControllerInfos(this);
        }
    }

    public string GetGameForm()
    {
        return playerForm;
    }

    public void EndTurn()
    {
        move++;

        //Horizontal 1 --> buttons 0/1/2
        if (buttonList[0].text == playerForm && buttonList[1].text == playerForm && buttonList[2].text == playerForm)
        {
            Loose(playerForm);
        }

        //Horizontal 2 --> buttons 3/4/5
        if (buttonList[3].text == playerForm && buttonList[4].text == playerForm && buttonList[5].text == playerForm)
        {
            Loose(playerForm);
        }

        //Horizontal 3 --> buttons 6/7/8
        if (buttonList[6].text == playerForm && buttonList[7].text == playerForm && buttonList[8].text == playerForm)
        {
            Loose(playerForm);
        }

        //vertical 1 --> buttons 0/3/6
        if (buttonList[0].text == playerForm && buttonList[3].text == playerForm && buttonList[6].text == playerForm)
        {
            Loose(playerForm);
        }

        //Vertical 2 --> buttons 1/4/7
        if (buttonList[1].text == playerForm && buttonList[4].text == playerForm && buttonList[7].text == playerForm)
        {
            Loose(playerForm);
        }

        //Vertical 3 --> buttons 2/5/8
        if (buttonList[2].text == playerForm && buttonList[5].text == playerForm && buttonList[8].text == playerForm)
        {
            Loose(playerForm);
        }

        //Diangonal 1 --> buttons 0/4/8
        if (buttonList[0].text == playerForm && buttonList[4].text == playerForm && buttonList[8].text == playerForm)
        {
            Loose(playerForm);
        }

        //Diagonal 2 --> buttons 2/4/6
        if (buttonList[2].text == playerForm && buttonList[4].text == playerForm && buttonList[6].text == playerForm)
        {
            Loose(playerForm);
        }

        if (move >= 9)
        {
            Loose("DRAW");
        }

        ChoiceForm();
    }

    //Function that change the form every rounds
    void ChoiceForm()
    {
        playerForm = (playerForm == "X") ? "O" : "X";
    }

    void Loose(string playerWin)
    {
        GameInteractRestart(false);

        if (playerWin == "DRAW")
        {
            LooseMessage("DRAW");
        }
        else
        {
            LooseMessage(playerForm + " WIN");
        }
        restartButton.SetActive(true);
    }

    void LooseMessage(string message)
    {
        LooseScreen.SetActive(true);
        LooseText.text = message;
        StartCoroutine(UpdateVictoryLoose());
    }

    public void Restart()
    {
        playerForm = "X";
        move = 0;
        LooseScreen.SetActive(false);

        GameInteractRestart(true);

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }

        restartButton.SetActive(false);
    }

    void GameInteractRestart(bool isRestart)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = true;
        }
    }

    IEnumerator UpdateVictoryLoose() {
        string post_url = "http://localhost/SQLUnity/UpdateVL.php";

        UnityWebRequest www = UnityWebRequest.Post(post_url, "data");
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else if (www.downloadHandler.text == "false")
        {
            Debug.Log("sql upload");
        }
        else
        {
            Debug.Log("" + www.downloadHandler.text);
        }
    }
}
