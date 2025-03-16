using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Pointcounter : MonoBehaviour
{
    public static Pointcounter instance;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI depthText;

    float depthScore = 0;
    int score = 0;
    private void Awake()
    {
     instance = this;
    }
    void Start()
    {
        UpdateScore();

    }


    public void UpdateScore() {
        scoreText.text = score.ToString() + "POINTS";
    }

    public void AddPoint()
    {
        score += 1;
        scoreText.text = score.ToString() + "POINTS";
        Launcher.stats.IncreaseStats();
    }

    public void DepthPoints(float depth) {
        depthText.text = " ";
        depthScore = depth;

        string depthString = depthScore.ToString("F0");

        depthText.text = depthString; 
    }


}
