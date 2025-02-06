using Godot;
using System;

namespace VampireSurvivors.scripts.components;

public partial class HitboxComponent : Area2D
{
	public Action<Node, Action?>? OnHit;

	public void Hit(Node hittingNode, Action? onHitCallback = null)
	{
		OnHit?.Invoke(hittingNode, onHitCallback);
	}
}
