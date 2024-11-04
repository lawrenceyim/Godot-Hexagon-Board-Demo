namespace TestProject;

[TestClass]
public class UnitTest1 {
    [TestMethod]
    public void TestMethod1() {
        TileData tile = new TileData();
        tile.SetCoordinates((0, 0));
    }
}