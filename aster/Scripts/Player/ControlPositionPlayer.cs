using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPositionPlayer
{
    
    private Transform player;

    public ControlPositionPlayer(Transform player)
    {
        this.player = player;
    }
    public void Teleportation()
    {
        if (player)
        {
            Vector2 start = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 end = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            if (player.position.y > start.y && player.position.y < end.y)
                player.position = player.position.x < start.x
                    ? player.position = new Vector2(end.x, player.position.y)
                    : player.position = new Vector2(start.x, player.position.y);
            else
                player.position = player.position.y < start.y
                    ? player.position = new Vector2(player.position.x, end.y)
                    : player.position = new Vector2(player.position.x, start.y);
        }
    }
    
}
