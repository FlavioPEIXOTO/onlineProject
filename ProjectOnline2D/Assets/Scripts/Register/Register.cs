using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;

    public Button sumbitButton;

    public void StartRegister()
    {
        StartCoroutine(RegisterIE());
    }

    IEnumerator RegisterIE()
    {
        string post_url = "http://localhost/SQLUnity/register.php";

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
        }
        else if(www.downloadHandler.text == "false")
        {
            Debug.Log("Form upload complete!");
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("Player Creation failed : " + www.downloadHandler.text);
        }
    }

    public void InputValidation()
    {
        sumbitButton.interactable = (usernameInput.text.Length >= 3 && usernameInput.text.Length <= 16 && passwordInput.text.Length >= 8);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
