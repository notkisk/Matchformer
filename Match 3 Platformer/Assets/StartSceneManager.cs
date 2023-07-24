using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    public Animator textAnimator;
    bool hasStarted = false;
    public GameObject music;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && hasStarted==false)
        {
            hasStarted = true;
            textAnimator.SetTrigger("Fast");
            FindObjectOfType<AudioManager>().Play("Start");
            FindObjectOfType<GameManager>().FlashEffect();
            FindObjectOfType<PlayerDeathHandler>().Kill();
            StartCoroutine (FindObjectOfType<SceneController>().LoadNextScene(1f,1));
            Destroy(music);
        }
    }
}
