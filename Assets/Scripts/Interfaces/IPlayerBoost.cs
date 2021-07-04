using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerBoost
{
    IEnumerator StartTimer(float countdownValue);
    void MoveBoostItem();
    void DestroyBooster();
}
