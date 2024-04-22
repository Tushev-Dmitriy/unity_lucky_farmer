using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedStatusController : MonoBehaviour
{
    public enum Status { EMPTY, GROW, READY, PLOW };
    public Status status = Status.EMPTY;

    public void SetStatus(Status newStatus)
    {
        status = newStatus;
    }

    public Status GetStatus()
    {
        return status;
    }
}
