using Godot;

namespace VampireSurvivors.scripts;

public partial class Arrow : Area2D
{
	private float damage = 10;

	private float speed = 300f;
	
	private Vector2 velocity = Vector2.Right;

	public override void _Ready()
	{
		Name = "Arrow";
	}

	public override void _Process(double delta)
	{
		Position += velocity * speed * (float)delta;

		if (Mathf.Abs(Position.X) > 1000 || Mathf.Abs(Position.Y) > 500)
		{
			QueueFree();
		}
	}

	public void Fire(Vector2 position, float direction)
	{
		Position = position;
		Rotation = direction;
		velocity = velocity.Rotated(direction);
	}

	public void OnAreaEntered(Area2D area)
	{
		if (area is not Enemy enemy)
		{
			return;
		}
		
		enemy.Die();
		QueueFree();
	}
}