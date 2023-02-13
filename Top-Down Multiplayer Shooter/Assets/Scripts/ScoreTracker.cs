using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public int playerOne = 0;
    public int playerTwo = 0;
    [SerializeField] TextMeshProUGUI playerOneScore;
    [SerializeField] TextMeshProUGUI playerTwoScore;

    // Update is called once per frame
    void Update()
    {
        playerOneScore.text = $"P1: {playerOne}";
        playerTwoScore.text = $"P2: {playerTwo}";
    }
}
