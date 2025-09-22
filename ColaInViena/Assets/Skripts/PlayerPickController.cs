using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickController : MonoBehaviour
{
    public float rayLength;
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject syringeInHand;
    public Animator injectionAnim;
    public Animator leftHandAnimation;

    public VolumeController volumeController;

    private void Start()
    {
        volumeController.enabled = false;
    }

    public void OnIjektEnd()
    {
        Debug.Log("Анимация лечения завершилась!");
        
        syringeInHand.transform.parent = null;
        syringeInHand.transform.parent = leftHandAnimation.gameObject.transform;
        //syringeInHand.AddComponent<Rigidbody>();
        syringeInHand.layer = 0;
        leftHandAnimation.SetTrigger("LHA_back");
        volumeController.enabled = true;
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            injectionAnim.SetTrigger("inject");
            leftHandAnimation.SetTrigger("LHA_Trigger");
        }



        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            // Берём центр камеры
            Camera cam = Camera.main;
            if (cam == null) return;

            Vector3 origin = cam.transform.position;
            Vector3 direction = cam.transform.forward;

            // Raycast
            if (Physics.Raycast(origin, direction, out RaycastHit hit, rayLength, targetLayers))
            {
                syringeInHand = hit.collider.gameObject;
                syringeInHand.transform.parent = hand.transform;
                syringeInHand.transform.localPosition = new Vector3(0,0,0);
                syringeInHand.transform.localRotation = Quaternion.Euler(0, 180, 0);//hand.transform.rotation;

            }
            else
            {
                Debug.Log("Ничего не задетектилось");
            }
        }
    }
        
    // Чтобы луч рисовался в редакторе
    private void OnDrawGizmos()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(cam.transform.position, cam.transform.forward * rayLength);
    }
}
