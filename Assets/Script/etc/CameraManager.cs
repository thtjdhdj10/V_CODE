﻿using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public static CameraManager manager;

    public float widthLogicalSize;
    public float heightLogicalSize;

    void Awake()
    {
        manager = this;

        SetLogicalSize();
    }

    void SetLogicalSize()
    {
        float widthRatio = (float)Screen.width / (float)Screen.height;

        heightLogicalSize = Camera.main.orthographicSize;
        widthLogicalSize = heightLogicalSize * widthRatio;
    }

    public Rect GetLogicalRect()
    {
        SetLogicalSize();

        Rect ret = new Rect();
        ret.xMin = -widthLogicalSize;
        ret.xMax = widthLogicalSize;
        ret.yMin = -heightLogicalSize;
        ret.yMax = heightLogicalSize;

        return ret;
    }
}
