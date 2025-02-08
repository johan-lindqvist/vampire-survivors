using Godot;
using VampireSurvivors.scripts.items;

namespace VampireSurvivors.scripts.components;

public partial class ItemDropComponent : Node2D
{
	[Export]
	private PackedScene itemScene = null!;

	public void DropItem()
	{
		var item = itemScene.Instantiate<Item>();
		GetTree().CurrentScene.CallDeferred(Node.MethodName.AddChild, item);
		item.AddToGroup("items");
		item.Position = GlobalPosition;
	}
}
