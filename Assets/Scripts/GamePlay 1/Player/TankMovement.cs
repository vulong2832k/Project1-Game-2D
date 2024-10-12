using System.Collections;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public TankStats tankStats;

    private Vector3 _movementDirection;
    [SerializeField] private float _currentMoveSpeed;
    private Coroutine _speedBoostCoroutine;

    private void Start()
    {
        
    }

    void Update()
    {
        UpdateMoveSpeed();
        MoveTank();
    }

    // Di chuyển
    private void MoveTank()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        _movementDirection = new Vector3(horizontalInput, verticalInput, 0);

        if (_movementDirection.magnitude > 0)
        {
            transform.Translate(_movementDirection * _currentMoveSpeed * Time.deltaTime, Space.World);

            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, _movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, tankStats.rotationSpeed * Time.deltaTime);
        }
    }

    // Tăng tốc tạm thời
    public void ApplyTemporarySpeedBoost(float speedIncrease, float duration)
    {
        if (_speedBoostCoroutine != null)
        {
            StopCoroutine(_speedBoostCoroutine); // Dừng hiệu ứng tăng tốc trước đó nếu có
        }

        _speedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine(speedIncrease, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float speedIncrease, float duration)
    {
        _currentMoveSpeed = tankStats.moveSpeed + speedIncrease; // Tăng tốc độ di chuyển tạm thời

        yield return new WaitForSeconds(duration);

        _currentMoveSpeed = tankStats.moveSpeed; // Reset lại tốc độ sau khi kết thúc thời gian tăng tốc
        _speedBoostCoroutine = null;
    }
    public void UpdateMoveSpeed()
    {
        _currentMoveSpeed = tankStats.moveSpeed;
    }
}