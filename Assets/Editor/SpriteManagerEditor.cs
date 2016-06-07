using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SpriteManager)), CanEditMultipleObjects]
public class SpriteManagerEditor : Editor {
    


    void OnEnable()
    {
        SpriteManager sm = target as SpriteManager;

        sm.LoadSprite();
    }

    public override void OnInspectorGUI()
    {

    }
}
