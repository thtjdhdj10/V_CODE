using UnityEngine;
using System.Collections.Generic;

using System.Runtime.Serialization;

public class LayerSetting : MonoBehaviour {

    public bool[] layerMask = new bool[LayerManager.MAX_LAYER_COUNT];
}
