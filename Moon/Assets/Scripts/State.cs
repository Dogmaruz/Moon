using UnityEngine;

public abstract class State : ScriptableObject
{
    [field: SerializeField] public bool IsFinished { get; set; }

    [HideInInspector] public IEntity Entity { get; set; }

    public virtual void Init() { }

    public abstract void Run();
}
