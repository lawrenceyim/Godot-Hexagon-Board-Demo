using Godot;

public partial class Tile : Node2D {
	[Export] private Sprite2D _sprite;
	
	public override void _Ready() {
	}

	// Change color to highlight
	private void _on_area_2d_mouse_entered() {
		_sprite.Modulate = new Color(0, 0, 0);
	}

	// Change color to original
	private void _on_area_2d_mouse_exited() {
		_sprite.Modulate = new Color(1, 1, 1);
	}
}
