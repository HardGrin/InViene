using System;
using System.Collections.Generic;

public static class MathUtil
{

    public static bool CheckLegalStatValue(StatValue statValue)
    {
        if (!CheckLegalValue(statValue.GetValue())) return false;
        if (!CheckLegalValue(statValue.GetWeight())) return false;

        return true;
    }

    public static bool CheckLegalValue(double value)
    {
        if (!ValueAboveZero(value)) return false;

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

    public static (double newValue, double newWeight) StatMix(double value1, double weight1, double value2, double weight2)
    {
        double newWeight = (weight1 + weight2) / 2f;

        double aWeight = weight1 + weight2;

        if (aWeight == 0f)
        {
            return (0f, newWeight);
        }

        double newValue = (value1 * weight1 + value2 * weight2) / aWeight;

        return (newValue, newWeight);
    }


}