using UnityEngine;
using System.Collections.Generic;

public class Player : Unit
{
    public void ShiftPosition(Vector2 delta)
    {
        Vector2 pos = transform.position + new Vector3(delta.x, delta.y, 0f);

        transform.position = pos;
    }


}
