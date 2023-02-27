using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    public bool loadNextQuestion;
    public bool isAnsweringQuestion = false;
    public float fillFraction;
    float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    private void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (timerValue <= 0)
        {
            if (!isAnsweringQuestion)
            {
                loadNextQuestion = true;
            }

            isAnsweringQuestion = !isAnsweringQuestion;
            timerValue = isAnsweringQuestion
                ? timeToCompleteQuestion 
                : timeToShowCorrectAnswer;

            return;
        }

        var totalTime = isAnsweringQuestion
            ? timeToCompleteQuestion
            : timeToShowCorrectAnswer;

        fillFraction = timerValue / totalTime;
    }
}
