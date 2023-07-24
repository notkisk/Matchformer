using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using EZCameraShake;
using UnityEngine.Rendering.PostProcessing;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    const string HIGH_SCORE = "HighScore";

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    PostProcessVolume postproccesingVolume;
    [HideInInspector]
    public int score;
    int highScore;
    public int combo = 1;
    public float comboDuration=3f;
    float timeBtwCombo;
    public GameObject comboEffect;
    public GameObject flashEffect;
    public GameObject restartObj;
    bool hasCollapsed = false;
    private void Awake()
    {
        postproccesingVolume=Camera.main.GetComponent<PostProcessVolume>();
        LoadHighScore();
        score = 0;
    }
    private void Update()
    {
        if (timeBtwCombo > 0f) timeBtwCombo -= Time.deltaTime;
        else combo = 1;
        if (restartObj)
        {
            if (restartObj.activeInHierarchy && Input.GetKeyDown(KeyCode.R) && !hasCollapsed && SceneManager.GetActiveScene().buildIndex == 2)
            {
                hasCollapsed = true;
                restartObj.GetComponent<Animator>().SetTrigger("Collapse");
            }
        }
     
    }
    public void AddScore(int amount)
    {
        //Combo();
        timeBtwCombo=comboDuration;
        score +=amount* combo;
        scoreText.text = score.ToString();
        CameraShaker.Instance.ShakeOnce(3f, 3f, 0.05f, 0.05f);
        PlayEffect();

    }

    public void LoadHighScore()
    {
        highScore= PlayerPrefs.GetInt(HIGH_SCORE, 0);
        highScoreText.text = highScore.ToString();
    }

    public void SaveScore()
    {
        if (score>PlayerPrefs.GetInt(HIGH_SCORE, 0))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, score);
        }
    }

    public void PlayEffect()
    {
        postproccesingVolume.profile.GetSetting<ChromaticAberration>().active = false;
        StartCoroutine(EffectsCourotine());

    }


    IEnumerator EffectsCourotine()
    {
        postproccesingVolume.profile.GetSetting<ChromaticAberration>().active = true;
        yield return new WaitForSeconds(0.1f);
        postproccesingVolume.profile.GetSetting<ChromaticAberration>().active = false;

    }
   public void Combo()
    {
        if (combo > 1)
        {
            var _comboEffect = Instantiate(comboEffect, Vector3.zero, Quaternion.identity);
            _comboEffect.GetComponent<ComboEffect>().Initialize(combo);
            FlashEffect();
        }
   
        if (timeBtwCombo > 0f)
            {
                combo += 1;
            }
      
       
        }

    public void FlashEffect()
    {
        Instantiate(flashEffect, Vector3.zero, Quaternion.identity);

    }

    public void ShowRestart()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) return;
        restartObj.SetActive(true);
    }

}



