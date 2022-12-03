using System.Collections.Generic;
using System;
using System.Collections;

namespace Wintellect.PowerCollections
{
    public class Stack<T> : IEnumerable<T>
    {
        private T[] stack;
        private int position = -1;
        public readonly int capacity = 0;
        public Stack(int capacityOfStack)
        {
            capacity = capacityOfStack;
            stack = new T[capacity];
        }
        public bool IsEmpty
        {
            get { return position < 0; }
        }
        public int Count
        {
            get { return position+1; }
        }
        public bool IsFull
        {
            get { return Count == capacity; }
        }
        public void Push(T element)
        {
            if(IsFull)
                throw new InvalidOperationException("Переполнение стека!");
            stack[++position] = element;
        }
        public T Pop()
        {
            if(IsEmpty)
                throw new InvalidOperationException("Стек пуст!");
            T currentElement = stack[position];
            stack[position] = default(T)!;
            position--;
            return currentElement;
        }
        public T Top()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Стек пуст!");
            return stack[position];
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)GetEnumerator();
        }
        public IEnumerator GetEnumerator()
        {
            return new StackEnum<T>(stack, Count);
        }
    }
    public class StackEnum<T> : IEnumerator<T>
    {
        private T[] arr;
        private int position;
        private int capacity;
        public StackEnum(T[] arr, int position)
        {
            this.arr = arr;
            this.position = position;
            capacity = position;
        }
        public T Current { get; private set; }

        object IEnumerator.Current => this.arr[position]!;

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            position--;
            return position > -1;
        }

        public void Reset()
        {
            position = capacity;
        }
    }
}
