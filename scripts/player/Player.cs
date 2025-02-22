using System.Collections.Immutable;
using Godot;
using Godot.Collections;
using GodotUtilities;
using VampireSurvivors.scripts.ui;
using VampireSurvivors.scripts.weapons.upgrades;

namespace VampireSurvivors.scripts.player;

[Scene]
public partial class Player : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 400;

	[Export]
	public int Health { get; set; } = 100;

	public int Experience { get; private set; } = 0;

	public int Level { get; private set; } = 1;

	[Node]
	private HealthBar healthBar = null!;

	[Node]
	private AnimatedSprite2D characterSprite = null!;

	public ImmutableList<BaseUpgrade> WeaponUpgrades { get; set; } = ImmutableList<BaseUpgrade>.Empty;

	public static Player Instance { get; private set; } = null!;

	[Export]
	public Array<BaseUpgrade> AvailableUpgrades { get; set; } = [];

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
		Instance = this;
		characterSprite.Play("idle");
		UI.Instance.SetLevelLabel(Level);
		UI.Instance.SetExperienceBarMax(Level * 100);
		UI.Instance.OnUpgradeSelected += OnUpgradeSelected;
		healthBar.SetMaxHealth(Health);
	}

	public override void _Process(double delta) { }

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
			OnLevelUp();
		}
	}

	private void OnLevelUp()
	{
		Level++;
		Experience = 0;
		UI.Instance.SetLevelLabel(Level);
		UI.Instance.SetExperienceBar(0);
		UI.Instance.SetExperienceBarMax(Level * 100);
		UI.Instance.ShowLevelUpScreen(AvailableUpgrades);
	}

	private void OnUpgradeSelected(BaseUpgrade upgrade)
	{
		WeaponUpgrades.Add(upgrade);
	}

	private void Die()
	{
		GetTree().ReloadCurrentScene();
	}
}
