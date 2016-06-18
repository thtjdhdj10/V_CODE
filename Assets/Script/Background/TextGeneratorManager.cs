using UnityEngine;
using System.Collections.Generic;

public class TextGeneratorManager : MonoBehaviour {

    public static TextGeneratorManager manager;

    public float generateTextRootSpeed;
    float passedGenerateDelay;

    public float greenChance;

    public float delay;

    public float alphaDecreaseSpeed; // per sec

    public float highlight;

    public float fontSize;

    public float startAlpha;

    void Awake()
    {
        manager = this;

        passedGenerateDelay = 0f;

        GameObject text = VEasyPoolerManager.GetObjectRequest("GreenText", false);
        UILabel label = text.GetComponent<UILabel>();
        fontSize = label.fontSize;
        startAlpha = label.color.a;
        VEasyPoolerManager.ReleaseObjectRequest(text);
    }

    void FixedUpdate()
    {
        List<Virus> virusList = new List<Virus>(); // TODO get all virus

        float totalCurrentHp = 1f;
        float totalMaxHp = 1f;
        for(int i = 0 ; i < virusList.Count;++i)
        {
            totalCurrentHp += virusList[i].currentAbilityDic[Enemy.BasicAbility.HEALTH_POINT];
            totalMaxHp += virusList[i].originalAbilityDic[Enemy.BasicAbility.HEALTH_POINT];
        }
        // LevelMaker.currentLevel TODO Get clearDegree
//        generateTextRootSpeed

        if(passedGenerateDelay < generateTextRootSpeed)
        {
            passedGenerateDelay += Time.fixedDeltaTime;
        }
        else
        {
            passedGenerateDelay = 0f;

            GameObject textRoot = VEasyPoolerManager.GetObjectRequest("TextRoot");
            textRoot.transform.parent = gameObject.transform;
            Vector3 pos = new Vector3();
            float widthRatio = (float)Screen.width / (float)Screen.height;
            float maxX = ((320f * widthRatio) + fontSize) / 160f;
            pos.x = Random.Range(-maxX, maxX);
            textRoot.transform.position = pos;
        }

//        greenChance = totalCurrentHp / totalMaxHp;
    }

}
