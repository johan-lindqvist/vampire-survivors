using Godot;

namespace VampireSurvivors.scripts;
public partial class Item : Area2D
{
	private int experience = 10;
	
	private void OnBodyEntered(Node2D node)
	{
		if (node is not Player player)
		{
			return;
		}
		
		PickedUp(player);
	}

	private void PickedUp(Player player)
	{
		player.AddExperience(experience);
		QueueFree();
	}
}
