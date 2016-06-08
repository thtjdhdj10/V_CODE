using UnityEngine;
using System.Collections.Generic;

using System.Runtime.Serialization;

public class LayerSetting : MonoBehaviour {

    public bool[] layerMask = new bool[LayerManager.MAX_LAYER_COUNT];

    void Start()
    {
        AddLayerMatchingComponent();
    }

    public void AddLayerMatchingComponent()
    {
        Component[] components = GetComponents<Component>();

        for(int i = 0 ; i < components.Length;++i)
        {
            var type = components[i].GetType();
            if(LayerManager.componentLayerNameDic.ContainsKey(type) == false)
                continue;

            string layerName = LayerManager.componentLayerNameDic[type];
            int layerNumber = LayerManager.layerNameNumberDic[layerName];

            layerMask[layerNumber] = true;
        }
    }
}
