using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Range(0, 30)]
    [SerializeField] private float m_timeScale;

    [SerializeField] private List<IEntity> m_entities;

    private float _previousTimeScale;

    private void Start()
    {
        _previousTimeScale = m_timeScale;
    }

    void Update()
    {
        if (_previousTimeScale == m_timeScale) return;

        foreach (var entity in m_entities)
        {
            entity.SetTimeScale(m_timeScale);

            _previousTimeScale = m_timeScale;
        }
    }
}
