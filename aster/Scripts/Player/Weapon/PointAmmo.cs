using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAmmo :Ammo
{
    public override void setView(SpriteRenderer _spriteRenderer)
    {
        base.setView(_spriteRenderer);
        _spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Circle");
        _spriteRenderer.color = Color.white;
        GameObject obj = _spriteRenderer.gameObject;
        if(obj.GetComponent<Collider2D>())
            GameObject.Destroy(obj.GetComponent<Collider2D>());
        
        obj.AddComponent<CircleCollider2D>().isTrigger = true;
        obj.tag = "AmmoPoint";
        obj.transform.localScale = new Vector3(.075f, .075f, 1);
    }
    
}
