using Godot;

public partial class LevelUpScreen : Control
{
	[Export]
	private LevelUpAbility abilityOne;

	[Export]
	private LevelUpAbility abilityTwo;

	[Export]
	private LevelUpAbility abilityThree;

	public override void _Ready()
	{
		abilityOne.SetAbilityName("Ability One");
		abilityOne.SetAbilityDescription("Description one...");

		abilityTwo.SetAbilityName("Ability Two");
		abilityTwo.SetAbilityDescription("Description two...");

		abilityThree.SetAbilityName("Ability Three");
		abilityThree.SetAbilityDescription("Description three...");

		abilityOne.OnClicked += OnAbilityClicked;
		abilityTwo.OnClicked += OnAbilityClicked;
		abilityThree.OnClicked += OnAbilityClicked;
	}

	private void OnAbilityClicked(LevelUpAbility ability)
	{
		GD.Print($"{ability.Name} Clicked");
	}
}
