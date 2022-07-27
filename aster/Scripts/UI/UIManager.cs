using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject Player;
    public Text UItextcount;
    public Text UIAngle;
    public Text UIPosition;
    public Text UISpeed;
    public Text UILaserCount;
    public Text UILaserTimer;
    public GameObject EndGamePanel;
    public Text EndGameScore;
    
    
    private Counter _counter;
    private PlayerStats playerStats;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        EndGamePanel.SetActive(false);
        GlobalEventManager.PlayerKill.AddListener(EndGame);
    }

    private void Start()
    {
        _counter = new Counter(UItextcount);
        playerStats = new PlayerStats(Player);
        
    }

    private void FixedUpdate()
    {
        UISpeed.text = "Мгновенная скорость:" + playerStats.getInstSpeed();
    }
    private void LateUpdate()
    {
        UIAngle.text = "Угол:"+playerStats.getRot();
        UIPosition.text = "Позиция:" + playerStats.getPos();
        UILaserCount.text = "Выстрелов с лазера:"+playerStats.getLaserCount();
        UILaserTimer.text = playerStats.getLaserTimer();
    }

    private void EndGame()
    {
        EndGamePanel.SetActive(true);
        EndGameScore.text = "Вы набрали: " + _counter.count.ToString();
        PoolManager.Instance.Restart();
        GlobalEventManager.EndGame.Invoke();
    }

    public void Restart()
    {
        EndGamePanel.SetActive(false);

        GlobalEventManager.Restart.Invoke();
        Start();
    }
}
