using Godot;
using VampireSurvivors.scripts.weapons;

namespace VampireSurvivors.scripts.enemies;

public partial class EnemyMovementComponent : Node2D
{
	[Export] public float Speed = 100f;

	[Export] private Enemy enemy = null!;

	private bool canMove = true;

	private Timer stunTimer = new()
	{
		OneShot = true
	};

	public override void _Ready()
	{
		if (enemy is null)
		{
			throw new ExportNodeNotBoundException();
		}

		enemy.OnDeath += OnDeath;

		AddChild(stunTimer);
	}

	public override void _Process(double delta)
	{
		if (!canMove)
		{
			return;
		}

		if (!stunTimer.IsStopped())
		{
			return;
		}

		enemy.Position = enemy.Position.MoveToward(player.Player.Instance.Position, (float)delta * Speed);
	}

	public void Stun(IStunAttribute stunAttribute)
	{
		if (!stunTimer.IsStopped())
		{
			return;
		}

		stunTimer.WaitTime = stunAttribute.StunDuration;
		stunTimer.Start();
	}

	private void OnDeath(Enemy e)
	{
		canMove = false;
	}
}
