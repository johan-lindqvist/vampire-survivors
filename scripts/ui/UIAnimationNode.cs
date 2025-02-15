using Godot;
using Godot.Collections;

namespace VampireSurvivors.scripts.ui;

public abstract partial class UIAnimationNode : Node
{
	[Export]
	protected float Duration = 0.2f;

	[Export]
	protected float Delay = 0.0f;

	[Export]
	protected bool RunInParallel = true;

	[Export]
	protected Vector2 Size;

	[Export]
	protected Vector2 Scale = new Vector2(1, 1);

	[Export]
	protected Vector2 Position;

	[Export]
	protected float Rotation = 0f;

	[Export]
	protected Color Modulate = Colors.White;

	[Export]
	protected Tween.TransitionType TransitionType = Tween.TransitionType.Linear;

	[Export]
	protected Tween.EaseType EaseType = Tween.EaseType.Out;

	private Array<string> properties = new() { "size", "scale", "position", "rotation", "self_modulate" };

	protected Control Target;

	protected Dictionary<string, Variant> DefaultValues = new();

	protected Dictionary<string, Variant> AnimationValues = new();

	public override void _Ready()
	{
		Target = GetParent<Control>();

		ConnectSignals();
	}

	protected virtual void Setup()
	{
		Target.PivotOffset = Target.Size / 2; // Makes the animations run from the center of the node

		DefaultValues.Add("size", Target.Size);
		DefaultValues.Add("scale", Target.Scale);
		DefaultValues.Add("position", Target.Position);
		DefaultValues.Add("rotation", Target.Rotation);
		DefaultValues.Add("self_modulate", Target.Modulate);

		AnimationValues.Add("size", Target.Size + Size);
		AnimationValues.Add("scale", Target.Scale *= Scale);
		AnimationValues.Add("position", Target.Position + Position);
		AnimationValues.Add("rotation", Target.Rotation + float.DegreesToRadians(Rotation));
		AnimationValues.Add("self_modulate", Modulate);
	}

	protected virtual void ConnectSignals()
	{
		Target.Resized += Setup;
	}

	protected void AddTween(Dictionary<string, Variant> endValues, bool instant = false)
	{
		var tween = GetTree().CreateTween();
		var duration = instant ? 0 : Duration;
		var parallel = instant || RunInParallel;
		var transitionType = instant ? Tween.TransitionType.Linear : TransitionType;

		tween.SetParallel(parallel);
		tween.SetTrans(transitionType);
		tween.SetEase(EaseType);

		foreach (var property in properties)
		{
			tween.TweenProperty(Target, property, endValues[property], duration);
		}
	}
}
