using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;

public class LINGQuery : MonoBehaviour {

    const float nearestDistance = 3.725f;
    const float furthestDistance = 7.777f;

    public ObjectCrowd ObjectCrowd;

    public int NearestCount;

    public int FurthestCount;

    private void Start()
    {
        UpdateManager.Instance.Updated += UpdateInternal;
    }

    //void Update()
    //{
    //    UpdateInternal();
    //}

    private void UpdateInternal()
    {
        Profiler.BeginSample("GetNearestCount");
        UpdateNearestCount();
        Profiler.EndSample();

        Profiler.BeginSample("GetFurthest");
        UpdateFurthestCount();
        Profiler.EndSample();
    }

    private void UpdateNearestCount()
    {
        const float nearestDistance = 3.725f;

        NearestCount = ObjectCrowd.SpawnedObjects.Count(o => o.transform.localPosition.x < nearestDistance);
    }

    private void UpdateFurthestCount()
    {
        //const float furthestDistance = 7.777f;

        var furthest = ObjectCrowd.SpawnedObjects.Where(o => o.transform.localPosition.x > furthestDistance);
        FurthestCount = furthest.Count();
    }
}
