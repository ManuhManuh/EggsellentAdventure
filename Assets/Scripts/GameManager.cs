using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    
    [SerializeField] private List<Egg> eggs = new List<Egg>();
    [SerializeField] private TMP_Text winCheckText;
    [SerializeField] private GameObject restart;

    private Egg winningEgg;
    private string laidEggType;
    private string winningEggType;
    private string laidEggPhrase;
    private string winningEggPhrase;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        // Generate the winning egg type

        int index = Random.Range(0, eggs.Count);
        winningEgg = eggs[index];
        //Debug.Log($"The winning egg type is {winningEgg.GetType()}");
    }

    public void CheckForWin(Egg laidEgg)
    {
        laidEggType = laidEgg.GetType().ToString();

        switch (laidEggType)
        {
            case "EasterCream":
                {
                    laidEggPhrase = "an Easter Cream Egg";
                    break;
                }
            case "HardBoiled":
                {
                    laidEggPhrase = "a Hard Boiled Egg";
                    break;
                }
            case "ChickToBe":
                {
                    laidEggPhrase = " a Proper Chicken Egg";
                    break;
                }

        }

        //Debug.Log($"Laid egg phrase is: {laidEggPhrase}");

        if (IsWinningType(laidEgg))
        {
            winCheckText.text = $"Congratulations!!  You laid {laidEggPhrase}!!!";
        }
        else
        {
            winCheckText.text = $"Such shame!!  You laid {laidEggPhrase} when you should have laid {winningEggPhrase}!!";
        }

        // Display restart button
        restart.SetActive(true);
     
    }

    public bool IsWinningType(Egg laidEgg)
    {
        winningEggType = winningEgg.GetType().ToString();
        switch (winningEggType)
        {
            case "EasterCream":
                {
                    winningEggPhrase = "an Easter Cream Egg";
                    break;
                }
            case "HardBoiled":
                {
                    winningEggPhrase = "a Hard Boiled Egg";
                    break;
                }
            case "ChickToBe":
                {
                    winningEggPhrase = " a Proper Chicken Egg";
                    break;
                }
        }
        //Debug.Log($"Winning egg phrase is: {winningEggPhrase}");

        return laidEggType == winningEggType;
    }

}
