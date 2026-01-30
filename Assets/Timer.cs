using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timerStartTime = 10.0f;
    private float _timeRemiaing;
    public float TimeRemiaing
    {
        get => _timeRemiaing;
        set
        {
            _timeRemiaing = value;
        }
    }
    private bool timerIsTicking = false;
    public TextMeshProUGUI timerText;

    public void Start()
    {
        StartTimer();
        timerText.text = "--:--:--";
    }

    public void Update()
    {
        if (timerIsTicking)
        {
            if (_timeRemiaing > 0)
            {
                _timeRemiaing -= Time.deltaTime;
                DisplayTime(_timeRemiaing);
            }
            else
            {
                _timeRemiaing = 0.0f;
                StopTimer();
                // TODO: timer elapsed event
            }
        }
    }

    public void StartTimer()
    {
        timerIsTicking = true;
    }

    public void StopTimer()
    {
        timerIsTicking = false;
    }

    public float GetSecondsRemaining()
    {
        return Mathf.FloorToInt(_timeRemiaing % 60);
    }

    public float GetMinutesRemaining()
    {
        return Mathf.FloorToInt(_timeRemiaing / 60);
    }

    private void DisplayTime(float timeToDisplay)
    {
        float seconds = GetSecondsRemaining();
        float minutes = GetMinutesRemaining();
        float milliseconds = (timeToDisplay % 1) * 1000;

        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
