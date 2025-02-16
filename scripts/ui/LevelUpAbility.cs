using System;
using Godot;

public partial class LevelUpAbility : Control
{
	public Action<LevelUpAbility>? OnClicked;

	[Export]
	private Label abilityName;

	[Export]
	private Label abilityDescription;

	public override void _Ready()
	{
		GuiInput += OnGuiInput;
	}

	public void SetAbilityName(string name)
	{
		abilityName.Text = name;
	}

	public void SetAbilityDescription(string description)
	{
		abilityDescription.Text = description;
	}

	private void OnGuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton { Pressed: true })
		{
			OnClicked?.Invoke(this);
		}
	}
}
