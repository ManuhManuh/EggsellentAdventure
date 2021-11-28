using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class EasterCream : Egg
{
    public override void Hatch()
    {
        // POLYMORPHISM (OVERRIDE)

        base.Hatch();

        // cream filling pours out
    }
}
