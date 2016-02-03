using UnityEngine;
using System.Collections;

public class Player : Person {


    override protected void Awake()
    {
        base.Awake();
        //animator = GetComponentInChildren<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        if (!GameManager.instance.phoneActive && GameManager.instance.gameStarted)
            isMoving = true;
        else if (GameManager.instance.phoneActive)
            isMoving = false;

    }

    public void startPhone()
    {
        animator.SetBool("Phone", true);
        animator.SetBool("Walking", false);
    }
    public void endPhone()
    {
        animator.SetBool("Phone", false);
    }

}
