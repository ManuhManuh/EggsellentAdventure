using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{

    private Rigidbody eggRB;
    private void Start()
    {
        eggRB = GetComponent<Rigidbody>();
        StartCoroutine(CommenceToHatch());
    }

    private IEnumerator CommenceToHatch()
    {
        yield return new WaitForSeconds(4);
        Hatch();
    }
    public virtual void Hatch()
    {
        // stop the egg from reacting to physics (hatching will be animated)
        eggRB.isKinematic = true;

        // Other Hatch behaviour will exist in overridden methods

        // Tell Game manager that egg has hatched
        GameManager.Instance.CheckForWin(this);
    }
}
