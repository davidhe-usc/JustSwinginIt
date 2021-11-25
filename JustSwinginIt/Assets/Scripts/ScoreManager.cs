using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI text;
    public int score;

    void Awake()
    {
        ScoreManager[] numScoreManager = FindObjectsOfType<ScoreManager>();
        if (numScoreManager.Length > 1)
        {
            for (int i = 0; i < numScoreManager.Length; i++)
            {
                if (numScoreManager[i] != this)
                {
                    ChangeScore(numScoreManager[i].score);
                    Destroy(numScoreManager[i]);
                }
            }
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        if (instance == null)
            instance = this;
    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        text.text = "X" + score.ToString();
    }
}
