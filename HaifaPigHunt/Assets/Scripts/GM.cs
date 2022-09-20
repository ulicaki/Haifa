using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GM : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Text BoarsLeftText;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject EndGamePanel;
    [SerializeField] GameObject WinLabel;
    [SerializeField] GameObject LoseLabel;
    [SerializeField] Light DirectionalLight;
    [SerializeField] Material DaySkybox;
    [SerializeField] Material NightSkybox;
    bool Night;
    bool Pause;
    int StartBoarCount;
    int BoarLeft;

    [Header("Spaw")]
    [SerializeField] Transform[] RandPos;
    [SerializeField] GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale =1;
        FirstSetUp();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        RandPosSpawn();
        NighRand();
    }

    void RandPosSpawn ()
    {
        int rando = Random.RandomRange(0, RandPos.Length);
        Player.transform.position = RandPos[rando].position;
    }

    void NighRand ()
    {
        int rand = Random.RandomRange(0, 2);
        if(rand == 0) // Night
        {
            DirectionalLight.intensity = 0.01f;
            GameObject[] lights = GameObject.FindGameObjectsWithTag("Lights");
            foreach (GameObject ligh in lights)
                ligh.SetActive(true);

            RenderSettings.skybox = NightSkybox;
        }
        else
                {
            DirectionalLight.intensity = 1;
            GameObject[] lights = GameObject.FindGameObjectsWithTag("Lights");
            foreach (GameObject ligh in lights)
                ligh.SetActive(false);

            RenderSettings.skybox = DaySkybox;
        }
        
    }

    void FirstSetUp()
    {
        GameObject[] Boarobjs = GameObject.FindGameObjectsWithTag("Enemy");
        StartBoarCount = Boarobjs.Length;
        BoarLeft = StartBoarCount;
        BoarsLeftText.text = StartBoarCount + "/"+ StartBoarCount;
    }

    public void MinusBoar ()
    {
        BoarLeft--;
        BoarsLeftText.text = BoarLeft + "/" + StartBoarCount;

        Sequence sec = DOTween.Sequence();
        sec.Append(BoarsLeftText.transform.DOScale(new Vector3(1.2f,1.2f,1.2f),0.2f));
        sec.Append(BoarsLeftText.transform.DOScale(new Vector3(1,1,1),0.2f));

        if (BoarLeft <= 0)
            WinLevel();
    }

    void TurnOnPause ()
    {
        Cursor.visible = true ;
        Cursor.lockState = CursorLockMode.None;
        Pause = true;
                PausePanel.SetActive(true);
        Time.timeScale = 0;

    }

    public void ExitPause ()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Pause = false;
        PausePanel.SetActive(false);
    }

   public void RestartGame ()
    {
        Application.LoadLevel(1);
    }

    public void ExitGame ()
    {
        Application.Quit();
    }

    public void WinLevel ()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        EndGamePanel.SetActive(true);
        WinLabel.SetActive(true);
        Time.timeScale =0;

    }

    public void LoseLevel()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        EndGamePanel.SetActive(true);
        LoseLabel.SetActive(true);
        Time.timeScale = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (!Pause)
                TurnOnPause();
            else
                ExitPause();
    }
}
