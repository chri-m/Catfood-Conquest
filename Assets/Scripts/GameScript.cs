using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public float distance;
    public GameObject cat;

    public GameObject pauseScreen;
    public Animator pauseanimation;
    public bool isPaused;

    public GameObject poisonedScreen;
    public Text milkCounttextwithin;

    public int coinCount;
    public int internalCoinCount;
    public Text coinCounttext;

    public int catfoodCount;
    public Text catfoodCounttext;

   public GameObject Milkdisplay;
    public int milkCount;
    public Text milkCounttext;

    public float score;
    public float scoreIncrement;
 
    public Text scoretext;

    public float highScore;

    private float revived;

    public int posison;
    public Text poisonText;


    public InventoryScript playerInventory;

    public SpawnScript spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("SpawnLogic").GetComponent<SpawnScript>();

        LoadCat();
        internalCoinCount = 0;
        coinCounttext.text = internalCoinCount.ToString();
        posison = 2;
        pauseScreen.SetActive(false);
        poisonedScreen.SetActive(false);
        isPaused = false;
        hideMilk();
        scoreIncrement = 10;
        revived = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused == false)
        {
            if (spawner != null)
            {
                float actualScoreIncrement = scoreIncrement * (spawner.moveSpeed/2);

                // Increase score based on the current score increment
                score += actualScoreIncrement * Time.deltaTime;
            }
            
            scoretext.text = ((int)score).ToString();
        }    
    }

    public void moveRight()
    {
        if (isPaused == false)
        {
            if (posison != 3)
            {
                Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f);
                cat.transform.Translate(direction * distance);
                posison += 1;
            }
        }
    }
    public void moveLeft()
    {
        if (isPaused == false)
        {
            if (posison != 1)
            {
                Vector3 direction = new Vector3(-1.0f, 0.0f, 0.0f);
                cat.transform.Translate(direction * distance);
                posison -= 1;
            }
        }
    }
    public void getCoin()
    {
        internalCoinCount += 1;
        coinCounttext.text = internalCoinCount.ToString();
    }
    public void getCatfood()
    {
        catfoodCount += 1;
        catfoodCounttext.text = catfoodCount.ToString();
    }

    public void getMilk()
    {
        milkCount += 1;
        milkCounttext.text = milkCount.ToString();
        milkCounttext.enabled = true;
        Milkdisplay.SetActive(true);
        Invoke("hideMilk", 1);
    }

    public void hideMilk()
    {
        milkCounttext.enabled = false;
        Milkdisplay.SetActive(false);
    }

    public void EndGame()
    {
        coinCount += internalCoinCount;
        if (score > highScore)
        {
            highScore = score;
        }
        SaveCat();
    }


    [ContextMenu("Infinite Money Glitch")]
    public void AddMoney()
    {
        coinCount = 99999;
        playerInventory.SaveCat();
    }

    [ContextMenu("No Money Glitch")]
    public void LoseMoney()
    {
        coinCount = 0;
        playerInventory.SaveCat();
    }

    [ContextMenu("Save")]
    public void SaveCat()
    {
        playerInventory.milkCount = milkCount;
        playerInventory.catfoodCount = catfoodCount;
        playerInventory.coinCount = coinCount;
        if (SceneManager.GetActiveScene().name == "MoonScene")
        {
            playerInventory.MoonhighScore = highScore;  // Moon highscore
        }
        else if (SceneManager.GetActiveScene().name == "MarsScene")
        {
            playerInventory.MarshighScore = highScore;   // Mars highscore
        }
        else
        {
            Debug.Log("No scene found");
        }
        playerInventory.SaveCat();
    }
    [ContextMenu("Load")]
    public void LoadCat()
    {
        CatDataScript data = SaveScript.LoadCat();

        playerInventory.LoadCat();
        milkCount = playerInventory.milkCount;
        catfoodCount = playerInventory.catfoodCount;
        coinCount = playerInventory.coinCount;
        if (SceneManager.GetActiveScene().name == "MoonScene")
        {
            highScore = playerInventory.MoonhighScore;  // Moon highscore load
        }
        else if (SceneManager.GetActiveScene().name == "MarsScene")
        {
            highScore = playerInventory.MarshighScore;   // Mars highscore load
        }
        else
        {
            Debug.Log("No scene found");
        }
        
        coinCounttext.text = coinCount.ToString();
        catfoodCounttext.text = catfoodCount.ToString();
        milkCounttext.text = milkCount.ToString();
    }


    [ContextMenu("Reset")]
    public void ResetCat()
    {
        coinCount = 0;
        coinCounttext.text = coinCount.ToString();
        SaveCat();
    }

    public void Poisoned()
    {
        poisonText.text = "Use " + (int)Mathf.Pow(2, revived) + "Milk to Continue?";
        poisonedScreen.SetActive(true);
        milkCounttextwithin.text = milkCount.ToString();
        isPaused = true;
    }
    public void Continue()
    {
        milkCount -= (int)Mathf.Pow(2, revived);
        revived += 1;
        poisonedScreen.SetActive(false);
        isPaused = false;
    }
    public void GIveup()
    {
        EndGame();
        SceneManager.LoadScene("GameOverScene");
    }



    [ContextMenu("Pause")]
    public void Pause()
    {
        if (poisonedScreen.activeSelf==false)
        {
            pauseScreen.SetActive(true);
            isPaused = true;
        }

    }

    [ContextMenu("Resume")]
    public void ResumeAnim()
    {
        pauseanimation.SetTrigger("pauseclose");
        Invoke("Resume", 0.4f);
    }
    public void Resume()
    {
        pauseScreen.SetActive(false);
        isPaused = false;
    }
    public void Quit()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
