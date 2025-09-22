using System.Collections.Generic;
using UnityEngine;

public class LiquidStats : MonoBehaviour
{
    private Dictionary<string, StatValue> stats = new Dictionary<string, StatValue>();

    public StatValue GetStatValue(string name)
    {
        return stats.GetValueOrDefault(name);
    }

    public bool SetStatValue(string name, StatValue statValue)
    {
        if (stats.ContainsKey(name))
        {
            stats[name] = statValue;
            return true;
        }
        return false;
    }

    public bool AddStatValue(string name, StatValue statValue)
    {
        if (!stats.ContainsKey(name))
        {
            stats.Add(name, statValue);
            return true;
        }
        return false;
    }

    public bool AddStatValue(string name)
    {
        if (!stats.ContainsKey(name))
        {
            stats.Add(name, new StatValue());
            return true;
        }
        return false;
    }

    public int RemoveEmptyStatValues()
    {
        List<string> keysToRemove = new List<string>();

        foreach (var kvp in stats)
        {
            if (kvp.Value.IsEmpty())
            {
                keysToRemove.Add(kvp.Key);
            }
        }

        foreach (string key in keysToRemove)
        {
            stats.Remove(key);
        }

        return keysToRemove.Count;
    }

    public void Mix(LiquidStats liquidStats)
    {
        if (liquidStats == null) return;

        foreach (var kvp in liquidStats.stats)
        {
            string key = kvp.Key;
            StatValue otherStat = kvp.Value;

            if (stats.ContainsKey(key))
            {
                stats[key].Mix(otherStat.GetValue(), otherStat.GetWeight());
            }
            else
            {
                stats.Add(key, otherStat.Copy());
            }
        }
    }


}