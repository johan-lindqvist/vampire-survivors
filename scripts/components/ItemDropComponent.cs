using Godot;

namespace VampireSurvivors.scripts.components;

public partial class ItemDropComponent : Node2D
{
	[Export] private PackedScene itemScene;
	[Export] private HealthComponent healthComponent;

	public override void _Ready()
	{
		healthComponent.OnDeath += OnDeath;
	}

	private void OnDeath()
	{
		var item = itemScene.Instantiate<items.Item>();
		GetTree().CurrentScene.CallDeferred(Node.MethodName.AddChild, item);
		item.AddToGroup("items");
		item.Position = GlobalPosition;

		healthComponent.OnDeath -= OnDeath;
	}
}
