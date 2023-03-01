using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite defautlAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
        //DisplayQuestion();
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            GetNextQuestion();
            timer.loadNextQuestion = false;
        } else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {

        }
    }

    public void OnAnswerSelected(int index)
    {
        correctAnswerIndex = question.GetCorrectAnswerIndex();
        Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();

        if (index.Equals(correctAnswerIndex))
        {
            questionText.text = "Correct!";
        } 
        else
        {
            var correctAnswerText = answerButtons[correctAnswerIndex].GetComponentInChildren<TextMeshProUGUI>();
            questionText.text = $"The correct answer was: {correctAnswerText.text}";
        }

        buttonImage.sprite = correctAnswerSprite;
        UpdateButtonState(false);
        timer.CancelTimer();
    }

    private void GetNextQuestion()
    {
        UpdateButtonState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
    }

    private void DisplayQuestion()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    private void UpdateButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    private void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defautlAnswerSprite;
        }
    }
}
