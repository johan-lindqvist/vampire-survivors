﻿using Godot;
using GodotUtilities;
using VampireSurvivors.scripts.player;
using VampireSurvivors.scripts.weapons.upgrades;

namespace VampireSurvivors.scripts.weapons;

[Scene]
public partial class Weapon : Node2D
{
	private PackedScene arrowScene = GD.Load<PackedScene>("res://scenes/arrow.tscn");

	[Node]
	private Marker2D shootingPoint = null!;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

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

		foreach (var weaponUpgrade in Player.Instance.WeaponUpgrades)
		{
			if (weaponUpgrade is not IUpgrade upgrade)
			{
				return;
			}

			upgrade.ApplyUpgrade(arrow);
		}

		GetTree().CurrentScene.AddChild(arrow);
	}
}
