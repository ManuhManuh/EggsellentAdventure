using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // ENCAPSULATION
    public int FoodValue
    {
        get { return foodValue; }
    }

    [SerializeField] protected int foodValue;

    private void Start()
    {
        foodValue = Random.Range(1, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Chicken chicken = collision.gameObject.GetComponent<Chicken>();
            // Eaten by the chicken that ran into it
            chicken.Eat(this, foodValue);
        }
    }
}

