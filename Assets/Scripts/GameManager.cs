using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public int score;
    public int highscore;
    public GameObject scoreText;
    public GameObject highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        highscore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int addScore)
    {     
        score = (score + addScore);
        scoreText.GetComponent<TextMeshProUGUI>().text = (score.ToString());
    }
}
