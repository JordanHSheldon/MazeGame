using Godot;
using System;

public partial class Player : RigidBody3D
{
	float mouseSensitivity = 0.001f;

	float twistInput = 0.000f;

	float pitchInput = 0.000f;

	public RigidBody3D _player;

	public Node3D twistPivot;

	public Node3D pitchPivot;

	public float Gravity = (float)ProjectSettings.GetSetting("physics/3d/default_gravity");

	public float Velocity;

	public override void _Ready()
	{
		_player =  GetParent().GetNode<RigidBody3D>("Player");
		twistPivot = _player.GetNode<Node3D>("TwistPivot");
		pitchPivot = twistPivot.GetNode<Node3D>("PitchPivot");	
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector3 input = Vector3.Zero;
		input.X = Input.GetAxis("move_left","move_right");
		input.Z = Input.GetAxis("move_forward","move_backward");

		ApplyCentralForce(twistPivot.Basis * input * (float)1200.0 * (float)delta);

		if (Input.IsActionJustPressed("ui_cancel"))
			Input.MouseMode = Input.MouseModeEnum.Visible;

		if (IsGrounded() && Input.IsActionJustPressed("jump"))
        {
            LinearVelocity = new Vector3(LinearVelocity.X, 10, LinearVelocity.Z);
        }

		twistPivot.RotateY(twistInput);
		pitchPivot.RotateX(pitchInput);
		pitchPivot.RotationDegrees = new Vector3(Mathf.Clamp(pitchPivot.RotationDegrees.X,-90,90), 0, 0);
		twistInput = 0.0f;
		pitchInput = 0.0f;
	}

    public override void _UnhandledInput(InputEvent @event)
    {
		if(@event is InputEventMouseMotion e)
		{
			if(Input.MouseMode == Input.MouseModeEnum.Captured)
			{
				twistInput = - e.Relative.X * mouseSensitivity;
				pitchInput = - e.Relative.Y * mouseSensitivity;
			}
		}
			
    }

	private bool IsGrounded()
	{
		return true;
	}
}
