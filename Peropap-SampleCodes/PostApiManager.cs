using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PostApiManager : MonoSingleton<PostApiManager>
{
    [System.Serializable]
    public class TimerData
    {
        public bool openOrClose;
        public int userId;
    }

    int userId;
    string userToken;

    public bool isPostAPIWait;

    public AlertDialogueSO alertDialogueSO;

    protected override void Awake()
    {
        base.Awake();
        userId = PlayerPrefs.GetInt("userId", -1);
        userToken = PlayerPrefs.GetString("userToken", "");
    }

    public void SendTimerData(bool openOrClose)
    {
        TimerData timerData = new()
        {
            openOrClose = openOrClose,
            userId = userId,
        };

        string jsonData = JsonUtility.ToJson(timerData);

        StartCoroutine(PostRequest(ApiData.Instance.timerURL, jsonData));
    }

    private IEnumerator PostRequest(string url, string json, int maxAttempts = 4)
    {
        int attempt = 0;
        while (attempt < maxAttempts)
        {
            attempt++;
            var request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Authorization", "Bearer " + userToken);
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Veri gönderildi: " + request.downloadHandler.text);
                isPostAPIWait = false;
                yield break;
            }
            else
            {
                //Debug.LogWarning($"Deneme {attempt} başarısız: {request.error}");
                yield return new WaitForSeconds(.25f); // bekleyip yeniden dene
            }
        }

        alertDialogueSO.AlertDialogueFunc("İnternet bağlantınızı kontrol ediniz", "LoginScene");
    }

    /*
        private IEnumerator PostRequest(string url, string json)
        {
            Debug.Log("url= " + url);
            Debug.Log("json= " + json);

            var request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Authorization", "Bearer " + userToken);
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Form verileri başarıyla gönderildi!" + request.downloadHandler.text);
                isPostAPIWait = false;
            }
            else
            {
                alertDialogueSO.AlertDialogueFunc("İnternet bağlantınızı kontrol ediniz", "LoginScene");
            }
        }
    */
}
