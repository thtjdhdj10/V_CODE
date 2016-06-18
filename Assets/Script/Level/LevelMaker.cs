using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelMaker : MonoBehaviour {

    public static LevelMaker currentLevel;

    public float clearDegree = 0f; // 0~1

    void Awake()
    {
        currentLevel = this;
    }

    public static void ActiveWithComponens(GameObject obj)
    {
        MonoBehaviour[] components = obj.GetComponentsInChildren<MonoBehaviour>();
        for(int i = 0 ; i<components.Length;++i)
        {
            components[i].enabled = true;
        }
    }

    public static IEnumerator ActionEnemyCircle(List<GameObject> objectList, Vector2 pos, float delay, float direction, float angle, float distance)
    {
        float deltaAngle = angle / (float)objectList.Count;

        float currentAngle = direction;

        Vector2 deltaPos = VEasyCalculator.GetRotatedPosition(direction, distance);

        Vector2 currentPos = pos + deltaPos;

        for (int i = 0; i < objectList.Count; ++i)
        {
            GameObject obj = objectList[i];

            obj.SetActive(true);
            ActiveWithComponens(obj);

            obj.transform.position = currentPos;

            currentAngle += deltaAngle;

            deltaPos = VEasyCalculator.GetRotatedPosition(currentAngle, distance);

            currentPos = pos + deltaPos;

            yield return new WaitForSeconds(delay);
        }

        yield break;
    }

    public static IEnumerator ActionEnemyCircle(List<GameObject> objectList, Vector2 pos, float direction, float angle, float distance)
    {
        float deltaAngle = angle / (float)objectList.Count;

        float currentAngle = direction;

        Vector2 deltaPos = VEasyCalculator.GetRotatedPosition(direction, distance);

        Vector2 currentPos = pos + deltaPos;

        for (int i = 0; i < objectList.Count; ++i)
        {
            GameObject obj = objectList[i];

            obj.SetActive(true);
            ActiveWithComponens(obj);

            obj.transform.position = currentPos;

            currentAngle += deltaAngle;

            deltaPos = VEasyCalculator.GetRotatedPosition(currentAngle, distance);

            currentPos = pos + deltaPos;
        }

        yield break;
    }

    public static IEnumerator ActionEnemyTrain(List<GameObject> objectList, Vector2 pos, float delay, float direction, float distance)
    {
        Vector2 currentPos = pos;
        
        Vector2 deltaPos = VEasyCalculator.GetRotatedPosition(direction, distance);

        for (int i = 0; i < objectList.Count; ++i)
        {
            GameObject obj = objectList[i];

            obj.SetActive(true);
            ActiveWithComponens(obj);

            obj.transform.position = currentPos;

            currentPos += deltaPos;

            yield return new WaitForSeconds(delay);
        }

        yield break;
    }

    public static IEnumerator ActionEnemyTrain(List<GameObject> objectList, Vector2 pos, float delay)
    {
        Vector2 currentPos = pos;

        for (int i = 0; i < objectList.Count; ++i)
        {
            GameObject obj = objectList[i];

            obj.SetActive(true);
            ActiveWithComponens(obj);

            obj.transform.position = currentPos;

            yield return new WaitForSeconds(delay);
        }

        yield break;
    }

    public static IEnumerator ActionEnemyTrain(List<GameObject> objectList, Vector2 pos, float direction, float distance)
    {
        Vector2 currentPos = pos;
        
        Vector2 deltaPos = VEasyCalculator.GetRotatedPosition(direction, distance);

        for (int i = 0; i < objectList.Count; ++i)
        {
            GameObject obj = objectList[i];

            obj.SetActive(true);
            ActiveWithComponens(obj);

            obj.transform.position = currentPos;

            currentPos += deltaPos;
        }

        yield break;
    }
}
