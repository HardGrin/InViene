using System;
using System.Collections.Generic;
using UnityEngine.AI;

public static class MathUtil
{

    public static bool CheckLegalStatValue(StatValue statValue)
    {
        if (!ValueAboveZero(statValue.GetWeight())) return false;
        if (!ValueAboveZero(statValue.GetValue()) && !statValue.IsAbsolute()) return false;

        return true;
    }

    public static bool ValueAboveZero(double value)
    {
        if (value < 0f)
        {
            return false;
        }
        return true;
    }

    public static StatValue StatMix(StatValue statValue, StatValue statValue2)
    {

        double aWeight = statValue.GetWeight() + statValue2.GetWeight();

        double newWeight = aWeight / 2f;


        if (aWeight == 0f)
        {
            return new StatValue();
        }

        if (!statValue.IsAbsolute() && !statValue2.IsAbsolute())
        {
            double newValue = (statValue.GetValue() * statValue.GetWeight() + statValue2.GetValue() * statValue2.GetWeight()) / aWeight;
            return new StatValue(newValue, newWeight, false);
        }

        if (statValue.IsAbsolute() && statValue2.IsAbsolute())
        {
            double newValue = (statValue.GetValue() * statValue.GetWeight() + statValue2.GetValue() * statValue2.GetWeight()) / aWeight;
            return new StatValue(newValue, newWeight, true);
        }

        // one absolute, one relative
        StatValue abs = statValue.IsAbsolute() ? statValue : statValue2;
        StatValue rel = statValue.IsAbsolute() ? statValue2 : statValue;

        // relative modifies absolute
        double newVal = (
            (abs.GetValue() * abs.GetWeight()) +
            (abs.GetValue() * rel.GetValue() * rel.GetWeight())
        ) / aWeight;

        return new StatValue(newVal, newWeight, true);

    }


}