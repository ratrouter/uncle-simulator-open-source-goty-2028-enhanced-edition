using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class FpsCounter : MonoBehaviour
{
    [SerializeField] private float _expSmoothingFactor = 0.9f; // 0.9 = strong smoothing, 0.1 = responsive
    [SerializeField] private float _refreshFrequency = 0.4f;   // Update UI every X seconds

    private float _timeSinceUpdate = 0f;
    private float _averageFps = 1f;
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        // Exponentially weighted moving average (EWMA)
        _averageFps = _expSmoothingFactor * _averageFps + (1f - _expSmoothingFactor) * (1f / Time.unscaledDeltaTime);

        _timeSinceUpdate += Time.deltaTime;

        if (_timeSinceUpdate >= _refreshFrequency)
        {
            int fps = Mathf.RoundToInt(_averageFps);
            _text.text = $"FPS: {fps}";
            _timeSinceUpdate = 0f;
        }
    }
}