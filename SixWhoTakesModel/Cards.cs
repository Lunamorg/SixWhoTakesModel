﻿using System;

public class Cards : IEquatable<Cards>, IComparable<Cards>
{
    private int value;

    public Cards(int value)
    {
        this.value = value;
    }

    public int CompareTo(Cards other)
    {
        if (other == null) return 1;

        if (other.getValue() == this.getValue()) return 0;

        if (other.getValue() < this.getValue()) return 1;

        return -1;
    }

    public bool Equals(Cards other)
    {
        if (other == null) return false;

        return other.getValue() == this.getValue();
    }

    public int getValue()
    {
        return value;
    }
}

