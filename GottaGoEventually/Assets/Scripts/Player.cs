using UnityEngine;
using System.Collections;

public class Player : Person {

    override protected void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        if (GameManager.instance.phoneDown)
            isMoving = true;
    }

}
