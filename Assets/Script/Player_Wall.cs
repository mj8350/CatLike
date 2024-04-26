using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Wall : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        if (!transform.root.TryGetComponent<Player>(out player))
            Debug.Log("Player_Wall.cs - Awake() - player참조 오류");
    }

    private void Update()
    {
        transform.position = player.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            player.wall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            player.wall = false;
        }
    }
}
