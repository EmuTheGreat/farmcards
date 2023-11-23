using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InterfaceData
{
    public int Balance;
    public int Water;
    public int Day;
    public InterfaceData(InterfaceManager manager) 
    {
        Balance = manager.balance; 
        Water = manager.water; 
        Day = manager.day;
    }
}
