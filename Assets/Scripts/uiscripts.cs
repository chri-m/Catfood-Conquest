using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uiscripts : MonoBehaviour
{
    [SerializeField] public GameObject soundPanel;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void back()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void soundPanelShow()
    {
        soundPanel.SetActive(true);
    }
    public void soundPanelOff()
    {
        soundPanel.SetActive(false);
    }
}
