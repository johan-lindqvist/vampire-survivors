using Godot;

namespace VampireSurvivors.scripts;
public partial class Item : Area2D
{
	private void OnBodyEntered(Node2D node)
	{
		if (node is not Player _)
		{
			return;
		}
		
		QueueFree();
	}
}
