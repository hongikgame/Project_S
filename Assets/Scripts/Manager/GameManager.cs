using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonobehavior<GameManager>
{
    public bool Debug = true;



    protected override void Awake()
    {
        base.Awake();
    }

}
