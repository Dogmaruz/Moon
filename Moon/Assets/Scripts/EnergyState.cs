using UnityEngine;

[CreateAssetMenu(fileName = "EnergyState", menuName = "State/EnergyState")]
public class EnergyState : State
{
    private Vector3 _lastEntityPosition = Vector3.zero;

    private bool _isChargingStarted;

    private float _chargingTime = 5f;

    private Robot _robot;

    private float _maxEnergy;

    public override void Init()
    {
        _robot = (Entity as Robot);

        _maxEnergy = _robot.MaxEnergy;
    }

    public override void Run()
    {
        if (IsFinished) return;

        if (_isChargingStarted == false)
        {
            MoveToBase();
        }
        else
        {
            DoCharging();
        }
    }

    private void MoveToBase()
    {
        var dist = Vector3.Distance(new Vector3(_robot.EnergyBase.position.x, 0, _robot.EnergyBase.position.z), new Vector3(_robot.transform.position.x, 0, _robot.transform.position.z));

        if (dist < 2 && _lastEntityPosition == Vector3.zero)
        {
            _lastEntityPosition = _robot.transform.position;
        }

        if (dist > 0.1f)
        {
            _robot.Animator.Play("Move");

            _robot.MoveTo(_robot.EnergyBase.position);

            _robot.IsMoving = false;
        }
        else
        {
            _robot.transform.position = new Vector3(_robot.EnergyBase.position.x, _robot.transform.position.y, _robot.EnergyBase.position.z);

            _robot.Animator.Play("Charging");

            _robot.IsMoving = false;

            _isChargingStarted = true;
        }
    }

    private void DoCharging()
    {
        _chargingTime -= Time.deltaTime * _robot.TimeScale;

        if (_chargingTime > 0) return;

        var dist = Vector3.Distance(new Vector3(_robot.transform.position.x, 0, _robot.transform.position.z), new Vector3(_lastEntityPosition.x, 0, _lastEntityPosition.z));

        if (dist > 0.1f)
        {
            _robot.Animator.Play("Move");

            _robot.MoveTo(_lastEntityPosition);

            _robot.IsMoving = false;
        }
        else
        {
            _robot.Animator.Play("Idel");

            _robot.IsMoving = false;

            _robot.transform.position = _lastEntityPosition;

            _robot.SetCurrentEnergy(_maxEnergy);

            IsFinished = true;

            _isChargingStarted = false;

            _lastEntityPosition = Vector3.zero;
        }
    }
}
