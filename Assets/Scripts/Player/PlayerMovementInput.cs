using UniRx;
using UnityEngine;

public class PlayerMovementInput : MonoBehaviour
{
	[SerializeField]
	VariableJoystick moveJoystick;

	public Vector2 movementInput { get; private set; }

	[SerializeField]
	VariableJoystick aimJoystick;
	public Vector2 aimInput { get; private set; }

	private void Awake()
	{
		moveJoystick = GameObject
			.FindGameObjectWithTag("MovementJoystick")
			.GetComponent<VariableJoystick>();

		aimJoystick = GameObject
			.FindGameObjectWithTag("AimJoystick")
			.GetComponent<VariableJoystick>();
	}

	// Start is called before the first frame update
	void Start()
	{
		Observable.EveryUpdate().Subscribe(_ => SetDirections()).AddTo(this);
	}

	void SetDirections()
	{
		movementInput = new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical);
		aimInput = new Vector2(aimJoystick.Horizontal, aimJoystick.Vertical);
	}

	public bool IsAiming()
	{
		return aimInput != Vector2.zero;
	}
}
