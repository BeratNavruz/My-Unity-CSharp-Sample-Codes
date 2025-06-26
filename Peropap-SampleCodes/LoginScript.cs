using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{
    [SerializeField] TMP_InputField userNameInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] Button loginButton;
    public AlertDialogueSO alertDialogueSO;
    LoginApiManager loginApiManager;

    void Awake()
    {
        if (PlayerPrefs.HasKey("userId") && PlayerPrefs.HasKey("username") && PlayerPrefs.HasKey("password"))
        {
            loginButton.interactable = false;
            LoginApiManager.Instance.StartLogin(PlayerPrefs.GetString("username"), PlayerPrefs.GetString("password"), loginButton, alertDialogueSO);
        }
    }

    private void Start()
    {
        loginApiManager = LoginApiManager.Instance;
        loginButton.onClick.AddListener(LoginButton);
    }

    void LoginButton()
    {
        loginButton.interactable = false;
        loginApiManager.StartLogin(userNameInput.text, passwordInput.text, loginButton, alertDialogueSO);
    }
}
