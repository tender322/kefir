using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats
{
  private GameObject Player;
  private Vector2 lastPos;
  private Weapon weapon;
  
  public PlayerStats( GameObject Player)
  {
    this.Player = Player;
    lastPos = Player.transform.position;
    weapon = Player.GetComponent<PlayerController>()._weapon;
  }

  public int getRot()
  {
    int rot = Mathf.CeilToInt(Player.transform.localEulerAngles.z);
    if (rot > 180)
      return 360 - rot;
    return -rot;
  }

  public string getPos()
  {
    string x = Player.transform.position.x.ToString("0.0");
    string y = Player.transform.position.y.ToString("0.0");
    return $"X:{x} Y:{y}";
  }

  public string getInstSpeed()
  {
    
    float distance = Vector2.Distance(lastPos, Player.transform.position);
    lastPos = Player.transform.position;
    return (distance / Time.fixedDeltaTime).ToString("0.0");
    
  }

  public string getLaserCount()
  {
    return weapon.laserAttackCount.ToString();
  }

  public string getLaserTimer()
  {
    if (weapon.laserAttackCount < 3)
      return "Перезарядка:" + weapon.laserCD.ToString("0.0");
    else return "";
  }

}
