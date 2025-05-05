using UnityEditor.Search;
using UnityEngine;
using TMPro; // Importing TextMeshPro for text rendering
using System.Collections.Generic; // Importing System.Collections.Generic for List<T> and other generic collections
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] // SerializeField allows you to set this value in the Unity Inspector
    private TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component for displaying the score
    [SerializeField] // SerializeField allows you to set this value in the Unity Inspector
    private TMP_InputField usernameInputField; // Reference to the TMP_InputField component for entering the username

    public UnityEvent<string, int> submitScoreEvent; // UnityEvent to handle score submission

    private void Start()
    {
      UpdateScoreUI();  
    }

    private void UpdateScoreUI(){
        
            scoreText.text = "Score: " + GameData.FinalScore.ToString(); // Update the score text with the current score
    }


    public void SubmitScore(){

        int currentScore = GameData.FinalScore; // 
        Debug.Log($"Submitting Score: Username = {usernameInputField.text}, Score = {currentScore}");

        submitScoreEvent.Invoke(usernameInputField.text, currentScore); // Invoke the UnityEvent with the username and score
    }
}
