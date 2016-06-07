using UnityEngine;
using System.Collections;

public class CustomLog{

    public enum WarningLevel
    {
        NONE,
        WARN,
        ERROR,
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void CompleteLog(string str)
    {
        CompleteLog(str, WarningLevel.NONE);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void CompleteLogWarning(string str)
    {
        CompleteLog(str, WarningLevel.WARN);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void CompleteLogError(string str)
    {
        CompleteLog(str, WarningLevel.ERROR);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void CompleteLog(string str, bool print)
    {
        if (print == false) return;

        CompleteLog(str, WarningLevel.NONE);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void CompleteLogWarning(string str, bool print)
    {
        if (print == false) return;

        CompleteLog(str, WarningLevel.WARN);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void CompleteLogError(string str, bool print)
    {
        if (print == false) return;

        CompleteLog(str, WarningLevel.ERROR);
    }

    [System.Diagnostics.Conditional("DEBUG")]
    public static void CompleteLog(string str, WarningLevel wl)
    {
        switch (wl)
        {
        case WarningLevel.NONE:
            {
                Debug.Log(str);
            }
            break;
        case WarningLevel.WARN:
            {
                Debug.LogWarning(str);
            }
            break;
        case WarningLevel.ERROR:
            {
                Debug.LogError(str);
            }
            break;
        }
    }
}
