using System;
using Godot;
using VampireSurvivors.scripts.components;

namespace VampireSurvivors.scripts;

public partial class WaveManager : Node2D
{
	private PackedScene enemyScene = GD.Load<PackedScene>("res://scenes/enemies/skeleton_enemy.tscn");
	
	private int currentWaveIndex;

	private int spawnedEnemiesFromWave;

	private int deadEnemiesFromWave;

	private int[] waveAmounts = [10, 20, 30];

	[Export] private Timer timer;

	public override void _Ready()
	{
		timer.Timeout += TimerOnTimeout;

		StartWave(0);
	}

	private void StartWave(int index)
	{
		GD.Print($"Wave {index} started");

		spawnedEnemiesFromWave = 0;
		deadEnemiesFromWave = 0;
		currentWaveIndex = index;

		timer.Start();
	}

	private void EndWave()
	{
		if (currentWaveIndex >= waveAmounts.Length - 1)
		{
			GD.Print("You completed the game!");
			return;
		}

		GD.Print($"Wave {currentWaveIndex} ended");
		StartWave(currentWaveIndex + 1);
	}

	private void TimerOnTimeout()
	{
		SpawnEnemy();

		if (spawnedEnemiesFromWave < waveAmounts[currentWaveIndex])
		{
			timer.Start();
		}
	}

	private void SpawnEnemy()
	{
		var spawnedEnemy = enemyScene.Instantiate<enemies.Enemy>();
		spawnedEnemy.Name = $"Enemy {spawnedEnemiesFromWave}";
		var spawnPosition = GetRandomSpawn();
		spawnedEnemy.Position = spawnPosition;
		spawnedEnemy.OnDeath += OnEnemyDeath;

		GetTree().CurrentScene.AddChild(spawnedEnemy);

		spawnedEnemiesFromWave++;

		return;

		Vector2 GetRandomSpawn()
		{
			float angle = (float)(Random.Shared.NextDouble() * Math.PI * 2);
			float distance = Random.Shared.Next(600, 800);

			return distance * Vector2.FromAngle(angle);
		}
	}

	private void OnEnemyDeath(enemies.Enemy enemy)
	{
		enemy.OnDeath -= OnEnemyDeath;
		deadEnemiesFromWave++;

		if (deadEnemiesFromWave >= waveAmounts[currentWaveIndex])
		{
			EndWave();
		}
	}
}
