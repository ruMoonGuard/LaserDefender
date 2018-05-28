using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public int Score;

    private Text text;

	void Start ()
    {
        text = GetComponent<Text>();
        Reset();
	}

    public void ScoreAdd(int value)
    {
        Score += value;
        text.text = Score.ToString();
    }

    public void Reset()
    {
        Score = 0;
        text.text = Score.ToString();
    }
}
