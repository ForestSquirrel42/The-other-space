using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarPositionLocker : MonoBehaviour
{
    [SerializeField] Transform boss;
    [SerializeField] float verticalPadding = 2f;

    private void LateUpdate()
    {
        if(boss != null && transform != null)
            transform.position = new Vector3(boss.transform.position.x, boss.transform.position.y + verticalPadding, boss.transform.position.z);
    }
}
