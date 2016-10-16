using UnityEngine;
using System.Collections.Generic;

public class MultipleScale : MonoBehaviour {

    private List<Vector3> scales = new List<Vector3>();
    public Vector3 this[int pos] {
        get{
            return scales[pos];
        }
        set{
            scales[pos] = value;
            recalculateScale();
        }
    }

    public int NewScale()
    {
        return NewScale(Vector3.one);
    }

    public int NewScale(Vector3 scale)
    {
        scales.Add(scale);
        recalculateScale();
        return scales.Count-1;
    }

    private void recalculateScale()
    {
        Vector3 finalScale = Vector3.one;
        foreach (Vector3 scale in scales)
        {
            finalScale.x *= scale.x;
            finalScale.y *= scale.y;
            finalScale.z *= scale.z;
        }
        transform.localScale = finalScale;
    }

}
