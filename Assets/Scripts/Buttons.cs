using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] Canvas pauseCanvas; 
    [SerializeField] Canvas feedbackCanvas; 
    bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        
        pauseCanvas = GameObject.Find("Pause").gameObject.GetComponent<Canvas>();
        feedbackCanvas = GameObject.Find("FeedbackCanvas").gameObject.GetComponent<Canvas>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {               
                pauseCanvas.enabled = false;
                feedbackCanvas.enabled = false;
                Time.timeScale = 1;
                paused = false;
            }
            else
            {
                pauseCanvas.enabled = true;
                feedbackCanvas.enabled = true;
                Time.timeScale = 0;
                paused = true;
            }

            //StartCoroutine(loadScene());
        }
    }
    public void resume()
    {
        pauseCanvas.enabled = false;
        feedbackCanvas.enabled = false;
        Time.timeScale = 1;
        paused = false;
    }
    public void startGame()
    {
        
        Debug.Log("loading scene");
        StartCoroutine(loadScene());
    }

    public void exitGame()
    {
        Application.Quit();
    }
  
    public void hideButton()
    {
        gameObject.SetActive(false);
    }
    IEnumerator loadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
