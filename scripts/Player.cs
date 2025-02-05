using Godot;

namespace VampireSurvivors.scripts;

public partial class Player : CharacterBody2D, IHealthComponent
{
	[Export]
	public int Speed { get; set; } = 400;

	[Export]
	public int Health { get; set; } = 100;
	
	public int Experience { get; private set; } = 0;
	
	public int Level { get; private set; } = 1;

	[Export] private HealthBar healthBar;
	
	[Export]
	private AnimatedSprite2D animatedSprite;

	[Export]
	private AnimatedSprite2D bowSprite;

	private float bowSpriteOffset = 20f;
	
	private PackedScene arrowScene = GD.Load<PackedScene>("res://scenes/arrow.tscn");

	public static Player Instance { get; private set; }

	public override void _Ready()
	{
		Instance = this;
		animatedSprite.Play("idle");
		UI.Instance.SetLevelLabel(Level);
		UI.Instance.SetExperienceBarMax(Level * 100);
		healthBar.SetMaxHealth(Health);
	}

	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
	}

	public void GetInput()
	{
		var inputDirection = Input.GetVector("left", "right", "up", "down");
		Velocity = inputDirection * Speed;
	}

	public int ChangeHealth(int value)
	{
		Health = Mathf.Max(Health + value, 0);
		
		healthBar.SetHealth(Health);

		if (Health <= 0)
		{
			Die();
		}

		return Health;
	}

	public void AddExperience(int value)
	{
		Experience += value;
		UI.Instance.SetExperienceBar(Experience);
		
		if (Experience >= Level * 100)
		{
			Level++;
			Experience = 0;
			UI.Instance.SetLevelLabel(Level);
			UI.Instance.SetExperienceBar(0);
			UI.Instance.SetExperienceBarMax(Level * 100);
		}
	}

	private void Die()
	{
		GetTree().ReloadCurrentScene();
	}
}
