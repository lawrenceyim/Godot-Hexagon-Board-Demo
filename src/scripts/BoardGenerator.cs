using Godot;

public partial class BoardGenerator : Node2D {
    [Export] private PackedScene _hexagonTilePrefab;
    private TileObject[,] _board;
    private float _sizeOfTile = 100;

    public override void _Ready() {
        _board = GenerateSquareBoard(5);
    }

    // Add a function to create a board from JSON data.
    public TileObject[,] GenerateSquareBoard(int boardSizeInTiles) {
        TileObject[,] board = new TileObject[boardSizeInTiles, boardSizeInTiles];

        float horizontalDistance = _sizeOfTile;
        float verticalDistance = _sizeOfTile * .75f;

        // row is the Y position; column is the X position
        for (int row = 0; row < boardSizeInTiles; row++) {
            float verticalPosition = verticalDistance * row;
            for (int column = 0; column < boardSizeInTiles; column++) {
                Node2D tile = (Node2D)_hexagonTilePrefab.Instantiate();
                board[column, row] = (TileObject)tile;
                board[column, row].GetTileData().SetCoordinates((column, row));
                Vector2 position = new Vector2(horizontalDistance * column, verticalPosition);
                position.X += row % 2 == 0 ? 0 : horizontalDistance / 2;
                tile.Position = position;
                AddChild(tile);
            }
        }

        return board;
    }
}
