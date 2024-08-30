using Godot;
using System;
using System.Threading.Tasks;

public partial class Door : StaticBody3D
{
	// false = closed door
	bool toggle = false;

	// disallows users to interact with the door.
	bool IsInteractable = true;

	[Export] AnimationPlayer animationPlayer;

	public override void _Ready()
	{
		// animationPlayer = GetParent().GetChild<AnimationPlayer>(3);
		//.
	}

	public async void Interact()
	{
		GD.Print("interact started");
		if(IsInteractable)
		{
			IsInteractable = false;
			toggle = !toggle;
			if(!toggle) animationPlayer.Play("animation_close");
			if(toggle) animationPlayer.Play("animation_open");
			await Task.Delay(TimeSpan.FromMilliseconds(1000));
			IsInteractable = true;
		}
	}
}
