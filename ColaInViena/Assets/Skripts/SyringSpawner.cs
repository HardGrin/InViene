using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringSpawner : Status
{
    [SerializeField] GameObject syring;
    public List<int> States;

    void Start()
    {
        
        GameObject newSh = Instantiate(syring, transform.position, transform.rotation);
        newSh.GetComponent<SyringeEffect>().statuses = States;
    }
}

