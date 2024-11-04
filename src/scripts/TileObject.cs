/*
    TileObject could technically contain the Vector2I coordinates and remove the TileData abstraction,
    but this was done intentionally to make it easier to unit test the pathfinding.
    This allows us to create unit tests without being forced to create Godot nodes.
*/
using Godot;

public partial class TileObject : Node2D {
	[Export] private Sprite2D _sprite;
    private TileData _tileData = new TileData();

    public void SetTileData(Vector2I coordinates) {
        _tileData.SetCoordinates(coordinates);
    }

    public TileData GetTileData() {
        return _tileData;
    }

	// Change color to highlight
	private void _on_area_2d_mouse_entered() {
		_sprite.Modulate = new Color(0, 0, 0);
		GD.Print(_tileData.GetCoordinates());
	}

	// Change color to original
	private void _on_area_2d_mouse_exited() {
		_sprite.Modulate = new Color(1, 1, 1);
	}
}