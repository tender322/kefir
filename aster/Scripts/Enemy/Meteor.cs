using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Enemy
{
    
    protected Vector2 targetpos;
    protected Transform thisTransform;
    protected float speed;
    protected int health;


    public override void Init(Transform thisTransform, float speed, SpriteRenderer _spriteRenderer, int health,
        Vector2 targetpos = default,
        Transform targetTransform = default)
    {
        base.Init(thisTransform, speed, _spriteRenderer, health, targetpos, targetTransform);
        _spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/meteor");
        this.health = health;
        this.speed = speed;
        this.thisTransform = thisTransform;
        this.targetpos = targetpos;
        setHealth();
    }


    private void setHealth()
    {
        switch (health)
        {
            case 3:
                thisTransform.localScale = new Vector3(1, 1, 1);
                break;
            case 2:
                thisTransform.localScale = new Vector3(.65f, .65f, .65f);
                break;
            case 1:
                thisTransform.localScale = new Vector3(.25f, .25f, .25f);
                break;
        }
    }


    public override void Move()
    {
        base.Move();
        thisTransform.position = Vector2.MoveTowards(thisTransform.position,targetpos,speed*Time.fixedDeltaTime);
        
        if (new Vector2(thisTransform.position.x,thisTransform.position.y) == targetpos)
            PoolManager.Instance.Despawn(thisTransform.gameObject);
    }

    public override void Trigger(Collider2D other)
    {
        base.Trigger(other);
        switch (other.tag)
        {
            case "AmmoPoint":
                health -= 1;
                if (health > 0)
                {
                    for (int i = 1; i > -2; i-=2)
                    {
                        float distance = Vector2.Distance(thisTransform.position, targetpos);
                        float y = distance * Mathf.Sin(i*30);
                        float x = distance * Mathf.Cos(i*30);
                        Vector2 targetPosNext = new Vector2(x,y);
                        EnemyController enemy = PoolManager.Instance.Spawn(thisTransform.gameObject, thisTransform.position, Quaternion.identity).GetComponent<EnemyController>();
                        enemy.Init(speed+speed*.1f,new Meteor(), health,targetPosNext);
                    }
                }
           
                PoolManager.Instance.Despawn(thisTransform.gameObject);
                PoolManager.Instance.Despawn(other.gameObject);
                GlobalEventManager.EnemyKill.Invoke();

                break;
            case "AmmoLaser":
                PoolManager.Instance.Despawn(thisTransform.gameObject);
                GlobalEventManager.EnemyKill.Invoke();
                break;
            case "Player":
                GlobalEventManager.PlayerKill.Invoke();
                break;
        }
       
    }
}
