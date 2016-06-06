using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(LayerSetting)), CanEditMultipleObjects]
public class LayerSettingEditor : Editor
{
    bool layerListFold = true;

    SerializedProperty layerMaskProp;

    void OnEnable()
    {
        layerMaskProp = serializedObject.FindProperty("layerMask");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        LayerManager lm = GameObject.FindObjectOfType<LayerManager>();
        if (lm != null)
        {
            LayerManager.mManager = lm;
            lm.InitLayer();
        }

        ShowList(layerMaskProp);

        // target 은 값은 가져오는 용도로만 사용 가능.
//        LayerSetting ls = target as LayerSetting;

        serializedObject.ApplyModifiedProperties();

        //Rect r = GUILayoutUtility.GetRect(0f, 16f);
        //bool showNext = EditorGUI.PropertyField(r, layerProp, true);
        //bool hasName = layerProp.NextVisible(showNext);

        //        if (layerListFold = EditorGUILayout.Foldout(layerListFold, "Layers"))

    }

    void ShowList(SerializedProperty prop)
    {
        EditorGUILayout.Space();

        if(layerListFold = EditorGUILayout.Foldout(layerListFold,new GUIContent("layers")))
        {
            foreach (var name in LayerManager.layerNameNumber.Keys)
            {
                SerializedProperty elementProperty = prop.GetArrayElementAtIndex(LayerManager.layerNameNumber[name]);
                EditorGUILayout.PropertyField(elementProperty, new GUIContent(name));
            }
        }
        
    }

}
