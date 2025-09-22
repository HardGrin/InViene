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

    public Dictionary<string, int> playerScores = new Dictionary<string, int>();
    private void Start()
    {

        var keys = new List<string>(playerScores.Keys);
        foreach (var stat in keys)
        {
            playerScores[stat] = 50;
        }
    }
}
