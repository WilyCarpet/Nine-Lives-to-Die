using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using Dan.Main;


public class Leaderboard : MonoBehaviour
{
    [SerializeField] // SerializeField allows you to set this value in the Unity Inspector
    private List<TextMeshProUGUI> names;

    [SerializeField] // SerializeField allows you to set this value in the Unity Inspector
    private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = "cefdcf456d44b80989bb80fe693ba2314f00afb25c2ebd772cbc916bbcbe87a8";

    private void Start()
    {
      GetLeaderboard(); // Call GetLeaderboard to fetch the leaderboard data when the script starts
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GetLeaderboard(){
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count; // Determine the length of the loop based on the number of entries or the number of UI elements
            for(int i = 0; i < loopLength; i++){
                names[i].text = msg[i].Username; // Set the name text to the leaderboard name
                scores[i].text = msg[i].Score.ToString(); // Set the score text to the leaderboard score
            }

        }));

    }

    public void SetLeaderboard(string username, int score){
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) => {
            GetLeaderboard();
        }));
    }
}
