using System;
using Godot;

namespace VampireSurvivors.scripts.components;

public partial class AnimationComponent : AnimatedSprite2D
{
	public void PlayAnimation(string animation, Action? onAnimationFinished = null)
	{
		Play(animation);

		if (onAnimationFinished != null)
		{
			AnimationFinished += onAnimationFinished;
		}
	}
}
