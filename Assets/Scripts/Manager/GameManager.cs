using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonobehavior<GameManager>
{


    private bool _hallucination = false;
    public bool Hallucination
    {
        get { return _hallucination; }
        set
        {
            _hallucination = value;
            EventHandler.CallPlayerInHallucination(_hallucination);
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
