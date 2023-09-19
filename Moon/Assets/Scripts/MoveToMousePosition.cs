using UnityEngine;

[CreateAssetMenu(fileName = "MoveToMousePositionState", menuName = "State/MoveToMousePositionState")]
public class MoveToMousePosition : State
{
    [SerializeField] private LayerMask groundLayer;

    private Robot _robot;

    private Camera mainCamera;

    private bool isMoving = false;

    private Vector3 _targetPosition;

    public override void Init()
    {
        _robot = (Entity as Robot);

        mainCamera = Camera.main;
    }

    public override void Run()
    {
        if (_robot.CurrentEnergy <= 0f)
        {
            IsFinished = true;
        }

        if (IsFinished) return;

        if (Input.GetMouseButtonDown(0) && _robot.IsObjectSelected)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                StartMovingTo(hit.point);
            }
        }

        if (isMoving)
        {
            MoveObject();
        }
    }

    private void StartMovingTo(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;

        isMoving = true;
    }

    private void MoveObject()
    {
        var dist = Vector3.Distance(new Vector3(_robot.transform.position.x, 0, _robot.transform.position.z), new Vector3(_targetPosition.x, 0, _targetPosition.z));

        if (dist > 0.1f)
        {
            _robot.Animator.Play("Move");

            _robot.MoveTo(_targetPosition);

            _robot.IsMoving = true;
        }
        else
        {
            _robot.Animator.Play("Idel");

            _robot.IsMoving = false;

            IsFinished = true;
        }
    }
}
