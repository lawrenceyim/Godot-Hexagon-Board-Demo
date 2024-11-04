using System;

public class TileData {
    private (int, int) _coordinates;

    public void SetCoordinates((int, int) coordinates) {
        _coordinates = coordinates;
    }

    public (int, int) GetCoordinates() {
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
