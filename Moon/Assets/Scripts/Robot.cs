using UnityEngine;

public class Robot : IEntity
{
    [field: SerializeField] public float MaxEnergy { get; set; }

    private float _currentEnergy;
    public float CurrentEnergy { get => _currentEnergy; }

    [field: SerializeField] public Transform EnergyBase { get; set; }

    [SerializeField] private float m_extract = 1f;

    [SerializeField] private State m_startState;

    [SerializeField] private State m_energyState;

    [SerializeField] private State m_moveToMousePositionState;

    [SerializeField] private State m_moveToWarehouseState;

    [SerializeField] private State m_extractState;

    [field: SerializeField] public Animator Animator { get; private set; }

    public State CurrentState { get; private set; }

    private float _energyEndTime = 15f;

    public bool IsMoving = false;

    //private float _extrtactEndTime = 30f;

    private void Start()
    {
        _currentEnergy = MaxEnergy;

        SetState(m_startState);
    }

    private void Update()
    {
        Animator.speed = TimeScale;

        if (IsMoving)
        {
            _currentEnergy -= Time.deltaTime * TimeScale / _energyEndTime;
        }

        //m_extract -= Time.deltaTime / _extrtactEndTime;

        if (!CurrentState.IsFinished)
        {
            CurrentState.Run();
        }
        else
        {
            if (_currentEnergy <= 0f)
            {
                SetState(m_energyState);
            }
            else
            {
                if (IsObjectSelected)
                {
                    SetState(m_moveToMousePositionState);
                }
            }
        }
    }

    public void SetCurrentEnergy(float energy)
    {
        _currentEnergy = energy;
    }

    public void SetState(State state)
    {
        CurrentState = Instantiate(state);

        CurrentState.Entity = this;

        CurrentState.Init();
    }

    public void MoveTo(Vector3 position)
    {
        position.y = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * TimeScale);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(position - transform.position), Time.deltaTime * TimeScale * 200f);
    }
}
