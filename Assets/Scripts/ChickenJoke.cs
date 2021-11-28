using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenJoke : MonoBehaviour
{
    [SerializeField] List<string> jokes = new List<string>();

    public string SelectJoke()
    {
        int index = Random.Range(0, jokes.Count);
        return jokes[index];

    }
}
