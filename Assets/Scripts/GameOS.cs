using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

   
    public void RestartButton()
    {
        SceneManager.LoadScene("MoonScene");
    }
    public void ExitButtton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
