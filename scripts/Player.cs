using Godot;

namespace VampireSurvivors.scripts;

public partial class Player : CharacterBody2D, IHealthComponent
{
	[Export]
	public int Speed { get; set; } = 400;

	[Export]
	public int Health { get; set; } = 40;
	
	[Export]
	private AnimatedSprite2D animatedSprite;
	
	private PackedScene arrowScene = GD.Load<PackedScene>("res://scenes/arrow.tscn");

	private float lookDirection = 0;
	
	public void GetInput()
	{
		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
		Velocity = inputDirection * Speed;
	}

	public override void _Ready()
	{
		GameState.Player = this;
		animatedSprite.Play("idle");
	}

	public override void _Process(double delta)
	{
		lookDirection = Position.AngleToPoint(GetGlobalMousePosition());
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton)
		{
			if (mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed)
			{
				FireArrow();
			}
		}
	}

	public int ChangeHealth(int value)
	{
		Health = Mathf.Max(Health + value, 0);
		
		GD.Print($"Player health changed to: {Health}");

		if (Health <= 0)
		{
			Die();
		}

		return Health;
	}

	private void Die()
	{
		GetTree().ReloadCurrentScene();
	}

	private void FireArrow()
	{
		var spawnedArrow = arrowScene.Instantiate<Arrow>();
		
		GetTree().CurrentScene.AddChild(spawnedArrow);
		
		spawnedArrow.Fire(Position, lookDirection);
	}
}