using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : Enemy
{
    protected Transform target;
    protected Transform thisTransform;
    protected float speed;
    
    public override void Init(Transform thisTransform, float speed, SpriteRenderer _spriteRenderer, int health, Vector2 targetPos = default,
        Transform targetTransform = default)
    {
        base.Init(thisTransform, speed, _spriteRenderer, health, targetPos, targetTransform);
        _spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ufo");
        this.thisTransform = thisTransform;
        this.speed = speed;
        this.target = targetTransform;
        thisTransform.localScale=new Vector3(1,1,1);
    }

    public override void Move()
    {
        base.Move();

        thisTransform.position = Vector2.MoveTowards(thisTransform.position,target.position,speed*Time.fixedDeltaTime);
    }

    public override void Trigger(Collider2D other)
    {
        base.Trigger(other);
        switch (other.tag)
        {
            case "AmmoPoint":
                PoolManager.Instance.Despawn(other.gameObject);
                PoolManager.Instance.Despawn(thisTransform.gameObject);
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
