using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPlayer : MonoBehaviour
{

    private Ray ray;
    private RaycastHit hit;
    public LayerMask gridMask;
    private GameObject selectedMagicTarget;
    [HideInInspector] public WalkTarget previousWalkTarget;
    private GameObject inspectedObject;
    public GameObject inspectionObject;
    bool isSecondTouch;

    enum PlayerState { Normal, Levitating, Inspecting, }
    PlayerState playerState = PlayerState.Normal;


    private void Update()
    {

        // TESTE - INPUTS
        if(Input.GetKey(KeyCode.W))
        {
            transform.Rotate(Vector3.left * 75f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(Vector3.right * 75f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * 75f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * 75f * Time.deltaTime);
        }


        ray = new Ray(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.forward);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity) && playerState == PlayerState.Normal)
        {
            // Select a magic target
            if(hit.collider.GetComponent<MagicTarget>() && Input.GetButtonDown("Fire1"))
            {
                selectedMagicTarget = hit.collider.gameObject;
                selectedMagicTarget.GetComponent<MagicTarget>().StartLevitation(this.transform);
                playerState = PlayerState.Levitating;
            }

            // Interact with anything that has tag + interface 'Interactable' by the function 'InteractAction()'
            if(hit.collider.CompareTag("Interactable") && Input.GetButtonDown("Fire1"))
            {
                hit.collider.GetComponent<Interactable>().InteractAction();
            }

            // Inspect objects of type SimpleObject
            if (hit.collider.GetComponent<InspectionObject>() && Input.GetButtonDown("Fire1"))
            {
                InspectObject(hit.collider.gameObject);
            }

        }

        if(playerState == PlayerState.Levitating)
        {
            // Move magic target along the grid
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, gridMask))
            {
                if (hit.collider.CompareTag("MagicGrid"))
                {
                    selectedMagicTarget.transform.position = hit.point;
                }
            }
            else
            {
                //StopLevitation();
            }

            // Stop Levitation Magic
            if(Input.GetButtonUp("Fire1"))
            {
                StopLevitation();
            }
        }


        if(playerState == PlayerState.Inspecting)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                if(!isSecondTouch)
                {
                    StartCoroutine(SecondTouchTimer());
                }
                else
                {
                    StopInspection();
                }
            }
        }

    }
    IEnumerator SecondTouchTimer()
    {
        isSecondTouch = true;
        yield return new WaitForSeconds(0.5f);
        isSecondTouch = false;
    }


    void StopLevitation()
    {
        playerState = PlayerState.Normal;
        selectedMagicTarget.GetComponent<MagicTarget>().EndLevitation();
        selectedMagicTarget = null;
    }


    void InspectObject(GameObject mesh)
    {
        playerState = PlayerState.Inspecting;
        inspectedObject = mesh;

        inspectionObject.GetComponent<MeshFilter>().mesh = inspectedObject.GetComponent<MeshFilter>().mesh;
        inspectionObject.GetComponent<MeshRenderer>().material = inspectedObject.GetComponent<MeshRenderer>().material;
        inspectedObject.SetActive(false);
        inspectionObject.SetActive(true);
    }
    void StopInspection()
    {
        playerState = PlayerState.Normal;
        inspectedObject.SetActive(true);
        inspectionObject.SetActive(false);
    }
}
