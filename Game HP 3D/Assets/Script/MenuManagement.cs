using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagement : MonoBehaviour
{
    [SerializeField] private GameObject MenuUI;
    [SerializeField] private GameObject MateriUI;
    [SerializeField] private GameObject CustomizeUI;
    [SerializeField] private GameObject PickMapUI;
    [SerializeField] private GameObject ProfilUI;

    private bool UpScorePrasyarat;

    public Text scorePrasyaratTextMenu;
    public Text scorePrasyaratTextMateri;

    public int ScorePrasyarat;
    public int LevelGet;
    public int MinimumScorePrasyarat;

    public Button[] BtnLevelPickUI;

    public GameObject SKKIKDUI;
    public GameObject MainMenuUI;
    public GameObject PrasyaratUI;

    public GameObject HintUIPrefabs;

    private LoadLevel animLoadLevel;
    private ScoreController scoreController;

    public Button[] BtnChangeColor;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("SoundPembukaan");
        MenuUI.SetActive(true);
        MateriUI.SetActive(false);
        CustomizeUI.SetActive(false);
        PickMapUI.SetActive(false);
        MainMenuUI.SetActive(true);
        SKKIKDUI.SetActive(false);
        PrasyaratUI.SetActive(false);
        ProfilUI.SetActive(false);

        animLoadLevel = FindObjectOfType<LoadLevel>();
        scoreController = GetComponent<ScoreController>();

        scoreController.Score = 0;
    }

    private void Update()
    {

        scorePrasyaratTextMenu.text = "Skor Prasyarat = " + scoreController.ScorePrasyarat;
        scorePrasyaratTextMateri.text = "Skor Prasyarat = " + scoreController.ScorePrasyarat;

        if (ScorePrasyarat >= MinimumScorePrasyarat)
        {
            UpScorePrasyarat = true;
        }
        else
        {
            UpScorePrasyarat = false;
        }

        if (scoreController.LevelGame == 0)
        {
            BtnLevelPickUI[0].interactable = true;
            BtnLevelPickUI[1].interactable = false;
            BtnLevelPickUI[2].interactable = false;
            BtnChangeColor[0].interactable = true;
            BtnChangeColor[1].interactable = false;
            BtnChangeColor[2].interactable = false;

        }
        else if (scoreController.LevelGame == 1)
        {
            BtnLevelPickUI[0].interactable = true;
            BtnLevelPickUI[1].interactable = true;
            BtnLevelPickUI[2].interactable = false;

            BtnChangeColor[0].interactable = true;
            BtnChangeColor[1].interactable = false;
            BtnChangeColor[2].interactable = false;
        }
        else if (scoreController.LevelGame == 2)
        {
            BtnLevelPickUI[0].interactable = true;
            BtnLevelPickUI[1].interactable = true;
            BtnLevelPickUI[2].interactable = true;

            BtnChangeColor[0].interactable = true;
            BtnChangeColor[1].interactable = false;
            BtnChangeColor[2].interactable = false;
        }
        else if (scoreController.LevelGame > 2)
        {
            BtnLevelPickUI[0].interactable = true;
            BtnLevelPickUI[1].interactable = true;
            BtnLevelPickUI[2].interactable = true;

            BtnChangeColor[0].interactable = true;
            BtnChangeColor[1].interactable = true;
            BtnChangeColor[2].interactable = true;
        }

        ScorePrasyarat = scoreController.ScorePrasyarat;
    }

    public void NewGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        if (UpScorePrasyarat == true)
        {
            MaterialController materialController = FindObjectOfType<MaterialController>();
            materialController.PickColour();
            StartCoroutine(NewGameAnim());
        }
        else
        {
            GameObject objUI = Instantiate(HintUIPrefabs, new Vector3(0, 0, 0), Quaternion.identity);
            objUI.transform.SetParent(MenuUI.transform, false);
            Destroy(objUI, 1.5f);
        }
    }

    IEnumerator NewGameAnim()
    {
        animLoadLevel.animLevel.SetTrigger("NextUI");
        yield return new WaitForSeconds(1f);
        MenuUI.SetActive(false);
        MateriUI.SetActive(false);
        CustomizeUI.SetActive(false);
        PickMapUI.SetActive(true);
        ProfilUI.SetActive(false);
    }

    public void gotoLevel(string LevelName)
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
        scoreController.SaveGame();
        StartCoroutine(gotoLevelAnim(LevelName));
    }

    IEnumerator gotoLevelAnim(string LevelName)
    {
        animLoadLevel.animLevel.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(LevelName);
    }

    public void Materi()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        StartCoroutine(MateriAnim());
    }

    IEnumerator MateriAnim()
    {
        animLoadLevel.animLevel.SetTrigger("NextUI");
        yield return new WaitForSeconds(1f);
        MenuUI.SetActive(false);
        MateriUI.SetActive(true);
        CustomizeUI.SetActive(false);
        PickMapUI.SetActive(false);
        ProfilUI.SetActive(false);
    }

    public void SKKIKD()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        StartCoroutine(SKKIKDUIAnim());
    }

    IEnumerator SKKIKDUIAnim()
    {
        animLoadLevel.animLevel.SetTrigger("NextUI");
        yield return new WaitForSeconds(1f);
        MainMenuUI.SetActive(false);
        SKKIKDUI.SetActive(true);
    }

    public void BackfromSKKIKD()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        StartCoroutine(BackfromSKKIKDAnim());
    }

    IEnumerator BackfromSKKIKDAnim()
    {
        animLoadLevel.animLevel.SetTrigger("NextUI");
        yield return new WaitForSeconds(1f);
        MainMenuUI.SetActive(true);
        SKKIKDUI.SetActive(false);
    }

    public void Prasyarat()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        StartCoroutine(PrasyaratAnim());
    }

    IEnumerator PrasyaratAnim()
    {
        animLoadLevel.animLevel.SetTrigger("NextUI");
        yield return new WaitForSeconds(1f);
        MainMenuUI.SetActive(false);
        PrasyaratUI.SetActive(true);
    }

    public void BackfromPrasyrat()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        StartCoroutine(BackfromPrasyaratAnim());
    }

    IEnumerator BackfromPrasyaratAnim()
    {
        animLoadLevel.animLevel.SetTrigger("NextUI");
        yield return new WaitForSeconds(1f);
        MainMenuUI.SetActive(true);
        PrasyaratUI.SetActive(false);
        scorePrasyaratTextMateri.text = "Skor Prasyarat = " + ScorePrasyarat;
    }

    public void PhytagorasMateri()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        if (UpScorePrasyarat == true)
        {
            FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
            MaterialController materialController = FindObjectOfType<MaterialController>();
            materialController.PickColour();
            StartCoroutine(PhytagorasMateriAnim());
        }
        else
        {
            GameObject objUI = Instantiate(HintUIPrefabs, new Vector3(0, 0, 0), Quaternion.identity);
            objUI.transform.SetParent(MateriUI.transform, false);
            Destroy(objUI, 1.5f);
        }
    }

    IEnumerator PhytagorasMateriAnim()
    {
        animLoadLevel.animLevel.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MapStudy");
    }

    public void TesLatihanUI()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
        StartCoroutine(TesLatihanUIAnim());
    }

    IEnumerator TesLatihanUIAnim()
    {
        animLoadLevel.animLevel.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("TesLatihan");
    }

    public void Customize()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        StartCoroutine(CustomizeAnim());
    }

    IEnumerator CustomizeAnim()
    {
        animLoadLevel.animLevel.SetTrigger("NextUI");
        yield return new WaitForSeconds(1f);
        MenuUI.SetActive(false);
        MateriUI.SetActive(false);
        CustomizeUI.SetActive(true);
        PickMapUI.SetActive(false);
        ProfilUI.SetActive(false);
    }

    public void Profil()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        StartCoroutine(ProfilAnim());
    }

    IEnumerator ProfilAnim()
    {
        animLoadLevel.animLevel.SetTrigger("NextUI");
        yield return new WaitForSeconds(1f);
        MenuUI.SetActive(false);
        MateriUI.SetActive(false);
        CustomizeUI.SetActive(false);
        PickMapUI.SetActive(false);
        ProfilUI.SetActive(true);
    }

    public void BacktoMenu()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        StartCoroutine(BacktoMenuAnim());
    }

    IEnumerator BacktoMenuAnim()
    {
        animLoadLevel.animLevel.SetTrigger("NextUI");
        yield return new WaitForSeconds(1f);
        MenuUI.SetActive(true);
        MateriUI.SetActive(false);
        CustomizeUI.SetActive(false);
        PickMapUI.SetActive(false);
        ProfilUI.SetActive(false);
        scorePrasyaratTextMenu.text = "Skor Prasyarat = " + ScorePrasyarat;
        scorePrasyaratTextMateri.text = "Skor Prasyarat = " + ScorePrasyarat;
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
        StartCoroutine(QuitGameAnim());
    }

    IEnumerator QuitGameAnim()
    {
        animLoadLevel.animLevel.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
}
