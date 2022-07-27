using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent EnemyKill = new UnityEvent();
    public static UnityEvent PlayerKill= new UnityEvent();
    public static UnityEvent Restart = new UnityEvent();
    public static UnityEvent EndGame = new UnityEvent();
}

