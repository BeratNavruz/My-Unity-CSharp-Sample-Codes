using TMPro;
using UnityEngine;

public class ScoreBoardControl : MonoBehaviour
{
    [SerializeField] TextMeshPro _homeText;
    [SerializeField] TextMeshPro _awayText;

    int _playerTeamScore = 0;
    int _opponentTeamScore = 0;

    private void Start()
    {
        _homeText.text = _playerTeamScore.ToString();
        _awayText.text = _opponentTeamScore.ToString();
    }

    public void UpdateScoreBoard(string teamName)
    {
        if (teamName == "PlayerTeam")
        {
            _playerTeamScore++;
            _homeText.text = _playerTeamScore.ToString();
        }
        else if (teamName == "OpponentTeam")
        {
            _opponentTeamScore++;
            _awayText.text = _opponentTeamScore.ToString();
        }
    }
}
