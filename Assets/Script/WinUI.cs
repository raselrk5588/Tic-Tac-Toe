using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class WinUI : MonoBehaviour
{
    public GameObject UiCanvas;
    [SerializeField] private Text WinText;
    public Button restartButton;

    [SerializeField] private Board board;

    void Start()
    {
        restartButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        board.onWinAction += OnWinEvent;
        UiCanvas.SetActive(false);
    }

    private void OnWinEvent(Mark mark, Color color)
    {
        WinText.text = (mark == Mark.None) ? "Nobody Win" : mark.ToString() + " Win";
        WinText.color = color;
        UiCanvas.SetActive(true);
    }
    private void OnDestroy()
    {
        restartButton.onClick.RemoveAllListeners();
        board.onWinAction -= OnWinEvent;
    }
}
