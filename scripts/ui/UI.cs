using Godot;
using GodotUtilities;

namespace VampireSurvivors.scripts.ui;

[Scene]
public partial class UI : CanvasLayer
{
	[Node]
	private ProgressBar experienceBar = null!;

	[Node]
	private Label levelValue = null!;

	[Export]
	private Control container;

	private LevelUpScreen levelUpScreen;

	private PackedScene levelUpScreenScene = GD.Load<PackedScene>("res://scenes/ui/level_up_screen.tscn");

	public static UI Instance { get; private set; } = null!;

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
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("quit"))
		{
			GetTree().Quit();
		}
	}

	public void SetExperienceBar(int currentValue)
	{
		experienceBar.Value = currentValue;
	}

	public void SetExperienceBarMax(int maxValue)
	{
		experienceBar.MaxValue = maxValue;
	}

	public void SetLevelLabel(int value)
	{
		levelValue.Text = value.ToString();
	}

	public void ShowLevelUpScreen()
	{
		levelUpScreen = levelUpScreenScene.Instantiate<LevelUpScreen>();
		container.AddChild(levelUpScreen);
	}
}
