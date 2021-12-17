using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Chicken : MonoBehaviour
{

    // ENCAPSULATION

    private bool isHungry = true;
    public bool IsHungry
    {
        get { return isHungry; }
    }
    
    [SerializeField] private Nest nest;
    [SerializeField] private EasterCream easterCreamPrefab;
    [SerializeField] private HardBoiled hardBoildedPrefab;
    [SerializeField] private ChickToBe chickToBePrefab;
    [SerializeField] private float eggOffset;
    [SerializeField] private TMP_Text foodCounts;

    private int maxFoodValue;
    private int cholocateCount;
    private int pepperCount;
    private int seedCount;
    private int totalFood;
    private bool eggLaid;
   
    private NavMeshAgent agentChicken;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        agentChicken = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        cholocateCount = 0;
        pepperCount = 0;
        seedCount = 0;
        totalFood = 0;
        maxFoodValue = Random.Range(8, 12);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))    //left click
        {
            // ABSTRACTION

            MoveToClickedSpot();
        }

        if (nest.Occupied && !eggLaid)
        {
            isHungry = false;
            InitiateLaying();
        }

        if (agentChicken.velocity.magnitude < 0.15f)
        {
            // chicken is not walking - disable animation
            animator.SetBool("Walking", false);
        }
        else
        {
            // chicken is walking - enable animation
            animator.SetBool("Walking", true);
        }

    }

    private void MoveToClickedSpot()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50))
        {
            agentChicken.destination = hit.point;
        }

    }

    public void Eat(Food food, int foodValue)
    {

        Rigidbody foodRB = food.GetComponent<Rigidbody>();
        foodRB.isKinematic = true;

        // local UI or SFX - "gulp"

        // Add food value to total, and to food type subtotal

        string foodType = food.GetType().ToString();

        switch (foodType)
        {
            case ("Chocolate"):
                {
                    cholocateCount += foodValue;
                    break;
                }

            case ("Peppers"):
                {
                    pepperCount += foodValue;
                    break;
                }
            case ("Seeds"):
                {
                    seedCount += foodValue;
                    break;
                }

        }

        totalFood += foodValue;

        // Update food count UI
        UpdateFoodCountUI();
        
        if (totalFood >= maxFoodValue)
        {
            //set the chicken to not hungry
            isHungry = false;

        }

        StartCoroutine(EatFood(food));
        
    }

    private void UpdateFoodCountUI()
    {
        foodCounts.text = ($"Chocolate: {cholocateCount}  Peppers: {pepperCount}  Seeds: {seedCount}");
            
    }

    private void InitiateLaying()
    {
        eggLaid = true;

        // Change to laying animation
        animator.SetBool("Nesting", true);

        if (cholocateCount > seedCount && cholocateCount > pepperCount)
        {
            StartCoroutine(LayEgg(easterCreamPrefab));
        }
        else if(seedCount > cholocateCount && seedCount > pepperCount)
        {
            StartCoroutine(LayEgg(chickToBePrefab));
        }
        else
        {
            StartCoroutine(LayEgg(hardBoildedPrefab));
        }

    }

    private IEnumerator LayEgg(Egg eggPrefab)
    {
        yield return new WaitForSeconds(2);
        Instantiate(eggPrefab,transform.position + new Vector3(eggOffset,0f,0f), transform.rotation);
        animator.SetBool("Nesting", false);
    }

    private IEnumerator EatFood(Food food)
    {
        // Play eating animation
        animator.SetBool("Eating", true);

        // Wait while eating
        yield return new WaitForSeconds(1.5f);

        //Get rid of food and stop animation
        
        Destroy(food.gameObject);
        animator.SetBool("Eating", false);
    }
}
