using Godot;
using GodotUtilities;
using VampireSurvivors.scripts.player;

namespace VampireSurvivors.scripts.enemies;

[Scene]
public partial class EnemyAttackComponent : Area2D
{
	[Export]
	private float attackDamage = 10f;

	[Node]
	private Timer timer = null!;

	private Player? collidingPlayer;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Process(double delta)
	{
		AttackPlayer();
	}

	private void OnBodyEntered(Node2D node)
	{
		GD.Print(node.Name);

		if (node is not Player player)
		{
			return;
		}

		collidingPlayer = player;
	}

	private void OnBodyExited(Node2D node)
	{
		if (node is not Player)
		{
			return;
		}

		collidingPlayer = null;
	}

	private void AttackPlayer()
	{
		if (collidingPlayer == null || timer.TimeLeft > 0)
		{
			return;
		}

		if (timer.IsStopped())
		{
			timer.OneShot = true;
			timer.Start();
		}

		collidingPlayer.ChangeHealth((int)-attackDamage);
	}
}
