using Godot;

namespace VampireSurvivors.scripts;

public partial class Weapon : Node2D
{
	private PackedScene arrowScene = GD.Load<PackedScene>("res://scenes/arrow.tscn");

	[Export]
	private Marker2D shootingPoint;
	
	public override void _PhysicsProcess(double delta)
	{
		LookAt(GetGlobalMousePosition());
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton)
		{
			if (mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed)
			{
				Shoot();
			}
		}
	}

	public void Shoot()
	{
		var arrow = arrowScene.Instantiate<Arrow>();
		arrow.Position = shootingPoint.GlobalPosition;
		arrow.Rotation = shootingPoint.GlobalRotation;
		GetTree().CurrentScene.AddChild(arrow);
	}
}
