using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;

    public Button sumbitButton;

    public void StartLogin()
    {
        StartCoroutine(LoginIE());
    }

    IEnumerator LoginIE()
    {
        string post_url = "http://localhost/SQLUnity/login.php";

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("Username", usernameInput.text));
        formData.Add(new MultipartFormDataSection("User_pass", passwordInput.text));

        Debug.Log(usernameInput.text);
        Debug.Log(passwordInput.text);

        UnityWebRequest www = UnityWebRequest.Post(post_url, formData);
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            Debug.Log(www.error);
            //Debug.Log("Error get: " + www.downloadHandler.text);
        }
        else if(www.downloadHandler.text == "false")
        {
            Debug.Log("Player well connected");
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
