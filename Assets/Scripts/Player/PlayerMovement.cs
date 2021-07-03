using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement related variables")]
    private Vector3 touchPosition;
    private Camera cam;
    private bool isAppeared;
    public static bool playerHasControl;

    [Header("Animation of tilting")]
    [SerializeField] Animator animator;
    private Vector3 lastTransformPosition;

    [Header("Hardcoded flying from (and to) offscreen")]
    [SerializeField] float fixedDeltaYChange;
    [SerializeField] float maxValue = 0.15f;
    [SerializeField] float minValue = 0.05f;
    [SerializeField] float YChange;
    [SerializeField] float counterLimit = 5f;
    [SerializeField] float fixedYDeltaChangeMinus = 0.0007f;
    [SerializeField] float fixedYDeltaChangePlus = 0.0001f;
    private float newY;
    private float counter = 0;

    [Header("References to other scripts")]
    [SerializeField] References refs;
    private DialogueTrigger StartLevelDialogue;

    

    private void Start()
    {
        StartLevelDialogue = refs.GetDialogueManagerOne().GetComponent<DialogueTrigger>();
        StartCoroutine(AppearOnScreen());
        cam = Camera.main;
    }

    void Update()
    {
        MovePlayer();
    }

    public float FixedDeltaYChange
    {
        get
        {
            return fixedDeltaYChange;
        }
    }

    private void MovePlayer()
    {
        if (Input.touchCount > 0 && playerHasControl == true)
        {
            Touch t = Input.GetTouch(0);
            touchPosition = cam.ScreenToWorldPoint(t.position);
            touchPosition.z = -1f;
            transform.position = Vector3.MoveTowards(transform.position, touchPosition, 0.5f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Tilting() // На случай если я захочу вернуть анимацию поворота корабля
     {
         if(transform.position.x < lastTransformPosition.x)
         {
             animator.SetInteger("Tilt", -1);
         }
         if (transform.position.x == lastTransformPosition.x)
         {
             animator.SetInteger("Tilt", 0);
         }
         if (transform.position.x > lastTransformPosition.x)
         {
             animator.SetInteger("Tilt", 1);
         }
     } 

    private IEnumerator StoringTransformPosition() // На случай если я захочу вернуть анимацию поворота корабля
    {
        while (true)
        {
            lastTransformPosition = new Vector3(transform.position.x, transform.position.y, -1f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator AppearOnScreen()
    {
        TimeTravelMachineAnimation.DeactivateTimeMachine();
        AudioManager.PlayAudioByName("Ship appears");

        YChange = Mathf.Clamp(maxValue, minValue, maxValue);

        while (playerHasControl == false && isAppeared == false)
        {
            if (YChange > minValue && counter < counterLimit)
            {
            YChange -= fixedYDeltaChangeMinus;
            }
            if (counter > counterLimit)
            {
                YChange += fixedYDeltaChangePlus;
                if (YChange >= 0)
                {
                    isAppeared = true;
                    playerHasControl = true;
                    StartLevelDialogue.TriggerDialogue();
                }
        }
        newY = transform.position.y + YChange;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        counter += Time.fixedDeltaTime;

        yield return new WaitForSeconds(Time.fixedDeltaTime / 3);
        }
    }

    public IEnumerator FlyOffScreen(float fixedDeltaYChange = 0.0015f)
    {
        playerHasControl = false;
        counter = 0;
        
        while(counter < 4f)
        {
            YChange += fixedDeltaYChange;
            newY = transform.position.y + (YChange);

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            counter += Time.fixedDeltaTime;

            yield return new WaitForSeconds(Time.fixedDeltaTime / 3);
        }
    }
}

