using Godot;
using System;

public partial class Interact : RayCast3D
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	[Export] RayCast3D interact;

	public override void _Ready()
	{
		interact = GetParent().GetChild<RayCast3D>(0);

        if (interact == null)
        {
            GD.PrintErr("RayCast3D node not found. Ensure the path is correct.");
        }
	}
	public override void _Process(double delta)
	{
		if (interact.IsColliding())
		{
			if (interact == null) GD.Print("processing");
			var hitobj = interact.GetCollider();
			if(hitobj.HasMethod("Interact") && Input.IsActionJustPressed("Interact"))
			{
				hitobj.Call("Interact");
			}
		}
	}
}
