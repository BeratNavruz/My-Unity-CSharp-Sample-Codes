using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginApiManager : MonoSingleton<LoginApiManager>
{
    [System.Serializable]
    public class LoginData
    {
        public string userName;
        public string password;
    }

    [System.Serializable]
    public class LoginResponse
    {
        public bool isSuccess;
        public string message;
        public Data data;
    }

    [System.Serializable]
    public class Data
    {
        public int id;
        public string name;
        public string surname;
        public AccessToken accessToken;
    }

    [System.Serializable]
    public class AccessToken
    {
        public string token;
    }

    #region Function Parametres

    string inputUserName;
    string inputPassword;
    Button loginButton;
    AlertDialogueSO alertDialogueSO;
    PostApiManager postApiManager;

    #endregion

    public void StartLogin(string inputUserName, string inputPassword, Button loginButton, AlertDialogueSO alertDialogueSO)
    {
        this.inputUserName = inputUserName;
        this.inputPassword = inputPassword;
        this.loginButton = loginButton;
        this.alertDialogueSO = alertDialogueSO;
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        string username = inputUserName;
        string password = inputPassword;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            alertDialogueSO.AlertDialogueFunc("Lütfen kullanıcı adı ve şifre girin", null);
            loginButton.interactable = true;
            yield break;
        }

        LoginData loginData = new LoginData { userName = username, password = password };

        string jsonData = JsonUtility.ToJson(loginData);

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        using UnityWebRequest www = new UnityWebRequest(ApiData.Instance.loginURL, "POST");
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            alertDialogueSO.AlertDialogueFunc("Giriş başarısız: " + www.error, null);
            loginButton.interactable = true;
        }
        else if (www.result == UnityWebRequest.Result.DataProcessingError)
        {
            alertDialogueSO.AlertDialogueFunc("Veri işleme hatası", null);
            loginButton.interactable = true;
        }
        else
        {
            string responseText = www.downloadHandler.text;

            LoginResponse loginResponse;
            try
            {
                loginResponse = JsonUtility.FromJson<LoginResponse>(responseText);

                Debug.Log("-------------------------------------------------------------------------------------------");
                Debug.Log("UserName: " + loginData.userName + " Password: " + loginData.password);
                Debug.Log("Succes: " + loginResponse.isSuccess);
                Debug.Log("Message: " + loginResponse.message);
                Debug.Log("Id: " + loginResponse.data.id);
                Debug.Log("Id: " + loginResponse.data.name);
                Debug.Log("Id: " + loginResponse.data.surname);
                Debug.Log("Token: " + loginResponse.data.accessToken.token);
                Debug.Log("-------------------------------------------------------------------------------------------");
            }
            catch (System.Exception)
            {
                alertDialogueSO.AlertDialogueFunc("Cevap İşleme Hatası", null);
                loginButton.interactable = true;
                yield break;
            }

            if (loginResponse != null)
            {
                bool isSuccessHere = loginResponse.isSuccess;
                string messages = loginResponse.message;

                if (isSuccessHere)
                {
                    int userId = loginResponse.data.id;
                    string userToken = loginResponse.data.accessToken.token;
                    Debug.Log("Doğru giriş yapıldı. User ID: " + userId + "token: " + userToken);

                    PlayerPrefs.SetInt("userId", userId);
                    PlayerPrefs.SetString("userToken", userToken);

                    PlayerPrefs.Save();
                    Debug.Log("userId saklandı: " + userId + "token: " + userToken);

                    PlayerPrefs.SetString("username", username);
                    PlayerPrefs.SetString("password", password);

                    //G İ R İ Ş   Y A P I L D I
                    postApiManager = PostApiManager.Instance;
                    postApiManager.alertDialogueSO = alertDialogueSO;
                    postApiManager.isPostAPIWait = true;
                    postApiManager.SendTimerData(true);

                    while (postApiManager.isPostAPIWait)
                    {
                        yield return null;
                    }

                    SceneManager.LoadScene("MainMenuScene");
                }
                else
                {
                    alertDialogueSO.AlertDialogueFunc("Hatalı Giriş", null);
                    loginButton.interactable = true;
                }
            }
            else
            {
                alertDialogueSO.AlertDialogueFunc("Sunucudan geçerli bir cevap alınamadı", null);
                loginButton.interactable = true;
            }
        }
    }
}
