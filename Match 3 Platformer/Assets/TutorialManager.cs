using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public float timeBeforeNextInstruction = 3f;
    bool hasPressedRight,hasPressedLeft;

    float tempTime;

    public GameObject movementInstruction;
    public GameObject carryInstruction;
    public GameObject throwInstruction;
    public GameObject finlInstruction;
    public GameObject funnyInstruction;


    bool hasChangedFirstInstruction = false;
    bool hasChangedSecondInstruction = false;
    bool hasChangedThirdInstruction = false;
    bool hasChangedFinalInstruction = false;
    bool hasChangedFunnyInstruction = false;
    public float timeBeforeFinalInstruction = 3f;

    bool hasStarted = false;
    bool hasSpawndedMagentaBlock = false;
    // Start is called before the first frame update
    void Start()
    {
        tempTime = timeBeforeNextInstruction;


    }

    // Update is called once per frame
    void Update()
    {
        tempTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.D))
        {
            hasPressedRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.A))
        {
            hasPressedLeft = true;
        }

        if ((hasPressedLeft&&hasPressedRight)||tempTime<=0f)
        {
            if (hasChangedFirstInstruction == false)
            {
                hasChangedFirstInstruction = true;
                FindObjectOfType<AudioManager>().Play("Change");
                movementInstruction.SetActive(false);
                FindObjectOfType<AudioManager>().Play("Change");
                carryInstruction.SetActive(true);
                FindObjectOfType<BlockSpawner>().SpawnBlueBlock();
            }
        }

        if (FindObjectOfType<BlockCarrier>().blocksCarried == 1 && hasChangedSecondInstruction == false)
        {
            hasChangedSecondInstruction = true;
            FindObjectOfType<AudioManager>().Play("Change");
            carryInstruction.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Change");
            throwInstruction.SetActive(true);
            StartCoroutine(FinalInstructionCourotine());

        }
        else if (FindObjectOfType<BlockCarrier>().blocksCarried==1 && FindObjectOfType<BlockCarrier>().isCarrieng==false && hasSpawndedMagentaBlock==false)
        {
            hasSpawndedMagentaBlock = true;
            FindObjectOfType<BlockSpawner>().SpawnMagentaBlock();
        }
        else if (FindObjectOfType<BlockCarrier>().blocksCarried == 2 && hasChangedFinalInstruction == false&&FindObjectOfType<BlockCarrier>().isCarrieng==false)
        {
            hasChangedFinalInstruction = true;
            FindObjectOfType<AudioManager>().Play("Change");
            finlInstruction.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Change");
            StartCoroutine(FunnyCourotine());
        }


        if (hasChangedFunnyInstruction && !hasStarted)
        {
            hasStarted = true;

            StartCoroutine(Somethin());
                //FindObjectOfType<AudioManager>().Play("Start");
                //FindObjectOfType<GameManager>().FlashEffect();
                //FindObjectOfType<PlayerDeathHandler>().Kill();
                //StartCoroutine(FindObjectOfType<SceneController>().LoadNextScene(3f, 1f));
            
        }
         
    }

    IEnumerator FinalInstructionCourotine()
    {
        yield return new WaitForSeconds(timeBeforeFinalInstruction);
        if (hasChangedThirdInstruction == false)
        {
            hasChangedThirdInstruction = true;
            FindObjectOfType<AudioManager>().Play("Change");
            throwInstruction.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Change");
            finlInstruction.SetActive(true);
        }
    }

    IEnumerator FunnyCourotine()
    {
        yield return new WaitForSeconds(1.5f);
        funnyInstruction.SetActive(true);
        hasChangedFunnyInstruction = true;

    }


    IEnumerator Somethin()
    {
        yield return new WaitForSeconds(0.5f) ;
        FindObjectOfType<AudioManager>().Play("Start");
        FindObjectOfType<GameManager>().FlashEffect();
        FindObjectOfType<PlayerDeathHandler>().Kill();
        StartCoroutine(FindObjectOfType<SceneController>().LoadNextScene(3f, 1f));
    }
}
