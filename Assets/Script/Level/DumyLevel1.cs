using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DumyLevel1 : LevelMaker {


    void Start()
    {
        Vector2 pos = new Vector2();

        List<GameObject> objList = VEasyPoolerManager.GetObjectListRequest("CustomError1", 3, false);

        float dirToPlayer = VEasyCalculator.GetDirection(pos, Player.player.transform.position);

        StartCoroutine(ActionEnemyTrain(objList, new Vector2(), 1f, dirToPlayer, 0f));
    }
}
