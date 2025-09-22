using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatValue
{

    private double value;

    /*
    Weight is the importancy of the value in the calculations. 1 is normal level, higher = more, lower = less.
    Cannot be less than 0. If equal to zero it means that it means nothing and therefore it will not even be
    calculated.
    */
    private double weight;
    
    /*
    Absolute boolean means that the value is either absolute or relative. Absolute
    value ex: +50, relative value ex: +50% (value will be 1.5d). Relative value cannot
    be below zero.
    */
    private bool absolute;

    public StatValue(double value, double weight, bool absolute)
    {
        this.value = value;
        this.weight = weight;
        this.absolute = absolute;
    }

    public StatValue(double value, double weight)
    {
        this.value = value;
        this.weight = weight;
        this.absolute = true;
    }

    public StatValue(double value)
    {
        this.value = value;
        this.weight = 1;
        this.absolute = true;
    }

    public StatValue()
    {
        this.value = 0;
        this.weight = 1;
        this.absolute = true;
    }

    public bool CheckLegal()
    {
        if (!MathUtil.CheckLegalStatValue(this)) return false;

        return true;
    }

    public virtual void SetEmpty()
    {
        SetValue(0f);
    }

    public bool IsEmpty()
    {
        if (GetValue() == 0f || GetWeight() == 0f)
        {
            return true;
        }
        return false;
    }

    // Use this method for hooking, not SetValue itself to not break anything
    public virtual double HookSetValue(double value)
    {
        // Do nothing
        return value;
    }

    public void SetValue(double value)
    {
        if (!MathUtil.ValueAboveZero(value) && !IsAbsolute())
        {
            throw new ArgumentException("Value is not legal: " + value);
        }
        value = HookSetValue(value);
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

    public virtual void Mix(StatValue statValue)
    {
        StatValue newStatValue = MathUtil.StatMix(this, statValue);
        this.value = newStatValue.value;
        this.weight = newStatValue.weight;
        this.absolute = newStatValue.absolute;
    }

    public virtual void AddValue(double value)
    {
        SetValue(GetValue() + value);
    }

    public virtual void DecimateValue(double value)
    {
        SetValue(GetValue() - value);
    }

    public virtual void MultiplyValue(double value)
    {
        SetValue(GetValue() * value);
    }

    public virtual void DivideValue(double value)
    {
        SetValue(GetValue() / value);
    }

    public virtual bool IsAbsolute()
    {
        return absolute;
    }

    public virtual void SetAbsolute(bool absolute)
    {
        this.absolute = absolute;
    }

    public virtual void FloorValue()
    {
        SetValue(Math.Floor(value));
    }

    public virtual void Clamp(double min, double max)
    {
        if (min > max)
            throw new ArgumentException("min cannot be greater than max");

        SetValue(Math.Max(min, Math.Min(max, value)));
    }

    public StatValue Copy()
    {
        return new StatValue(value, weight);
    }
    
}