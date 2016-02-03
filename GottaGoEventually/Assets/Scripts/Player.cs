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
        if (GameManager.instance.phoneDown && GameManager.instance.gameStarted)
            isMoving = true;

    }

    public void startPhone()
    {
        animator.SetBool("Phone", true);
    }
    public void endPhone()
    {
        animator.SetBool("Phone", false);
    }

}
