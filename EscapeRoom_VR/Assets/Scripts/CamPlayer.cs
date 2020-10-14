using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamPlayer : MonoBehaviour
{

    private Ray ray;
    private RaycastHit hit;
    public LayerMask gridMask;
    public LayerMask targetMask;
    private GameObject selectedMagicTarget;
    [HideInInspector] public WalkTarget previousWalkTarget;
    private GameObject inspectedObject;
    public GameObject inspectionObject;
    public GameObject inspectionParent;
    bool isSecondTouch;

    enum PlayerState { Normal, Levitating, Inspecting, }
    PlayerState playerState = PlayerState.Normal;


    private void Start()
    {
        roomIndex++;
        if (roomIndex <= lastRoomIndex)
        {
            timer += 60f;
            roomClue = roomClues[roomIndex - 1];
            UpdateHUD();
        }
    }

    private void Update()
    {

        if(isTimerOn)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
                ui_timer.text = (Mathf.Round(timer)).ToString();
            }
            else
            {
                BadEnd();
            }
        }


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
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, targetMask) && playerState == PlayerState.Normal)
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
            if (Input.GetButtonDown("Fire1"))
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
        yield return new WaitForSeconds(0.3f);
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
        inspectionParent.transform.SetParent(null);
        inspectionParent.SetActive(true);
    }
    void StopInspection()
    {
        playerState = PlayerState.Normal;
        inspectedObject.SetActive(true);
        inspectionParent.transform.SetParent(this.transform);
        inspectionParent.SetActive(false);
    }




    public int lastRoomIndex;
    public string[] roomClues; 
    int roomIndex;
    float timer;
    string roomClue;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish"))
        {
            isTimerOn = false;
            roomIndex++;
            if(roomIndex <= lastRoomIndex)
            {
                timer += 60f;
                roomClue = roomClues[roomIndex-1];
                UpdateHUD();
            }
            else
            {
                GoodEnd();
            }
        }
    }

    public Text ui_progress;
    public Text ui_timer;
    public Text ui_clue;
    bool isTimerOn;
    void UpdateHUD()
    {
        ui_progress.text = roomIndex.ToString() + " / " + lastRoomIndex.ToString();
        ui_clue.text = roomClue;
        isTimerOn = true;
    }

    void GoodEnd()
    {
        //Fim de jogo -- finalizou todas as salas
    }
    void BadEnd()
    {
        // game over -- time's up
    }
}
