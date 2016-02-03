using UnityEngine;
using System.Collections;

public class Player : Person {

    new Animator animator;

    override protected void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
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
        animator.SetBool("Walking", false);
    }
    public void endPhone()
    {
        animator.SetBool("Phone", false);
    }

}
