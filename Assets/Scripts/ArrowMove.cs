using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    public GameObject arrow;
    private SavedData savedData;

    private void Start()
    {
        savedData = FindObjectOfType<SavedData>();
        if (savedData.lastDotId != 0)
        {
            arrow.SetActive(false);
        }
    }

    void Update()
    {
        arrow.transform.position = new Vector3(transform.position.x,
            transform.position.y + 0.15f * Mathf.Sin(Mathf.PI * Time.time),
            transform.position.z);
    }
}
