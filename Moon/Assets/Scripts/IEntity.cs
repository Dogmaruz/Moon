using UnityEngine;

public abstract class IEntity : MonoBehaviour
{
    [SerializeField] private bool IsTimeScaleEnabled;

    private static IEntity previousSelectedEntity;

    public float TimeScale = 1f;

    public bool IsObjectSelected = false;

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

        previousSelectedEntity = this;

    }

    private void Deselect()
    {
        previousSelectedEntity.IsObjectSelected = false;
    }
}

