using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LeanTweenType moveTweenType;
    public GameObject moveEffect;
    public GameObject myCamera;

    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                LeanTween.scale(transform.gameObject, new Vector3(0.7f, 0.7f, 0.7f), 0.5f).setEase(LeanTweenType.easeOutBounce);
                //CameraShaker();
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                MoveForward(transform.forward, 2);
            }
        }
        //Movement
       
       
        //Rotate
       if (Input.GetKeyDown(KeyCode.A))
       {
            Rotate(true);
       }
       if (Input.GetKeyDown(KeyCode.D))
       {
            Rotate(false);
       }
    }

    void MoveForward(Vector3 direction, float dist)
    {
        canMove = false;
        //transform.Translate(direction * dist);
        
        //LeanTween.move(transform.gameObject, (transform.position + (direction * dist)), 0.25f).setEase(moveTweenType);
        LeanTween.move(transform.gameObject, (transform.position + (direction * dist)), 0.25f).setEase(moveTweenType).setOnComplete(
        () => 
        {
            print("Just fin move!");
            MoveEffect();
            canMove = true;
        }
        ); 
        LeanTween.scale(transform.gameObject, new Vector3(1f, 1f, 1f), 0.5f).setEase(LeanTweenType.easeOutBounce);
        
    }

    void Rotate(bool left)
    {
        if (left)
        {
            //transform.Rotate(Vector3.up, -90f);
            LeanTween.rotate(transform.gameObject,transform.rotation.eulerAngles + new Vector3(0f,-90f,0f), 1.0f).setEase(LeanTweenType.easeOutElastic);
        }
        else
        {
            transform.Rotate(Vector3.up, 90f);
        }
        
    }

    void CameraShaker()
    {
        /**************
				* Camera Shake
				**************/

        float shakeAmt = 2f; // the degrees to shake the camera
        float shakePeriodTime = 0.42f; // The period of each shake
        float dropOffTime = 1f; // How long it takes the shaking to settle down to nothing
        LTDescr shakeTween = LeanTween.rotateAroundLocal(myCamera, Vector3.right, shakeAmt, shakePeriodTime)
        .setEase(LeanTweenType.easeShake) // this is a special ease that is good for shaking
        .setLoopClamp()
        .setRepeat(-1);

        // Slow the camera shake down to zero
        LeanTween.value(myCamera, shakeAmt, 0f, dropOffTime).setOnUpdate(
            (float val) => {
                shakeTween.setTo(Vector3.right * val);
            }
        ).setEase(LeanTweenType.easeOutCubic);
    }
    void MoveEffect()
    {
        Instantiate(moveEffect, transform.position, Quaternion.identity);
    }
}
