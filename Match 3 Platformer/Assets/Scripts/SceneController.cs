using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public GameObject _sprite;
    Vector3 startingSize;


    private void Awake()
    {
        _sprite.SetActive(true);
        startingSize = _sprite.transform.localScale;
        instance = this;



    }

    private void Start()
    {
        _sprite.transform.DOScale(Vector3.zero,1f);
    }
    private void Update()
    {
       
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (SceneManager.GetActiveScene().buildIndex!=0)
        //    {
        //        StartCoroutine(LoadScene(1f, 0));
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.R)&&SceneManager.GetActiveScene().buildIndex!=0)
        {
            StartCoroutine(ReloadScene(1f,1f));
        }

    }
    public IEnumerator ReloadScene(float InAnimationDelay,float delay)
    {
        Debug.Log("reload Scene");
        yield return new WaitForSeconds(InAnimationDelay);
        ScaleUp();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator LoadNextScene(float InAnimationDelay, float delay)
    {
        Debug.Log("LoadNextScene");
        yield return new WaitForSeconds(InAnimationDelay);
        ScaleUp();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public IEnumerator LoadScene(float delay,int index)
    {
        ScaleUp();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(index);

    }

    void ScaleUp()
    {
        _sprite.transform.DOScale(startingSize, 2f);

    }
    public void ReloadSceneTester(float inAnimation,float delay)
    {
        StartCoroutine(ReloadScene(inAnimation,delay));
    }

    public void LoadSceneUI(int index) { StartCoroutine(LoadScene(1f, index)); }
}
