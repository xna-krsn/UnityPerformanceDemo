using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Profiling.Memory.Experimental;

public class MultiarrayIteration : MonoBehaviour {

    public int Count;

    public string CurrentItem;
    private Vector3 _currentPosition;

    //private Transform[,] _transforms;
    //private Vector3[,] _positions;

    private Transform[][] _transforms;
    private Vector3[][] _positions;

    private void Start()
    {
        MemoryProfiler.TakeSnapshot("Snapshot", ((s, b) => Debug.Log(s + b)), CaptureFlags.ManagedObjects|CaptureFlags.NativeObjects);

        _transforms = new Transform[Count][];
        _positions = new Vector3[Count][];

        for (int i = 0; i < Count; i++)
        {
            _positions[i] = new Vector3[Count];
            for (int j = 0; j < Count; j++)
            {
                //_transforms[i, j] = new GameObject("obj" + i).transform;
                _positions[i][j] = Vector3.one * i;
                //_transforms[i, j].position = _positions[i, j];
            }
        }
    }

    void Update()
    {
        IterateByRows();

        IterateByColumns();
    }

    private void IterateByColumns()
    {
        Profiler.BeginSample("IterateByColumns");

        for (int i = 0; i < Count; i++)
        {
            for (int j = 0; j < Count; j++)
            {
               // _currentPosition = _transforms[j, i].position;
                _currentPosition = _positions[j][i];
            }
        }

        Profiler.EndSample();
    }

    private void IterateByRows()
    {
        Profiler.BeginSample("IterateByRows");

        for (int i = 0; i < Count; i++)
        {
            for (int j = 0; j < Count; j++)
            {
               // _currentPosition = _transforms[i, j].position;
                _currentPosition = _positions[i][j];
            }
        }

        Profiler.EndSample();
    }

}
