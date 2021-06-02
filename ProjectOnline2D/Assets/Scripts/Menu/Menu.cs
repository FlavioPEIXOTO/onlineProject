using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button registerButton;
    public Button loginButton;
    public Button logoutButton;

    public void Start()
    {
        StartCoroutine(CheckLog());
    }

    IEnumerator CheckLog()
    {
        string post_url = "http://localhost/SQLUnity/CheckLog.php";

        var data = "";

        UnityWebRequest www = UnityWebRequest.Post(post_url, data);
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else if (www.downloadHandler.text == "true")
        {
            Debug.Log("User not connected, please login");
            registerButton.gameObject.SetActive(true);
            loginButton.gameObject.SetActive(true);
            logoutButton.gameObject.SetActive(false);
        }
        else if (www.downloadHandler.text == "false")
        {
            Debug.Log("User connected");
            registerButton.gameObject.SetActive(false);
            loginButton.gameObject.SetActive(false);
            logoutButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Player Creation failed : " + www.downloadHandler.text);
        }
    }



    public void RegisterScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void LoginScreen()
    {
        SceneManager.LoadScene(2);
    }
}
