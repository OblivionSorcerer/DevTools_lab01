using Xunit;
using Wintellect.PowerCollections;
using System;


namespace StackTests
{
    public class TestStack
    {
        //This test checks constructor
        [Fact]
        public void TestConstructor()
        {
            Stack<int> stackCards = new Stack<int>(3);
            Assert.Equal(3, stackCards.capacity);
        }
        //These tests check IsEmpty 
        [Fact]
        public void IsEmptyShouldBeTrueWhenStackIsNew()
        {
            Stack<int> stackCards = new Stack<int>(5);
            Assert.True(stackCards.IsEmpty);
        }
        [Fact]
        public void IsEmptyShouldBeFalseAfterPush()
        {
            var stackCards = new Stack<int>(5);
            stackCards.Push(1);
            Assert.False(stackCards.IsEmpty);
        }
        [Fact]
        public void IsEmptyShouldBeTrueAfterPushThenPop()
        {
            var stackCards = new Stack<int>(6);
            stackCards.Push(5);
            stackCards.Pop();
            Assert.True(stackCards.IsEmpty);
        }
        //These tests check IsFull
        [Fact]
        public void IsFullShouldBeFalseWhenStackIsNew()
        {
            Stack<int> stackCards = new Stack<int>(5);
            Assert.False(stackCards.IsFull);
        }
        [Fact]
        public void PushWhenIsFullShouldBeTrue()
        {
            Stack<int> stackCard = new Stack<int>(2);
            stackCard.Push(2);
            stackCard.Push(2);
            Action act = () => stackCard.Push(2);
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal("Переполнение стека!", exception.Message);
        }
        //This test checks Count
        [Fact]
        public void CountShouldBeZeroWhenStackIsNew()
        {
            Stack<int> stackCard = new Stack<int>(2);
            Assert.Equal(0, stackCard.Count);
        }
        //These tests check Push
        [Fact]
        public void PushAndCountAfterPush()
        {
            Stack<int> stackCard = new Stack<int>(2);
            stackCard.Push(43);
            Assert.Equal(1, stackCard.Count);
        }
        [Fact]
        public void PushWhenStackIsFull()
        {
            Stack<int> stackCard = new Stack<int>(2);
            stackCard.Push(2);
            stackCard.Push(2);
            Action act = () => stackCard.Push(2);
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal("Переполнение стека!", exception.Message);
        }
        //These tests check Top()
        [Fact]
        public void TopShouldBeTheSameAsLastPushedElement()
        {
            Stack<string> stackCard = new Stack<string>(2);
            stackCard.Push("Hello");
            Assert.Equal("Hello", stackCard.Top());
        }
        [Fact]
        public void TopShouldBeThePreviousPushedElementAfterPop()
        {
            Stack<string> stackCard = new Stack<string>(2);
            stackCard.Push("Hello");
            stackCard.Push(" world!");
            stackCard.Pop();
            Assert.Equal("Hello", stackCard.Top());
        }
        [Fact]
        public void TopWhenStackIsEmpty()
        {
            Stack<int> stackCard = new Stack<int>(2);
            Action act = () => stackCard.Top();
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal("Стек пуст!", exception.Message);
        }
        //These tests check Pop()
        [Fact]
        public void PoppedElementShouldBeAsLastPushedElement()
        {
            Stack<int> stackCard = new Stack<int>(2);
            stackCard.Push(2);
            stackCard.Push(32);
            Assert.Equal(32, stackCard.Pop());
            Assert.Equal(2, stackCard.Top());
        }
        [Fact]
        public void PopWhenStackIsEmpty()
        {
            Stack<int> stackCard = new Stack<int>(2);
            Action act = () => stackCard.Pop();
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal("Стек пуст!", exception.Message);
        }
        //Test Foreach
        [Fact]
        public void ForEachShouldBeAbleEnumerateElementsFromTopToBottom()
        {
            int counter = 3;
            Stack<int> stackCard = new Stack<int>(counter);
            int[] testArr = new int[counter];
            int[] trueArr = new int[] { 323,32,2};
            stackCard.Push(2);
            stackCard.Push(32);
            stackCard.Push(323);
            int i = 0;
            foreach (int item in stackCard)
            {
                testArr[i] = item;
                i++;
            }
            Assert.Equal(testArr, trueArr);
        }
    }
}