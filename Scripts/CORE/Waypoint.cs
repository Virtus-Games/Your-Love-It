using UnityEngine;

public class Waypoint : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int j = GetNextIndex(i);

            Gizmos.color = Color.blue;

            Gizmos.DrawSphere(GetPosition(i).position, 0.15f);

            Gizmos.DrawLine(GetPosition(i).position, transform.GetChild(j).position);
        }
    }


    public bool GetLastIndex()
    {
        if (j == transform.childCount - 2)
            return true;

        else return false;
    }

    public int GetChildIndex(Transform child)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i) == child)
                return i;
        }

        return -1;
    }

    int j;

    public int GetNextIndex(int i)
    {
       j = i + 1;

        if (j == transform.childCount)
        {
            j = transform.childCount - 1;
            lastPosition(j);
        }

        return j;
    }

    public Transform GetPosition(int i) => transform.GetChild(i);

    public bool lastPosition(int j)
    {
        if (j == transform.childCount - 1)
            return true;

        else return false;
    }
}
