using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringSpawner : Status
{
    [SerializeField] GameObject syring;
    void Start()
    {
        GameObject newSh = Instantiate(syring,gameObject.transform.position,gameObject.transform.rotation);
        newSh.GetComponent<SyringeEffect>().statuses = States;
    }
}
