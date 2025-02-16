using System;
using Godot;
using Godot.Collections;

namespace VampireSurvivors.scripts.ui;

[GlobalClass]
public partial class UIAnimationNode : Node
{
	private Action? onAnimationFinished;

	private bool enterAnimationFinished = false;

	private Dictionary<string, Variant> defaultValues = new();

	private Dictionary<string, Variant> enterValues = new();

	private Dictionary<string, Variant> hoverValues = new();

	private Control? target;

	// Options

	[Export]
	private bool runEnterAnimation = false;

	[Export]
	private bool runHoverAnimation = false;

	[Export]
	private UIAnimationNode? waitFor;

	// Enter Options

	[ExportGroup("Enter Animation Options")]
	[Export]
	private bool enterRunInParallel = true;

	[Export]
	private float enterDelay = 0f;

	[Export]
	private float enterDuration = 0f;

	[Export]
	private float enterScale = 1f;

	[Export]
	private Vector2 enterPosition = new Vector2(0, 0);

	[Export]
	private float enterRotation = 0f;

	[Export]
	private Tween.TransitionType enterTransitionType = Tween.TransitionType.Linear;

	[Export]
	private Tween.EaseType enterEaseType = Tween.EaseType.Out;

	[Export]
	private Array<string> enterProperties = ["scale", "position", "rotation"];

	// Hover Options

	[ExportGroup("Hover Animation Options")]
	[Export]
	private bool hoverRunInParallel = true;

	[Export]
	private float hoverDelay = 0f;

	[Export]
	private float hoverDuration = 0f;

	[Export]
	private float hoverScale = 1f;

	[Export]
	private Vector2 hoverPosition = new Vector2(0, 0);

	[Export]
	private float hoverRotation = 0f;

	[Export]
	private Tween.TransitionType hoverTransitionType = Tween.TransitionType.Linear;

	[Export]
	private Tween.EaseType hoverEaseType = Tween.EaseType.Out;

	[Export]
	private Array<string> hoverProperties = ["scale", "position", "rotation"];

	public override void _Ready()
	{
		target = GetParent<Control>();

		ConnectSignals();
		CallDeferred("Setup");
		CallDeferred("Enter");
	}

	private void Setup()
	{
		if (target == null)
			return;

		target.PivotOffset = target.Size / 2; // Makes the animations run from the center of the node

		UpdateDictionaryProperty(defaultValues, "scale", target.Scale);
		UpdateDictionaryProperty(defaultValues, "position", target.Position);
		UpdateDictionaryProperty(defaultValues, "rotation", target.Rotation);

		UpdateDictionaryProperty(enterValues, "scale", target.Scale * +enterScale);
		UpdateDictionaryProperty(enterValues, "position", target.Position + enterPosition);
		UpdateDictionaryProperty(enterValues, "rotation", target.Rotation + float.DegreesToRadians(enterRotation));

		UpdateDictionaryProperty(hoverValues, "scale", target.Scale * +hoverScale);
		UpdateDictionaryProperty(hoverValues, "position", target.Position + hoverPosition);
		UpdateDictionaryProperty(hoverValues, "rotation", target.Rotation + float.DegreesToRadians(hoverRotation));
	}

	private void ConnectSignals()
	{
		if (target == null)
			return;

		target.Resized += Setup;
		target.MouseEntered += OnMouseEntered;
		target.MouseExited += OnMouseExited;

		if (waitFor != null)
		{
			waitFor.onAnimationFinished += Enter;
		}
	}

	private void Enter()
	{
		if (!runEnterAnimation)
			return;

		AddTween(enterValues, 0, 0, true, Tween.TransitionType.Linear, Tween.EaseType.In);

		if (waitFor is { enterAnimationFinished: false })
			return;

		AddTween(
			defaultValues,
			enterDelay,
			enterDuration,
			enterRunInParallel,
			enterTransitionType,
			enterEaseType,
			() =>
			{
				enterAnimationFinished = true;
				onAnimationFinished?.Invoke();
			}
		);
	}

	private void OnMouseEntered()
	{
		if (!runHoverAnimation)
			return;

		AddTween(hoverValues, hoverDelay, hoverDuration, hoverRunInParallel, hoverTransitionType, hoverEaseType);
	}

	private void OnMouseExited() => AddTween(defaultValues, hoverDelay, hoverDuration, hoverRunInParallel, hoverTransitionType, hoverEaseType);

	private static void UpdateDictionaryProperty(Dictionary<string, Variant> dictionary, string property, Variant value)
	{
		if (dictionary.ContainsKey(property))
		{
			dictionary[property] = value;
		}
		else
		{
			dictionary.Add(property, value);
		}
	}

	private void AddTween(
		Dictionary<string, Variant> values,
		float delay,
		float duration,
		bool parallel,
		Tween.TransitionType transitionType,
		Tween.EaseType easeType,
		Action? onFinished = null
	)
	{
		void RunTween()
		{
			var tween = GetTree().CreateTween();

			tween.SetParallel(parallel);
			tween.SetTrans(transitionType);
			tween.SetEase(easeType);

			if (onFinished != null)
			{
				tween.Finished += onFinished;
			}

			foreach (var property in values.Keys)
			{
				tween.TweenProperty(target, property, values[property], duration);
			}
		}

		if (delay > 0)
		{
			var timer = GetTree().CreateTimer(delay);
			timer.Timeout += RunTween;
		}
		else
		{
			RunTween();
		}
	}
}
