using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMDead : FSMSingleton<FSMDead>, CharState<Controller>
{
    public void Enter(Controller e)
    {
        print(e.gameObject.name + " State : Dead");
        e.Dead();
    }

    public void Excute(Controller e)
    {
        if (e.gameObject.activeSelf == false)
            return;
    }

    public void Exit(Controller e)
    {

    }
}
