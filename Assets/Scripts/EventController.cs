using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{

    public void OnFire()
    {
        WeaponController.Instance.onFire.Invoke();
    }
}
