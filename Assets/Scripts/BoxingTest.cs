using System.Collections.Generic;
using UnityEngine;

public enum TestEnum
{
    Val1,
    Val2,
    Val3,
    Val4,
    Val5
}

public struct TestStruct
{
    public int X;
    public int Y;
    public int Z;

}


public class BoxingTest : MonoBehaviour
{
    public Vector3 Position1;
    public Vector3 Position2;
    public Vector3 Position3;
    public Vector3 Position4;
    public Vector3 Position5;

    private ObjectCrowd _objects;
    private Dictionary<TestEnum, GameObject> _testDictionary;

    private void Start()
    {
        _objects = FindObjectOfType<ObjectCrowd>();

        _testDictionary = new Dictionary<TestEnum, GameObject>(_objects.Count);

        for (int i = 0; i < _objects.Count; i++)
        {
            _testDictionary[(TestEnum) i] = _objects.SpawnedObjects[i];
        }

        UpdateManager.Instance.Updated += UpdateInternal;
    }

    private void UpdateInternal()
    {
        Position1 = _testDictionary[TestEnum.Val1].transform.position;
        Position2 = _testDictionary[TestEnum.Val2].transform.position;
        Position3 = _testDictionary[TestEnum.Val3].transform.position;
        Position4 = _testDictionary[TestEnum.Val4].transform.position;
        Position5 = _testDictionary[TestEnum.Val5].transform.position;
    }
}
