using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerDeathHandler : MonoBehaviour
{

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }
    private void Awake()
    {
        FindObjectOfType<AudioManager>()._mixer.SetFloat("MusicVolume", 0f);

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
        {
            Kill();
        }
#endif

        if (Input.GetKeyDown(KeyCode.R))
        {
            Kill();
        }
    }

    public void Kill()
    {
        FindObjectOfType<AudioManager>()._mixer.SetFloat("MusicVolume", -80f);
        anim.SetTrigger("Death");
        Destroy(GetComponent<PlayerMovement>());
        FindObjectOfType<AudioManager>().Play("Death");
        CameraShaker.Instance.ShakeOnce(3f, 5f, 0.2f, 0.2f);
        FindObjectOfType<GameManager>().PlayEffect();
        FindObjectOfType<GameManager>().SaveScore();
        if (SceneManager.GetActiveScene().buildIndex != 0&&! Input.GetKeyDown(KeyCode.R)) {
            FindObjectOfType<GameManager>().ShowRestart();
            FindObjectOfType<BlockSpawner>().enabled = false;
            Bird[] birds = FindObjectsOfType<Bird>();
            foreach (var bird in birds)
            {
                bird.FlyAway();
            }
        }

   

    }

    public void DestroyObj()
    {
        Destroy(gameObject);

    }

}
