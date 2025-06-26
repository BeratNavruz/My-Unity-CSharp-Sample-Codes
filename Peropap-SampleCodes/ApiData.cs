public class ApiData : MonoSingleton<ApiData>
{
    string api = "https://peropap.com";

    public string loginURL = "/api/auth/Login";
    public string timerURL = "/api/user/AppOpenOrClose";

    protected override void Awake()
    {
        base.Awake();
        APIAssignment();
    }

    void APIAssignment()
    {
        loginURL = api + loginURL;
        timerURL = api + timerURL;
    }
}
