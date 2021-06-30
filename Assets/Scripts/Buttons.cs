using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Buttons : MonoBehaviour
{
    [SerializeField] Canvas pauseCanvas; 
    [SerializeField] Canvas feedbackCanvas; 
    [SerializeField] AudioMixer mixer;
    [SerializeField] Canvas settingsCanvas;
    bool paused = false;
    List<GameObject> soundObjects = new List<GameObject>();
    Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
        settingsCanvas = GameObject.Find("Settings Canvas").GetComponent<Canvas>();
        pauseCanvas = GameObject.Find("Pause").gameObject.GetComponent<Canvas>();
        feedbackCanvas = GameObject.Find("FeedbackCanvas").gameObject.GetComponent<Canvas>();
        currentScene = SceneManager.GetActiveScene();
        var sources = FindObjectsOfType<AudioSource>();
        foreach(AudioSource source in sources)
        {
            //source.outputAudioMixerGroup = mixer;
        }


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
    public void reloadCurrentScene()
    {
        StartCoroutine(menu());
        //StartCoroutine(reloadScene());
    }

    public void exitGame()
    {
        Application.Quit();
    }
  
    public void hideButton()
    {
        gameObject.SetActive(false);
    }
    public void adjustSound(float sliderValue)
    {
        mixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
    }
    public void openSettings()
    {
        settingsCanvas.enabled = true;
    }
    public void closeSettings()
    {
        settingsCanvas.enabled = false;
    }
    IEnumerator loadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    IEnumerator reloadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentScene.name);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    IEnumerator menu()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
