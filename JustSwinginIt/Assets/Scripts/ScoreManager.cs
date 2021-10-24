using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI text;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("scoremanage start function");

        if (instance == null)
            instance = this;
    }

    public void ChangeScore(int coinValue)
    {
        UnityEngine.Debug.Log("changescore function here");
        score += coinValue;
        text.text = "X" + score.ToString();
    }
}
