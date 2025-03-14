using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Pointcounter : MonoBehaviour
{
    public static Pointcounter instance;

    public TextMeshProUGUI scoreText;
    int score = 0;
    private void Awake()
    {
     instance = this;
    }
    void Start()
    {
        scoreText.text = score.ToString() + "POINTS";

    }


    public void AddPoint()
    {
        score += 1;
        scoreText.text = score.ToString() + "POINTS";
    }
}
