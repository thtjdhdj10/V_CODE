using UnityEngine;
using System.Collections;

public class SpecialError : Error {

    public enum Type
    {
        NONE,
        A,
        ANALOG_TIMER,
        DIGITAL_TIMER,
        TURRET,
        WING,
    }

}
