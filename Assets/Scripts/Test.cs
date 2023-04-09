using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public NavMeshSurface player;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            mapGenerator.GenerateMap();
            player.BuildNavMesh();
        }
    }
}
