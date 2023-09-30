using NUnit.Framework;

namespace GameOfLife;

[TestFixture]
public class GameTests
{
    [Test]
    public void SomeTest1()
    {
        // 1.Initalization
        var field = new bool[5, 5];
        Random idexFirst = new Random();
        Random indexSecond = new Random();
        int valueFirst = idexFirst.Next(3);
        int valueSecond = indexSecond.Next(3);

        field[valueFirst, valueSecond] = true;
        field[valueFirst + 1, valueSecond] = false;
        field[valueFirst, valueSecond+1] = false;
        
        var result = Game.NextStep(field);
        
        Assert.AreEqual(false, result[valueFirst, valueSecond]);
        Assert.AreEqual(false, result[valueFirst+1, valueSecond]);
        Assert.AreEqual(false, result[valueFirst, valueSecond+1]);


    }
    
    [Test]
    public void SomeTest()
    {
        // 1.Initalization
        var field = new bool[5, 5];
        field[0, 0] = false;
        field[0, 1] = true;
        field[1, 0] = true;
        field[1, 1] = true;


        // 2.Action
        var result = Game.NextStep(field);


        // 3.Validate
        // Assert.That(field[0, 0], Is.EqualTo(true));
        
        
        Assert.AreEqual(true, result[0, 0]);
        Assert.AreEqual(true, result[0, 1]);
        Assert.AreEqual(true, result[1, 0]);
        Assert.AreEqual(true, result[1, 1]);
        
        



    }
}