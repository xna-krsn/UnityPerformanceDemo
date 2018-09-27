using System.Collections.Generic;
using UnityEngine;

using TypeGeneration;
using System.Reflection;

public class ObjectCrowd : MonoBehaviour {

    public int Count;
    public float Distance;

    public bool Run;

    private List<GameObject> _spawnedObjects;
    private List<TransformScaler> _spawnedTransfors;
    private float _startTime;

    //public List<GameObject> SpawnedObjects => _spawnedObjects;
    public List<GameObject> SpawnedObjects
    {
        get { return _spawnedObjects; }
    }

    void Awake ()
    {
        _spawnedObjects = new List<GameObject>(Count);
        _spawnedTransfors = new List<TransformScaler>(Count);

        //GenerateObjectsOfDifferentTypes();

        GenerateObjetsOfSameType();

        UpdateManager.Instance.Updated += UpdateInternal;
    }

    private void GenerateObjetsOfSameType()
    {
        for (int i = 0; i < Count; i++)
        {
            var go = SpawnObject(i, Distance);
            var component = go.AddComponent<TransformScaler>();
            _spawnedObjects.Add(go);

            _spawnedTransfors.Add(component);

            UpdateManager.Instance.Updated += component.UpdateInternal;
        }
    }

    private void GenerateObjectsOfDifferentTypes()
    {
        const int typeMaxCount = 100;

        var generatedTypes = TypeGenerator.Generate(typeMaxCount);

        var coeff = Count / typeMaxCount;
        for (int i = 0; i < Count; i++)
        {
            var go = SpawnObject(i, Distance / 2);
            var typeIndex = i / coeff;
            var component = go.AddComponent(generatedTypes[typeIndex]);

            _spawnedObjects.Add(go);

            //var methodInfo = generatedTypes[typeIndex].GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic);
            //UpdateManager.Instance.Updated += () => methodInfo.Invoke(component, null);
        }
    }

    private GameObject SpawnObject(int index, float distance)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(go.GetComponent<BoxCollider>());
        go.transform.parent = transform;
        var d = 2 * distance;
        go.transform.localPosition = new Vector3(Random.Range(-d, d), distance, Random.Range(-d, d));
        go.transform.localRotation = Random.rotation;
        go.transform.localScale *= 0.1f;

        return go;
    }

    void UpdateInternal () {
        if (!Run) return;

        UpdateTransform();
    }

    private void UpdateTransform()
    {
        _startTime += Time.deltaTime;

        for (var i = 0; i < _spawnedObjects.Count; i++)
        {
            var spawnedObject = _spawnedObjects[i];
            //var transformScaler = spawnedObject.GetComponent<TransformScaler>();
            var transformScaler = _spawnedTransfors[i];

            var localPosition = spawnedObject.transform.localPosition;
            localPosition.y = Distance - (transformScaler.G * _startTime * _startTime) / 2;

            if (localPosition.y < -Distance)
            {
                localPosition.y *= -1;
                _startTime = 0;
            }

            spawnedObject.transform.localPosition = localPosition;
        }
    }
}
