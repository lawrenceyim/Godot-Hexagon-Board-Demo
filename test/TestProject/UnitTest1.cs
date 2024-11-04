namespace TestProject;

[TestClass]
public class UnitTest1 {
    [TestMethod]
    public void StartEqualsEndTest() {
        TileData[,] board = GenerateSquareBoard(10);
        PathFinding pathFinding = new PathFinding(board);
        TileData[] path = pathFinding.FindPath(board[0, 0], board[0, 0]);
        Assert.AreEqual(path.Length, 1, "Path length does not match.");
    }

    [TestMethod]
    public void PathOfLength6Test() {
        TileData[,] board = GenerateSquareBoard(10);
        PathFinding pathFinding = new PathFinding(board);
        TileData[] path = pathFinding.FindPath(board[0, 0], board[1, 5]);
        Assert.AreEqual(path.Length, 6, "Path length does not match.");
    }

    private TileData[,] GenerateSquareBoard(int boardSizeInTiles) {
        TileData[,] board = new TileData[boardSizeInTiles, boardSizeInTiles];
        for (int row = 0; row < boardSizeInTiles; row++) {
            for (int column = 0; column < boardSizeInTiles; column++) {
                board[column, row] = new TileData();
                board[column, row].SetCoordinates((column, row));
            }
        }
        return board;
    }
}