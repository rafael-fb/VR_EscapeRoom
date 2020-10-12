using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTarget : MonoBehaviour
{

    public GameObject gridPrefab;
    private GameObject grid;

    public void StartLevitation(Transform playerTransform)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        grid = Instantiate(gridPrefab, transform.position, Quaternion.identity);
        grid.transform.LookAt(playerTransform);
        grid.transform.rotation = new Quaternion(0f, grid.transform.rotation.y, 0f, grid.transform.rotation.w);
        print("start");
    }

    public void EndLevitation()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        Destroy(grid);
    }
}
