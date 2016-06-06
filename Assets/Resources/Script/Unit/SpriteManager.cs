using UnityEngine;
using System.Collections.Generic;

using  =
System.Collections.Generic.Dictionary<SpriteManager.SpriteType,
System.Collections.Generic.Dictionary<SpriteManager.SpriteType,
System.Collections.Generic.Dictionary<SpriteManager.SpriteType, >>>;

public class SpriteManager : MonoBehaviour {

    public enum Type
    {

    }

    public Dictionary<string, SpriteType> nameTypeDic = new Dictionary<string, SpriteType>();

    MultiTypeDic  = new MultiTypeDic();

    public enum SpriteType
    {
        NONE = 0,
        ENEMY,
        PLAYER,
        PARTICLE,
        WEAPON,
    }

	void Start () {
        d[(int)SpriteType.ENEMY][(int)SpriteType.ENEMY][(int)SpriteType.ENEMY] = "s";
	}


}
