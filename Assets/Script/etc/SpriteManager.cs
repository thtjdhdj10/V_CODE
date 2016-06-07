using UnityEngine;
using System.Collections.Generic;

using TypeTextureDictionary = 
System.Collections.Generic.Dictionary<SpriteManager.Category,
System.Collections.Generic.Dictionary<SpriteManager.Name,
System.Collections.Generic.Dictionary<SpriteManager.Status, UnityEngine.Texture>>>;

public class SpriteManager : MonoBehaviour {

    public const bool PRINT_DEBUG = true;

    public TypeTextureDictionary typeTextureDic = new TypeTextureDictionary();

    private Dictionary<string, Category> strCategoryDic = new Dictionary<string, Category>();
    private Dictionary<string, Name> strNameDic = new Dictionary<string, Name>();
    private Dictionary<string, Status> strStatuDic = new Dictionary<string, Status>();

    private List<string> categoryKeywordList;
    private List<string> nameKeywordList;
    private List<string> statusKeywordList;

    public const string RESOURCES_PATH = "Resources";
    public const string SPRITE_PATH = "Sprite";

    const string UNUSED_KEYWORD = "@";
    const string STRIP_KEYWORD = "strip";

    public int countLoadSprite;

    public void LoadSprite()
    {
        countLoadSprite = 0;

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
                // 3 파트로 나뉜 이름
                string[] strType = { "", "", ""};
                string[] splitName = GetSplitName(spriteNameWithPath[j]);
                for(int k = 0; k < splitName.Length; ++k)
                {
                    strType[k] = splitName[k];
                }

                GetCompleteName(ref strType);

                // resource.load 를 위한 이름
                string resourceLoadName = GetLoadingName(spriteNameWithPath[j]);

                Texture sprite = Resources.Load(resourceLoadName) as Texture;

                CustomLog.CompleteLogError(
                    "Invalid Sprite: " + resourceLoadName,
                    PRINT_DEBUG && !(sprite == null));

                CustomLog.CompleteLogError(
                    "Invalid Category: " + strType[0],
                    PRINT_DEBUG && !strCategoryDic.ContainsKey(strType[0]));

                CustomLog.CompleteLogError(
                    "Invalid Name: " + strType[1],
                    PRINT_DEBUG && !strNameDic.ContainsKey(strType[1]));

                CustomLog.CompleteLogError(
                    "Invalid Status: " + strType[2],
                    PRINT_DEBUG && !strStatuDic.ContainsKey(strType[2]));
                
                Category category = strCategoryDic[strType[0]];
                Name name = strNameDic[strType[1]];
                Status status = strStatuDic[strType[2]];

                Dictionary<Status, Texture> d = new Dictionary<Status, Texture>();
                Dictionary<Name, Dictionary<Status, Texture>> dd = new Dictionary<Name, Dictionary<Status, Texture>>();
                d[status] = sprite;
                dd[name] = d;
                typeTextureDic[category] = dd;
                ++countLoadSprite;
            }
        }

        CustomLog.CompleteLog("Load Sprite Count: " + countLoadSprite);
    }
    
    void GetCompleteName(ref string[] name)
    {
        for (int i = 0; i < name.Length; ++i)
        {
            if (name[i] == "")
                continue;

            string prevName = name[i];

            // @, _strip 이하는 자름
            int finishIndex = name[i].LastIndexOf("_strip");
            finishIndex = Mathf.Max(finishIndex, name[i].LastIndexOf(UNUSED_KEYWORD));
            if (finishIndex == -1)
                continue;

            name[i] = name[i].Substring(0, finishIndex);
        }
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
                loadingName += splitName[i] + "\\";
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

        // particle
        LASER,
        RUPTURE,
        SCRATCH,

        // body
        CIRCLE,
        SQUARE,
        BELL,
        GLIDER,
        POWER_CIRCLE,
        POWER_SQUARE,
        POWER_BELL,
        POWER_GLIDER,

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
        DIGITAL_CLOCK,
        ANALOG_CLOCK,
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
        strNameDic["powerbell"] = Name.POWER_BELL;
        strNameDic["powercircle"] = Name.POWER_CIRCLE;
        strNameDic["powersquare"] = Name.POWER_SQUARE;

        strNameDic["bazooka"] = Name.BAZOOKA;
        strNameDic["launcher"] = Name.LAUNCHER;
        strNameDic["laser"] = Name.LASER;
        strNameDic["bolt"] = Name.BOLT;
        strNameDic["spear"] = Name.SPEAR;

        strNameDic["A"] = Name.A;
        strNameDic["digital_clock"] = Name.DIGITAL_CLOCK;
        strNameDic["analog_clock"] = Name.ANALOG_CLOCK;
        strNameDic["turret"] = Name.TURRET;
        strNameDic["wing"] = Name.WING;

        nameKeywordList = new List<string>(strNameDic.Keys);

        strStatuDic.Clear();

        strStatuDic[""] = Status.NONE;

        strStatuDic["die"] = Status.DIE;
        strStatuDic["born"] = Status.BORN;
        strStatuDic["change"] = Status.CHANGE;
        strStatuDic["secondform"] = Status.SECOND_FORM;
        strStatuDic["explosion"] = Status.EXPLOSION;
        strStatuDic["init"] = Status.INIT;
        strStatuDic["root"] = Status.ROOT;
        strStatuDic["column"] = Status.COLUMN;
        strStatuDic["warn"] = Status.WARN;
        strStatuDic["part1"] = Status.PART_1;
        strStatuDic["part2"] = Status.PART_2;
        strStatuDic["part3"] = Status.PART_3;
        strStatuDic["part4"] = Status.PART_4;
        strStatuDic["part5"] = Status.PART_5;
        strStatuDic["part6"] = Status.PART_6;
        strStatuDic["part7"] = Status.PART_7;
        strStatuDic["part8"] = Status.PART_8;

        statusKeywordList = new List<string>(strStatuDic.Keys);
    }
}
