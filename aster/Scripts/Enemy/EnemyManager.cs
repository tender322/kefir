using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private float TimeForSpawnUFO;
    [SerializeField] private float TimeForSpawnMeteors;
    [SerializeField] private float speedUFO;
    [SerializeField] private float speedMeteor;
    [SerializeField] private float difSpeed;
    private GameObject fab;
    private Transform Player;
    Vector2 minScreenSize;
    Vector2 maxScreenSize;
    private bool running;


    private void Start()
    {
        running = true;
        minScreenSize = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));
        maxScreenSize = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));
        Player = GameObject.FindWithTag("Player").transform;
        fab = Resources.Load<GameObject>("Fabs/Enemy");
        InvokeRepeating("SpawnUFO",0,TimeForSpawnUFO);
        InvokeRepeating("SpawnMeteor",0,TimeForSpawnMeteors);
        GlobalEventManager.Restart.AddListener(startGame);
        GlobalEventManager.EndGame.AddListener(stopGame);
    }

    private void startGame() => running = true;
    private void stopGame() => running = false;

    private int GetSpawnZone(int notZone = default)
    {
        int zone = Random.Range(1, 5);
        if (zone == notZone)
        {
            GetSpawnZone(notZone);
        }
        return zone;
    }
    private Vector2 Spawn(int spawnZone)
    {
    


        float x;
        float y;
        switch (spawnZone)
        {
            case 1:
                x = minScreenSize.x - 1;
                y = Random.Range(minScreenSize.y, maxScreenSize.y);
                return new Vector2(x,y);
            case 2:
                x = Random.Range(minScreenSize.x, maxScreenSize.x);
                y = maxScreenSize.y + 1;
                return new Vector2(x,y);
            case 3: 
                x = maxScreenSize.x + 1;
                y = Random.Range(minScreenSize.y, maxScreenSize.y);
                return new Vector2(x,y);
            case 4:
                x = Random.Range(minScreenSize.x, maxScreenSize.x);
                y = minScreenSize.y - 1;
                return new Vector2(x,y);
        }

        return new Vector2();

    }

    private void SpawnUFO()
    {
        if (running)
        {
            Vector2 pos = Spawn(GetSpawnZone());
            GameObject enemy = PoolManager.Instance.Spawn(fab, pos, Quaternion.identity);
            enemy.GetComponent<EnemyController>().Init(speedUFO, new UFO(), 1, default, Player);
        }
    }

    private void SpawnMeteor()
    {
        if (running)
        {
            int spawnPos = GetSpawnZone();
            int targetPos = GetSpawnZone(spawnPos);

            Vector2 pos = Spawn(spawnPos);
            Vector2 target = Spawn(targetPos);
            int health = Random.Range(1, 4);
            float speed = Random.Range(speedMeteor - difSpeed, speedMeteor + difSpeed);
            GameObject enemy = PoolManager.Instance.Spawn(fab, pos, Quaternion.identity);
            enemy.GetComponent<EnemyController>().Init(speed, new Meteor(), health, target, default);
        }
    }
}
