using System.Collections.Generic;
using UnityEngine;

public abstract class IEntity : MonoBehaviour
{
    [SerializeField] private bool IsTimeScaleEnabled;

    private static IEntity previousSelectedEntity;

    public float TimeScale = 1f;

    public bool IsObjectSelected = false;

    private List<Material> materials = new List<Material>();

    private void Awake()
    {
        var meshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach (var mesh in meshRenderers)
        {
            var material = mesh.materials[1];

            materials.Add(material);
        }
    }

    public virtual void SetTimeScale(float timeScale)
    {
        if (!IsTimeScaleEnabled) return;

        TimeScale = timeScale;
    }

    private void OnMouseDown()
    {
        if (previousSelectedEntity == this) return;

        if (previousSelectedEntity != null)
        {
            previousSelectedEntity.Deselect();
        }

        Select();
    }

    private void Select()
    {
        IsObjectSelected = true;

        foreach (var material in materials)
        {
            material.SetFloat("_Scale", -1.1f);
        }

        Debug.Log(materials.Count);

        previousSelectedEntity = this;

    }

    private void Deselect()
    {
        foreach (var material in materials)
        {
            material.SetFloat("_Scale", 0f);
        }

        previousSelectedEntity.IsObjectSelected = false;
    }
}

