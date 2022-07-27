using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAmmo : Ammo
{
    public override void setView(SpriteRenderer _spriteRenderer)
    {
        base.setView(_spriteRenderer);
        _spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Capsule");
        _spriteRenderer.color = Color.red;
        GameObject obj = _spriteRenderer.gameObject;
        if(obj.GetComponent<Collider2D>())
            GameObject.Destroy(obj.GetComponent<Collider2D>());
        obj.AddComponent<CapsuleCollider2D>().isTrigger = true;
        obj.tag = "AmmoLaser";
        obj.transform.localScale = new Vector3(.025f, 0.1f, 1);
    }
}
