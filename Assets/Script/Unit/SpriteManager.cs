using UnityEngine;
using System.Collections.Generic;

using TypeSpriteDictionary = 
System.Collections.Generic.Dictionary<SpriteManager.Category,
System.Collections.Generic.Dictionary<SpriteManager.Name,
System.Collections.Generic.Dictionary<SpriteManager.Status, SpriteManager.SpriteAttribute>>>;

public class SpriteManager : MonoBehaviour {

    public const bool PRINT_DEBUG = true;

    public int countLoadSprite;

    public static float spriteDefaultRotation = -90f;

    public static int spriteDefaultFramePerSec = 12;

    public static SpriteManager manager;

    void Awake()
    {
        manager = this;

        LoadSprite();
    }

    public TypeSpriteDictionary typeSpriteDic = new TypeSpriteDictionary();

    public struct SpriteAttribute
    {
        public Sprite sprite;
        public float speed;
        public int frameCount;
        public float cycle;
    }

    private Dictionary<string, Category> strCategoryDic = new Dictionary<string, Category>();
    private Dictionary<string, Name> strNameDic = new Dictionary<string, Name>();
    private Dictionary<string, Status> strStatusDic = new Dictionary<string, Status>();

    [SerializeField]
    private List<string> loadingSpriteNameList = new List<string>();

    [SerializeField]
    private List<string> categoryKeywordList;
    [SerializeField]
    private List<string> nameKeywordList;
    [SerializeField]
    private List<string> statusKeywordList;

    public const string RESOURCES_PATH = "Resources";
    public const string SPRITE_PATH = "Sprite";

    const string EXCLUDE_KEYWORD = "@";

    public void LoadSprite()
    {
        countLoadSprite = 0;

        loadingSpriteNameList.Clear();

        KeywordSetting();

        string fullPath = Application.dataPath + "/" +
            RESOURCES_PATH + "/" +
            SPRITE_PATH + "/";

        // Sprite Directory 이하의 Directory 들을 가져옴
        string[] targetDirectoryWithPath = System.IO.Directory.GetDirectories(fullPath);

        for (int i = 0; i < targetDirectoryWithPath.Length; ++i)
        {
            // directory 들 이하의 png file 들을 가져옴
            string[] spriteNameWithPath = System.IO.Directory.GetFiles(targetDirectoryWithPath[i], "*.png");

            for (int j = 0; j < spriteNameWithPath.Length; ++j)
            {
                if (spriteNameWithPath[j] == EXCLUDE_KEYWORD)
                    continue;

                // 4 파트로 나뉜 이름
                string[] strType = { "", "", "", ""};
                string[] splitName = GetSplitName(spriteNameWithPath[j]);
                for(int k = 0; k < splitName.Length; ++k)
                {
                    strType[k] = splitName[k];
                }
                
                SpriteAttribute sa = new SpriteAttribute();

                // resource.load 를 위한 이름
                string resourceLoadName = GetLoadingName(spriteNameWithPath[j]);
                sa.sprite = Resources.Load<Sprite>(resourceLoadName);

                sa.frameCount = GetSpriteFrameCount(strType);
                sa.speed = GetSpriteSpeed(strType, sa.frameCount);
                sa.cycle = (1f / (float)spriteDefaultFramePerSec) / sa.speed * (float)(sa.frameCount - 1);

                CutSpriteAttribute(ref strType);
                
                //

                if (sa.sprite == null)
                {
                    CustomLog.CompleteLogWarning(
                        "Invalid Sprite: " + resourceLoadName,
                        PRINT_DEBUG);

                    continue;
                }

                if(strCategoryDic.ContainsKey(strType[0]) == false)
                {
                    CustomLog.CompleteLog(
                        "Invalid Category: " + strType[0],
                        PRINT_DEBUG);

                    continue;
                }
                
                if(strNameDic.ContainsKey(strType[1]) == false)
                {
                    CustomLog.CompleteLog(
                        "Invalid Name: " + strType[1],
                        PRINT_DEBUG);

                    continue;
                }
                
                if(strStatusDic.ContainsKey(strType[2]) == false)
                {
                    CustomLog.CompleteLog(
                        "Invalid Status: " + strType[2],
                        PRINT_DEBUG);

                    continue;
                }

                loadingSpriteNameList.Add(strType[0] + " " + strType[1] + " " + strType[2]);
                
                Category category = strCategoryDic[strType[0]];
                Name name = strNameDic[strType[1]];
                Status status = strStatusDic[strType[2]];

                //// TODO 이부분의 코드 정리
                //Dictionary<Status, SpriteAttribute> d = new Dictionary<Status, SpriteAttribute>();
                //Dictionary<Name, Dictionary<Status, SpriteAttribute>> dd = new Dictionary<Name, Dictionary<Status, SpriteAttribute>>();
                //d[status] = sa;
                //dd[name] = d;
                if (typeSpriteDic.ContainsKey(category) == false)
                    typeSpriteDic.Add(category, new Dictionary<Name, Dictionary<Status, SpriteAttribute>>());
                if (typeSpriteDic[category].ContainsKey(name) == false)
                    typeSpriteDic[category].Add(name, new Dictionary<Status, SpriteAttribute>());
                if (typeSpriteDic[category][name].ContainsKey(status) == false)
                    typeSpriteDic[category][name].Add(status, sa);
                ++countLoadSprite;
            }
        }

        CustomLog.CompleteLog("Load Sprite Count: " + countLoadSprite);
    }

    public static void SetSpriteCycleTime(ref SpriteAttribute sa, float time)
    {
        sa.cycle = time;
        sa.speed = (1f / (float)spriteDefaultFramePerSec) / time * (float)(sa.frameCount - 1);
    }

    // [speed]_strip[frame]
    int GetSpriteFrameCount(string[] name)
    {
        string attribute = GetSpriteAttribute(name);
        if (attribute == "")
            return 1;

        int frameCountStartIndex = attribute.LastIndexOf("_strip") + "_strip".Length;

        attribute = attribute.Substring(frameCountStartIndex);

        return System.Convert.ToInt32(attribute);
    }

    float GetSpriteSpeed(string[] name, int frameCount)
    {
        string attribute = GetSpriteAttribute(name);
        if (attribute == "")
            return 1f;

        int speedEndIndex = attribute.LastIndexOf("_strip");

        attribute = attribute.Substring(0, speedEndIndex);

        int framePerSec = System.Convert.ToInt32(attribute);

        float deltaTime = 1f / (float)framePerSec;

        float speed = (1f / (float)spriteDefaultFramePerSec) / deltaTime;

        return speed;
    }

    void CutSpriteAttribute(ref string[] name)
    {
        for (int i = name.Length - 1; i >= 0; --i)
        {
            if (name[i].Contains("_strip") == true)
            {
                name[i] = "";
            }
        }
    }

    string GetSpriteAttribute(string[] name)
    {
        for (int i = name.Length - 1; i >= 0; --i)
        {
            if (name[i].Contains("_strip") == true)
            {
                return name[i];
            }
        }

        return "";
    }


    string[] GetSplitName(string name)
    {
        string[] splitName = name.Split('/', '\\');

        string nameWithExtension = splitName[splitName.Length - 1];

        string originName = nameWithExtension.Split('.')[0];

        return originName.Split(' ');
    }

    string GetLoadingName(string name)
    {
        // / or \ 로 split
        string[] splitName = name.Split('/', '\\');

        string loadingName = "";

        // Resouces 하위의 상대경로 + 파일이름( 확장자명 제외 )

        bool b = false;
        for (int i = 0; i < splitName.Length; ++i)
        {
            if (splitName[i] == RESOURCES_PATH)
            {
                b = true;
                continue;
            }
            if (b)
            {
                loadingName += splitName[i] + "/";
            }
        }
        return loadingName.Split('.')[0];
    }

    //

    public enum Category
    {
        NONE = 0,
        
        WEAPON,
        BODY,
        PLAYER,
        PARTICLE,
        ERROR,
        VIRUS,
    }

    public enum Name
    {
        NONE = 0,

        // player
        SHIELD,

        // particle
        LASER,
        RUPTURE,
        SCRATCH,
        PLAYER_BULLET,
        SPARK_1,
        SPARK_2,
        AIM,
        HIT,

        // body
        CIRCLE,
        SQUARE,
        BELL,
        GLIDER,

        // weapon
        BAZOOKA,
        BOLT,
//        LASER,
        LAUNCHER,
        SPEAR,

        // virus
        BOOTING_TEMPUS,
        MICRO_BEETLE,
        BIT_SLICER,
        CYBER_DISK,
        DIGITAL_TERROR,
        TRANSISTOR_BURST,

        // error
        DIGITAL_TIMER,
        ANALOG_TIMER,
        A,
        WING,
        TURRET,

    }

    public enum Status
    {
        NONE = 0,
        
        DIE,
        CHANGE,
        SECOND_FORM,
        BORN,

        PART_1,
        PART_2,
        PART_3,
        PART_4,
        PART_5,
        PART_6,
        PART_7,
        PART_8,

        // laser
        ROOT,
        COLUMN,
        WARN,
        
        INIT,
        EXPLOSION,
    }

    void KeywordSetting()
    {
        strCategoryDic.Clear();

        strCategoryDic[""] = Category.NONE;

        strCategoryDic["error"] = Category.ERROR;
        strCategoryDic["virus"] = Category.VIRUS;
        strCategoryDic["particle"] = Category.PARTICLE;
        strCategoryDic["player"] = Category.PLAYER;
        strCategoryDic["body"] = Category.BODY;
        strCategoryDic["weapon"] = Category.WEAPON;

        categoryKeywordList = new List<string>(strCategoryDic.Keys);

        strNameDic.Clear();

        strNameDic[""] = Name.NONE;

        strNameDic["shield"] = Name.SHIELD;

        strNameDic["transistor_burst"] = Name.TRANSISTOR_BURST;
        strNameDic["digital_terror"] = Name.DIGITAL_TERROR;
        strNameDic["cyber_disk"] = Name.CYBER_DISK;
        strNameDic["micro_beetle"] = Name.MICRO_BEETLE;
        strNameDic["booting_tempus"] = Name.BOOTING_TEMPUS;
        strNameDic["bit_slicer"] = Name.BIT_SLICER;

        strNameDic["bell"] = Name.BELL;
        strNameDic["circle"] = Name.CIRCLE;
        strNameDic["square"] = Name.SQUARE;
        strNameDic["glider"] = Name.GLIDER;

        strNameDic["bazooka"] = Name.BAZOOKA;
        strNameDic["launcher"] = Name.LAUNCHER;
        strNameDic["laser"] = Name.LASER;
        strNameDic["bolt"] = Name.BOLT;
        strNameDic["spear"] = Name.SPEAR;

        strNameDic["A"] = Name.A;
        strNameDic["digital_timer"] = Name.DIGITAL_TIMER;
        strNameDic["analog_timer"] = Name.ANALOG_TIMER;
        strNameDic["turret"] = Name.TURRET;
        strNameDic["wing"] = Name.WING;

        strNameDic["player_bullet"] = Name.PLAYER_BULLET;
        strNameDic["spark1"] = Name.SPARK_1;
        strNameDic["spark2"] = Name.SPARK_2;
        strNameDic["aim"] = Name.AIM;
        strNameDic["bazooka"] = Name.BAZOOKA;
        strNameDic["hit"] = Name.HIT;

        nameKeywordList = new List<string>(strNameDic.Keys);

        strStatusDic.Clear();

        strStatusDic[""] = Status.NONE;

        strStatusDic["die"] = Status.DIE;
        strStatusDic["born"] = Status.BORN;
        strStatusDic["change"] = Status.CHANGE;
        strStatusDic["secondform"] = Status.SECOND_FORM;
        strStatusDic["explosion"] = Status.EXPLOSION;
        strStatusDic["init"] = Status.INIT;
        strStatusDic["root"] = Status.ROOT;
        strStatusDic["column"] = Status.COLUMN;
        strStatusDic["warn"] = Status.WARN;
        strStatusDic["part1"] = Status.PART_1;
        strStatusDic["part2"] = Status.PART_2;
        strStatusDic["part3"] = Status.PART_3;
        strStatusDic["part4"] = Status.PART_4;
        strStatusDic["part5"] = Status.PART_5;
        strStatusDic["part6"] = Status.PART_6;
        strStatusDic["part7"] = Status.PART_7;
        strStatusDic["part8"] = Status.PART_8;

        statusKeywordList = new List<string>(strStatusDic.Keys);
    }
}
