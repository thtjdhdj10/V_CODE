using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DumyLevel1 : LevelHoldOut
{


    void Start()
    {
        clearTime = 20f;
//        Vector2 pos = new Vector2();

//        List<GameObject> objList = VEasyPoolerManager.GetObjectListRequest("CustomError1", 3, false);

//        float dirToPlayer = VEasyCalculator.GetDirection(pos, Player.player.transform.position);

//        StartCoroutine(ActionEnemyTrain(objList, new Vector2(), 1f, dirToPlayer, 0f));

        StartCoroutine(Stage());
    }

    IEnumerator Stage()
    {
        passedTime = 0f;

        string type = "CustomError4";
        int count = 0;
        float maxX = ((float)Screen.width / (float)Screen.height) * 2f;
        float maxY = 2f;
        Vector2 pos;
        float delay = 1f;
        float direction = 0f;
        float distance = 0f;

        count = 4;
        List<GameObject> objList = VEasyPoolerManager.GetObjectListRequest(type, count, false);
        pos = new Vector2(-maxX, maxY);
        delay = 1f;
        direction = 0f;
        distance = 0f;
        StartCoroutine(ActionEnemyTrain(objList, pos, delay, direction, distance));

        yield return new WaitForSeconds((float)count * delay);

        objList = VEasyPoolerManager.GetObjectListRequest(type, count, false);
        pos = new Vector2(maxX, maxY);
        StartCoroutine(ActionEnemyTrain(objList, pos, delay, direction, distance));

        yield return new WaitForSeconds((float)count * delay);

        yield return new WaitForSeconds(2.5f);

        delay = 0.5f;

        count = 2;

        objList = VEasyPoolerManager.GetObjectListRequest(type, count, false);
        pos = new Vector2(-maxX, maxY);
        StartCoroutine(ActionEnemyTrain(objList, pos, delay, direction, distance));

        objList = VEasyPoolerManager.GetObjectListRequest(type, count, false);
        pos = new Vector2(maxX, maxY);
        StartCoroutine(ActionEnemyTrain(objList, pos, delay, direction, distance));

        yield return new WaitForSeconds((float)count * delay);

        //
        yield return new WaitForSeconds(10f);

        type = "CustomError2";

        count = 2;

        objList = VEasyPoolerManager.GetObjectListRequest(type, count, false);
        pos = new Vector2(-maxX, maxY);
        StartCoroutine(ActionEnemyTrain(objList, pos, delay, direction, distance));

        objList = VEasyPoolerManager.GetObjectListRequest(type, count, false);
        pos = new Vector2(maxX, maxY);
        StartCoroutine(ActionEnemyTrain(objList, pos, delay, direction, distance));

        objList = VEasyPoolerManager.GetObjectListRequest(type, count, false);
        pos = new Vector2(-maxX, -maxY);
        StartCoroutine(ActionEnemyTrain(objList, pos, delay, direction, distance));

        objList = VEasyPoolerManager.GetObjectListRequest(type, count, false);
        pos = new Vector2(maxX, -maxY);
        StartCoroutine(ActionEnemyTrain(objList, pos, delay, direction, distance));

        yield return new WaitForSeconds((float)count * delay);

        yield break;
    }
}
