using UnityEngine;
using System.Collections.Generic;

public class LayerManager : MonoBehaviour {

    public bool useUnityBasicLayer = false;

    enum CustomLayer
    {

        ERROR,
        BUG,
        VIRUS,
        CONTROLABLE,
        HITTABLE,
        COUNT,
    }

    //

    public static LayerManager mManager;

    public static int MAX_LAYER_COUNT = 100;

    private static int UNITY_MAX_LAYER_COUNT = 32;

    public static Dictionary<string, int> layerNameNumber = new Dictionary<string, int>();

    //

    public void InitLayer()
    {
        MAX_LAYER_COUNT = Mathf.Min(MAX_LAYER_COUNT, int.MaxValue);

        ClearLayer();
        if (useUnityBasicLayer == true)
            AddUnityLayer();
        AddCumtomLayer();
    }
    
    public void ClearLayer()
    {
        layerNameNumber.Clear();
    }

    public void AddUnityLayer()
    {
        for (int i = 0; i < UNITY_MAX_LAYER_COUNT; i++)
        {
            string name = LayerMask.LayerToName(i);

            AddLayer(name);
        }
    }

    public void AddCumtomLayer()
    {
        for (int i = 0; i < (int)CustomLayer.COUNT; ++i)
        {
            string name = ((CustomLayer)i).ToString();

            AddLayer(name);
        }
    }

    public static void AddLayer(string name)
    {
        if(name.Length == 0)
            return;

        if (layerNameNumber.ContainsKey(name) == true)
            return;

        int layerCount = layerNameNumber.Count;
        if (layerCount < MAX_LAYER_COUNT)
        {
            layerNameNumber[name] = layerCount;
        }
    }

    public static bool CheckLayer(bool[] layer, string name)
    {
        if (layerNameNumber.ContainsKey(name) == false)
            return false;

        int number = layerNameNumber[name];

        return layer[number];
    }

    public static bool CheckLayerOr(bool[] layer, params string[] names)
    {
        for (int i = 0; i < names.Length; ++i)
        {
            if (layerNameNumber.ContainsKey(names[i]) == false)
                continue;

            int number = layerNameNumber[names[i]];

            if (layer[number] == true)
                return true;
        }

        return false;
    }

    public static bool CheckLayerAnd(bool[] layer, params string[] names)
    {
        for (int i = 0; i < names.Length; ++i)
        {
            if (layerNameNumber.ContainsKey(names[i]) == false)
                continue;

            int number = layerNameNumber[names[i]];

            if (layer[number] == false)
                return false;
        }

        return true;
    }

    public static void AddLayer(ref bool[] layer, params string[] names)
    {
        for (int i = 0; i < names.Length; ++i)
        {
            if (layerNameNumber.ContainsKey(names[i]) == false)
                continue;

            int number = layerNameNumber[names[i]];

            layer[number] = true;
        }
    }

    public static void AddLayer(ref bool[] layer, params int[] number)
    {
        for (int i = 0; i < number.Length; ++i)
        {
            layer[number[i]] = true;
        }
    }

    public static void RemoveLayer(ref bool[] layer, params string[] names)
    {
        for (int i = 0; i < names.Length; ++i)
        {
            if (layerNameNumber.ContainsKey(names[i]) == false)
                continue;

            int number = layerNameNumber[names[i]];

            layer[number] = false;
        }
    }

    public static void RemoveLayer(ref bool[] layer, params int[] number)
    {
        for (int i = 0; i < number.Length; ++i)
        {
            layer[number[i]] = false;
        }
    }

    //

    public static bool[] StringToMask(string str)
    {
        bool[] ret = new bool[MAX_LAYER_COUNT];

        for (int i = 0; i < MAX_LAYER_COUNT; ++i)
        {
            ret[i] = false;
        }

        if (layerNameNumber.ContainsKey(str) == false)
            return ret;

        ret = GetMask(layerNameNumber[str]);

        return ret;
    }

    public static int StringToNumber(string str)
    {
        if (layerNameNumber.ContainsKey(str) == false)
            return -1;

        return layerNameNumber[str];
    }

    public static bool[] GetMask(int number)
    {
        bool[] ret = new bool[MAX_LAYER_COUNT];

        for (int i = 0; i < MAX_LAYER_COUNT; ++i)
        {
            ret[i] = false;
        }

        if (number < 0 || number >= MAX_LAYER_COUNT)
            return ret;

        ret[number] = true;

        return ret;
    }

    public static void LogicalRightShift(ref bool[] pattern, int shift)
    {
        for (int i = pattern.Length - 1; i >= 0; --i)
        {
            if (i > shift)
            {
                pattern[i] = pattern[i - shift];
            }
            else
            {
                pattern[i] = false;
            }
        }
    }

    public static void LogicalLeftShift(ref bool[] pattern, int shift)
    {
        for (int i = 0; i < pattern.Length; ++i)
        {
            if (i < shift)
            {
                pattern[i] = pattern[i + shift];
            }
            else
            {
                pattern[i] = false;
            }
        }
    }

}
