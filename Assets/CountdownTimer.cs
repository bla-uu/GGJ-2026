using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.BoolParameter;

public class CountdownTimer : MonoBehaviour
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

    private bool _timerIsTicking = false;
    public bool TimerIsTicking
    {
        get => _timerIsTicking;
    }
    public TextMeshProUGUI timerText;

    public void Start()
    {
        //StartTimer();
        _timeRemiaing = timerStartTime;
        DisplayTime(timerStartTime);
    }

    public void Update()
    {
        if (_timerIsTicking)
        {
            if (_timeRemiaing > 0)
            {
                _timeRemiaing -= Time.deltaTime;
                DisplayTime(_timeRemiaing);
            }
            else
            {
                _timeRemiaing = 0.0f;
                DisplayTime(_timeRemiaing);
                StopTimer();
                StartCoroutine(LoadLevelFailedSceneAsyncScene());
                // TODO: timer elapsed event
            }
        }
    }

    public void StartTimer()
    {
        _timerIsTicking = true;
    }

    public void StopTimer()
    {
        _timerIsTicking = false;
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

        string text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);

        timerText.text = text;
    }

    IEnumerator LoadLevelFailedSceneAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelFailedScene");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
