using Godot;

namespace VampireSurvivors.scripts;

public interface IHealthComponent
{
	[Export]
	public int Health { get; set; }

	public int ChangeHealth(int delta);
}
