using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSpawn : FSMSingleton<FSMSpawn>, CharState<Controller>
{
    public void Enter(Controller e)
    {
        print(e.gameObject.name + " Sapwned");
        e.spawnElapsed = 0;
    }

    public void Excute(Controller e)
    {
        e.spawnElapsed += Time.deltaTime;
        if (e.spawnElapsed > e.spawnTime)
        {
            if (e.unitType == UnitType.Player)
                e.ChangeState(FSMPossess.Instance);
            else
                e.ChangeState(FSMPatrol.Instance);
        }
    }

    public void Exit(Controller e)
    {
        print(e.gameObject.name + " Sapwn end");
    }
}
