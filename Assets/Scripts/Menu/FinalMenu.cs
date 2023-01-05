using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMenu : MonoBehaviour
{
    public GameObject menu;
    public string playertag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();

        if (player != null)
            menu.SetActive(true);
    }
}
