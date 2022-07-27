using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
 
    public virtual void Init(Transform thisTransform,float speed, SpriteRenderer _spriteRenderer,
        int health,Vector2 targetPos = default,Transform targetTransform=default) { }
    
    public virtual void Trigger(Collider2D other) { }

    public virtual void Move() { }
}
