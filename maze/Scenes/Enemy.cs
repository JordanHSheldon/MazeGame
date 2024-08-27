using Godot;
using System;

public partial class Enemy : Node2D
{
	[Export] public float speed = 60f;

	float direction = 1;

	private RayCast2D detectLeft;

	private RayCast2D detectRight;

	public AnimatedSprite2D sprite;

    public override void _Ready()
    {
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");;
        detectLeft = GetNode<RayCast2D>("DetectRight");
        detectRight = GetNode<RayCast2D>("DetectLeft");
    }

    public override void _Process(double delta)
	{
		Console.WriteLine(delta.ToString(),detectLeft.IsColliding());
		if(detectRight.IsColliding())
		{
			direction = -1;
			sprite.FlipH = false;
		}
			
		if(detectLeft.IsColliding())
		{
			direction = 1;
			sprite.FlipH = true;
		}
		
		Position += new Vector2(-direction * speed * (float)delta, 0);
	}
}
