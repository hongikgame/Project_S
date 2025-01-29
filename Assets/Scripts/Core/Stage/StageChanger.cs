using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class StageChanger : MonoBehaviour
{
    public Collider2D Collider { get; private set; }
    public int Index { get; set; }

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StageManager.SetStage(Index + 1);
        }
    }
}
