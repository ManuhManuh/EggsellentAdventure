using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class HardBoiled : Egg
{
    public override void Hatch()
    {
        // POLYMORPHISM (OVERRIDE)

        base.Hatch();

        // egg just falls over, showing hard boiled yolk
    }
}
