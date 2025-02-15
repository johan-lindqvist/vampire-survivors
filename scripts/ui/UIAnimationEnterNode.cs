using Godot;

namespace VampireSurvivors.scripts.ui;

[GlobalClass]
public partial class UIAnimationEnterNode : UIAnimationNode
{
	protected override void Setup()
	{
		base.Setup();
		AddTween(AnimationValues, true);
		AddTween(DefaultValues);
	}
}
