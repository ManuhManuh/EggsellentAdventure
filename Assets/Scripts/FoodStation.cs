using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodStation : MonoBehaviour
{
    [SerializeField] private Food foodPrefab;
    [SerializeField] private ChickenJoke chickenJokePrefab;
    [SerializeField] private Chicken chicken;
    [SerializeField] private Vector3 launchDirection;
    [SerializeField] private TMP_Text chickenJokeText;
    [SerializeField] private float tossForce = 1f;
    [SerializeField] private float tossRadius;

    private bool chickenIsClose;
    private Food currentlyDispensedFood;
    private ChickenJoke currentlyDispensedChickenJoke;
    private bool needNewJoke;
    private Animator hingeAnimator;
    private Transform launchPosition;

    private void Start()
    {
        hingeAnimator = GetComponentInChildren<Animator>();
        foreach (Transform childTrans in gameObject.GetComponentsInChildren<Transform>())
        {
            if (childTrans.name == "LaunchPosition")
            {
                launchPosition = childTrans;
            }
        }
    }
    private void Update()
    {
        // make sure the chicken is within range and there isn't currently any uneaten food from this dispenser hanging about

        if (chickenIsClose)
        {
            // see if it needs to be fed or amused
            if(currentlyDispensedFood == null)
            {
                if (chicken.IsHungry)
                {
                    // Toss the food out so the chicken can eat it
                    Dispense(foodPrefab);

                }
                else
                {
                    if (needNewJoke)
                    {
                        // Display a random chicken joke in the UI
                        needNewJoke = false;
                        if (currentlyDispensedChickenJoke == null)
                        {
                            currentlyDispensedChickenJoke = Instantiate(chickenJokePrefab);
                        }
                        Dispense(currentlyDispensedChickenJoke);
                    }
                }
            }

        }
        else
        {
            // chicken moved away - reset the need for a new joke so it will get one if it comes back
            needNewJoke = true;
            
        }

        // TESTING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dispense(foodPrefab);
            //Debug.Log($"Is the chicken hungry?: {chicken.IsHungry}");
        }
    }

    // POLYMORPHISM (OVERLOAD)
    private void Dispense(Food foodPrefab)
    {

        // Note: when the chicken eats this food, currentlyDispensedFood should equate to null
        currentlyDispensedFood = Instantiate(foodPrefab, launchPosition.position, transform.rotation);

        // Run the launching of the food
        StartCoroutine("LaunchFood");

    }

    private void Dispense(ChickenJoke chickenJokePrefab)
    {

        chickenJokeText.text = chickenJokePrefab.SelectJoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chickenIsClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            chickenIsClose = false;
            chickenJokeText.text = " ";
        }
    }

    private IEnumerator LaunchFood()
    {
        // Open the lid (wait until it is open)
        hingeAnimator.SetTrigger("OpenLid");
        yield return new WaitForSeconds(1);

        // Launch the food (and give it time to clear the lid)
        Rigidbody foodRB = currentlyDispensedFood.GetComponent<Rigidbody>();
        foodRB.AddForce(launchDirection * tossForce, ForceMode.Impulse);
        yield return new WaitForSeconds(3);

        // Close the lid
        hingeAnimator.SetTrigger("CloseLid");
    }
}
