using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using TMPro;
public class GameManager : MonoBehaviour
{
    public delegate void GameManagerEvent();
    public static event GameManagerEvent OnInspectionClosed;

    public static GameManager instance = null;

    [SerializeField] private GameObject inspectionPanel;
    [SerializeField] private Transform inspectionParent;
    [SerializeField] private List<Sprite> collectableSprites;

    [SerializeField] private GameObject hintObj;
    [SerializeField] private TMP_Text message;

    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private TMP_Text finalMessage;

    private TMP_Text hintText;

    private GameObject objectInstance;

    [SerializeField] private int limitSeconds;
    [SerializeField] private TMP_Text timeText;
    public bool escaped;

    private int time;
    private string timeFormatted;

    private bool isPaused;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

     
    }

    private void Start()
    {
        hintText = hintObj.GetComponent<TMP_Text>();
        hintText.text = string.Empty;

        message.text = string.Empty;

        inspectionPanel.SetActive(false);

        mainCanvas.SetActive(false);

        print(mainCanvas.activeSelf);

        isPaused = false;

        StartCoroutine(BombTimer());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }

            isPaused = !isPaused;
        }
    }
    #region INSPECTION
    public void InspectObject(GameObject objectToInspect, string hint)
    {
        PlayerController.instance.SetMovePermission(false);

        inspectionPanel.SetActive(true);

        objectInstance = Instantiate(objectToInspect, inspectionParent);

        hintObj.SetActive(true);
        hintText.text = hint;
    }

    public void EndInteraction()
    {
        hintText.text = string.Empty;
        inspectionPanel.SetActive(false);
        hintObj.SetActive(false);
        PlayerController.instance.SetMovePermission(true);

        if (objectInstance != null)
            Destroy(objectInstance);

        objectInstance = null;

        OnInspectionClosed?.Invoke();
    }


    public Sprite GetCollectableSprite(CollectableTypes type)
    {
        return collectableSprites[(int)type];
    }
    #endregion

    #region BASIC_FUNCTIONS
    public void ShowErrorMessage(string msg)
    {
        message.text = msg;

        StartCoroutine(MessageTimer());
    }

    private IEnumerator MessageTimer()
    {
        yield return new WaitForSeconds(3);
        message.text = string.Empty;
    }

    public void ShowMainCanvas(string message)
    {
        mainCanvas.SetActive(true);
        finalMessage.text = message;
        PlayerController.instance.SetMovePermission(false);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        ShowMainCanvas("PAUSED");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Retry()
    {
        mainCanvas.SetActive(false);
        SceneManager.LoadScene("EscapeRoom");
    }

    private IEnumerator BombTimer()
    {
        time = 0;
        while (time < limitSeconds && !escaped)
        {
            yield return new WaitForSeconds(1);
            time++;
            TimeSpan timeLeft = TimeSpan.FromSeconds(limitSeconds) - TimeSpan.FromSeconds(time);

            string timeFormatted = string.Format("{0:D2}:{1:D2}:{2:D2}",
                           timeLeft.Hours,
                           timeLeft.Minutes,
                           timeLeft.Seconds);

            timeText.text = $"The bomb will explode in: {timeFormatted}";
        }

        if (escaped)
            ShowMainCanvas("YOU ESCAPED");
        else
            ShowMainCanvas("YOU DIED");

    }
    #endregion
}
