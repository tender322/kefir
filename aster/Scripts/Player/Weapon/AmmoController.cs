using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AmmoController :MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private Ammo _ammo;
    public void Init(Vector2 position, float speed,Vector2 direction, Ammo ammo)
    {
        transform.position = position;
        this.speed = speed;
        this.direction = direction;
        _ammo = ammo;
        _ammo.setView(gameObject.GetComponent<SpriteRenderer>());
    }
    
    private void FixedUpdate()
    {
        transform.position = (new Vector2(transform.position.x,transform.position.y) + direction * speed);
    }
    

    private void OnBecameInvisible()
    {
        if(gameObject.activeInHierarchy)
            PoolManager.Instance.Despawn(this.gameObject);
    }
}

