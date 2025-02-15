using Godot;

namespace VampireSurvivors.scripts.ui;

[GlobalClass]
public partial class UIAnimationHoverNode : UIAnimationNode
{
	protected override void ConnectSignals()
	{
		base.ConnectSignals();
		Target.MouseEntered += OnMouseEntered;
		Target.MouseExited += OnMouseExited;
	}

	private void OnMouseEntered()
	{
		AddTween(AnimationValues);
	}

	private void OnMouseExited()
	{
		AddTween(DefaultValues);
	}
}
