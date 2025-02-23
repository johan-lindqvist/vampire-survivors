using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;
using VampireSurvivors.scripts.weapons.upgrades;

namespace VampireSurvivors.scripts.ui;

public partial class LevelUpScreen : Control
{
	[Export]
	private LevelUpCard cardOne;

	[Export]
	private LevelUpCard cardTwo;

	[Export]
	private LevelUpCard cardThree;

	public Action<BaseUpgrade>? OnUpgradeSelected;

	public override void _Ready()
	{
		GetTree().Paused = true;
	}

	public void SetUpgradeCards(Array<BaseUpgrade> upgrades)
	{
		var upgradeOne = upgrades[0];
		var upgradeTwo = upgrades[1];
		var upgradeThree = upgrades[2];

		cardOne.SetUpgrade(upgradeOne);
		cardOne.OnClicked += OnCardClicked;

		cardTwo.SetUpgrade(upgradeTwo);
		cardTwo.OnClicked += OnCardClicked;

		cardThree.SetUpgrade(upgradeThree);
		cardThree.OnClicked += OnCardClicked;
	}

	private void OnCardClicked(LevelUpCard card, BaseUpgrade upgrade)
	{
		OnUpgradeSelected?.Invoke(upgrade);
		GetTree().Paused = false;
		QueueFree();
	}
}
