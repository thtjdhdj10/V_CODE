using UnityEngine;
using System.Collections.Generic;

public class SetRandomUnicodeText : MonoBehaviour {

    float passedDelay;

    Vector2 cursor;

    List<GameObject> labelObjectList = new List<GameObject>();
    List<UILabel> labelList = new List<UILabel>();

    int textCount;

    void Start()
    {
        passedDelay = 0f;

        cursor = transform.position;
        cursor.y = (320f - TextGeneratorManager.manager.fontSize * 0.5f) / 160f;
        transform.position = cursor;

        // textConut = (int)(320f / TextGeneratorManager.manager.fontSize) + 1;
	}

    string GetRandomCharacter()
    {
        string chars = "";
        //        chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        chars += "§※＠＃＆†♬º™№㏇";
        chars += "ⅨⅧⅦⅥⅣⅢ";
        chars += "±≠∞∴♂♀∂∇≡≒√∝∵∫∬∈∋⊆⊇∪⇒⇔∀∃∮∑∏";
        chars += "ÆÐĦĲĿŁØŒÞŦŊæđðŁØĳĸŀłøœßþŧŋŉ";
        chars += "＄％￦Ｆ′″℃Å￠￡￥¤℉‰";
        chars += "БГДЖЗЙЛФЦЧШЩЪЫЬЭЮЯ";
        chars += "ΓΔΘΛΜΝΞΟΠΣΦΧΨΩαβγδεζηθικλμνξοπρστυφχψω";

        int num = Random.Range(0, chars.Length - 1);
        return chars[num].ToString();
    }

    void AddText()
    {
        float dice = Random.Range(0f,1f);
        GameObject obj;
        if(dice < TextGeneratorManager.manager.greenChance)
        {
            obj = VEasyPoolerManager.GetObjectRequest("GreenText");
        }
        else
        {
            obj = VEasyPoolerManager.GetObjectRequest("RedText");
        }

        obj.transform.parent = transform.parent;

        obj.transform.position = cursor;
        cursor.y -= TextGeneratorManager.manager.fontSize / 160f;

        UILabel label = obj.GetComponent<UILabel>();

        label.text = GetRandomCharacter();

        Color c = label.color;
        c.a = TextGeneratorManager.manager.startAlpha;
        label.color = c;

        labelObjectList.Add(obj);
        labelList.Add(label);
    }

    void FixedUpdate()
    {
        if(passedDelay < TextGeneratorManager.manager.delay)
        {
            passedDelay += Time.fixedDeltaTime;
        }
        else
        {
            passedDelay = 0f;

            if (cursor.y >= -(320f + TextGeneratorManager.manager.fontSize * 2f) / 160f)
            {
                AddText();
            }
        }

        SetTextAlpha();
        if (cursor.y < -2f)
        {
            if(labelObjectList.Count == 0)
            {
                VEasyPoolerManager.ReleaseObjectRequest(gameObject);
            }
        }
    }

    void SetTextAlpha()
    {
        List<int> deleteObjectIndexList = new List<int>();

        for(int i = 0; i <labelList.Count; ++i)
        {
            Color c = labelList[i].color;

            // 마지막 문자 하이라이트
            if (i == labelList.Count - 1)
            {
                c.a = TextGeneratorManager.manager.highlight;
            }
            else
            {
                c.a = Mathf.Min(c.a, TextGeneratorManager.manager.startAlpha);
            }

            c.a -= TextGeneratorManager.manager.startAlpha *
                TextGeneratorManager.manager.alphaDecreaseSpeed * Time.fixedDeltaTime;

            labelList[i].color = c;

            if(c.a < 0.05f)
            {
                // alpha 가 일정치 이하로 내려가면 지움
                deleteObjectIndexList.Add(i);
            }
        }

        // 뒤에서부터 지움
        for(int i = deleteObjectIndexList.Count - 1; i >= 0; --i)
        {
            int idx = deleteObjectIndexList[i];

            labelList.RemoveAt(idx);
            VEasyPoolerManager.ReleaseObjectRequest(labelObjectList[idx]);
            labelObjectList.RemoveAt(idx);
        }
    }
}
