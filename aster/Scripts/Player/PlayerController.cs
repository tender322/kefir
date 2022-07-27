using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float gasForce;
    [SerializeField] private float speedAmmo;
    [SerializeField] private int laserCountAttack;
    [SerializeField] private float LaserCD;
    public Controller _control { get; protected set; }
    public Weapon _weapon { get; protected set; }
    private PlayerInputActions _playerInputActions;
    private ControlPositionPlayer _controlPositionPlayer;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        _control = new Controller(this.transform,gasForce,_playerInputActions );
        _weapon = new Weapon(transform.GetChild(0),speedAmmo,_playerInputActions ,laserCountAttack,LaserCD);
        _controlPositionPlayer = new ControlPositionPlayer(transform);
        GlobalEventManager.EndGame.AddListener(endGame);
        GlobalEventManager.Restart.AddListener(startGame);
    }

    private void FixedUpdate()
    {
        _control.Update();
        _weapon.Update();
    }

    private void OnBecameInvisible()=> _controlPositionPlayer.Teleportation();

    private void endGame()
    {
        _control.EndGame();
        _weapon.EndGame();
    }

    private void startGame()
    {
        _control.StartGame();
        _weapon.StartGame();
    }
}
