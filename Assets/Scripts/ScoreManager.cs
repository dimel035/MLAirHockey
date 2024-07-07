using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int RedPoints;
    public int BluePoints;

    [SerializeField] private Text scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        RedPoints = 0;
        BluePoints = 0;
        UpdateScoreText();
    }

    public void AddRedPoint()
    {
        RedPoints += 1;
        UpdateScoreText();
    }

    public void AddBluePoint()
    {
        BluePoints += 1;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + BluePoints + " - " + RedPoints;
        }
    }
}
