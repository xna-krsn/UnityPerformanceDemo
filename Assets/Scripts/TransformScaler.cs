using UnityEngine;

public class TransformScaler : MonoBehaviour
{
    private float _initialScale;
    private float _targetScale;
    private float _time;

    public float G { get; private set; }

    void Start()
    {
        const float minScale = 0.05f;
        const float maxScale = 0.45f;

        _initialScale = transform.localScale.x;
        _targetScale = Random.Range(minScale, maxScale);

        const float minG = 0.8f;
        const float maxG = 2.0f;

        G = Random.Range(minG, maxG);
    }

    //private void Update()
    //{
    //    //UpdateInternal();
    //}

    //public void UpdateInternal()
    //{

    //}

    public void UpdateInternal()
    {
        const float time = 3;

        if (_time < time)
        {
            _time += Time.deltaTime;

            transform.localScale = Vector3.one * Mathf.Lerp(_initialScale, _targetScale, _time / time);
        }
        else
        {
            _time = 0;

            var tmp = _targetScale;
            _targetScale = _initialScale;
            _initialScale = tmp;
        }
    }
}
