using System;
using Godot;

public class TileData {
	private Vector2I _coordinates;

	public void SetCoordinates(Vector2I coordinates) {
		_coordinates = coordinates;
	}

	public Vector2I GetCoordinates() {
		return _coordinates;
	}

	public override bool Equals(object obj) {
		if (obj == null || GetType() != obj.GetType()) {
			return false;
		}
		TileData other = (TileData)obj;
		return _coordinates.Equals(other.GetCoordinates());
	}

	public override int GetHashCode() {
		return HashCode.Combine(_coordinates);
	}
}
