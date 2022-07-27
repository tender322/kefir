using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   private Enemy _enemy;
   private CircleCollider2D _circleCollider2D;
   private void Start()
   {
      _circleCollider2D = GetComponent<CircleCollider2D>();
      _circleCollider2D.enabled = false;
   }

   public void Init(float speed,Enemy typeEnemy, int health, Vector2 targetPos = default, Transform targetTransofrm = default)
   {
      _enemy = typeEnemy;
      _enemy.Init(this.transform,speed,GetComponent<SpriteRenderer>(),health,targetPos,targetTransofrm);
   }

   private void FixedUpdate()
   {
      _enemy.Move();
   }
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      _enemy.Trigger(other);
   }

   private void OnBecameVisible() => _circleCollider2D.enabled = true;
}
