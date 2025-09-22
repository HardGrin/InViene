using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatValue
{

    private double value;
    private double weight;

    public StatValue(double value, double weight)
    {
        this.value = value;
        this.weight = weight;
    }

    public StatValue(double value)
    {
        this.value = value;
        this.weight = 1;
    }

    public StatValue()
    {
        this.value = 0;
        this.weight = 1;
    }

    public bool CheckLegal()
    {
        if (!MathUtil.CheckLegalStatValue(this)) return false;

        return true;
    }

    public virtual void SetEmpty()
    {
        value = 0f;
    }

    public bool IsEmpty()
    {
        if (value == 0f || weight == 0f)
        {
            return true;
        }
        return false;
    }

    public void SetValue(double value)
    {
        if (!MathUtil.CheckLegalValue(value))
        {
            throw new ArgumentException("Value is not legal: " + value);
        }
        this.value = value;
    }

    public double GetValue()
    {
        return value;
    }

    public void SetWeight(double weight)
    {
        this.weight = weight;
    }

    public double GetWeight()
    {
        return weight;
    }

    public virtual void Mix(double value2, double weight2)
    {
        (value, weight) = MathUtil.StatMix(value, weight, value2, weight2);
    }

    public virtual void AddValue(double value)
    {
        this.value += value;
    }

    public virtual void DecimateValue(double value)
    {
        this.value -= value;
    }

    public virtual void MultiplyValue(double value)
    {
        this.value *= value;
    }

    public virtual void DivideValue(double value)
    {
        this.value /= value;
    }

    public virtual void FloorValue()
    {
        value = Math.Floor(value);
    }

    public virtual void Clamp(double min, double max)
    {
        if (min > max)
            throw new ArgumentException("min cannot be greater than max");

        value = Math.Max(min, Math.Min(max, value));
    }

    public StatValue Copy()
    {
        return new StatValue(value, weight);
    }
    
}