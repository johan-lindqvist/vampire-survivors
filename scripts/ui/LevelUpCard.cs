using System;
using Godot;
using VampireSurvivors.scripts.weapons.upgrades;

namespace VampireSurvivors.scripts.ui;

public partial class LevelUpCard : Control
{
	public Action<LevelUpCard, BaseUpgrade>? OnClicked;

	[Export]
	private Label title;

	[Export]
	private Label description;

	private BaseUpgrade upgrade;

	public override void _Ready()
	{
		GuiInput += OnGuiInput;
	}

	public void SetUpgrade(BaseUpgrade u)
	{
		upgrade = u;
		title.Text = upgrade.Name;
		description.Text = upgrade.Description;
	}

	private void OnGuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton { Pressed: true })
		{
			OnClicked?.Invoke(this, upgrade);
		}
	}
}
