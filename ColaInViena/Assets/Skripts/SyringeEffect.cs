using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeEffect : MonoBehaviour
{
    public List<int> statuses;

    public void ChandgeStatuses()
    {
        Status.instance.ChandgeStatus(statuses);
    }
}
