using System;
using Godot;

public partial class Tile : Node2D {
    [Export] private Sprite2D _sprite;
    private Vector2I _coordinates;

    public void SetCoordinates(Vector2I coordinates) {
        _coordinates = coordinates;
    }

    public Vector2I GetCoordinates() {
        return _coordinates;
    }

    // Change color to highlight
    private void _on_area_2d_mouse_entered() {
        _sprite.Modulate = new Color(0, 0, 0);
        GD.Print(_coordinates);
    }

    // Change color to original
    private void _on_area_2d_mouse_exited() {
        _sprite.Modulate = new Color(1, 1, 1);
    }

    public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }
        Tile other = (Tile)obj;
        return _coordinates.Equals(other.GetCoordinates());
    }

    public override int GetHashCode() {
        return HashCode.Combine(_coordinates);
    }
}
