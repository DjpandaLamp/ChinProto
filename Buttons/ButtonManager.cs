using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    public string gameStartScreen;
    public string mainMenuScreen;
    private LevelDefine characteristics;
    private CanvasGroup canvas;
    private PauseShade PauseShade;
    public float localTrans;



    private void Start()
    {
        if (gameObject.name == "PauseCanvas")
        {
            canvas = this.GetComponent<CanvasGroup>();
            
            PauseShade = GameObject.Find("PauseShade").GetComponent<PauseShade>();
            canvas.alpha = 0;
        }
        characteristics = GameObject.Find("LevelDefine").GetComponent<LevelDefine>();

    }

    private void FixedUpdate()
    {
        if (gameObject.name == "PauseCanvas")
        {
            if (PauseShade.opacity > 0.2f)
            {
                localTrans = PauseShade.opacity * 2.5f;
            }
            else
            {
                localTrans = 0;
            }
            canvas.alpha = Mathf.SmoothStep(canvas.alpha, localTrans, 0.125f);
        }
    }

    public void NewGame()
    {
        SceneManager.LoadSceneAsync(gameStartScreen);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResumeGame()
    {
        characteristics.gamePaused = false;
    }
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(mainMenuScreen);
    }

}
