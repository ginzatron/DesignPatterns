using System;

namespace DesignPatternsDuckExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Duck duck = new MallardDuck();
            duck.PerformFly();
            duck.PerformQuack();

            Duck duck2 = new SqueakyDuck();
            duck2.PerformFly();
            duck2.PerformQuack();
        }

        public class Duck
        {
            protected IFLyBehavior _fLyBehavior;
            protected IQuackBehavior _quackBehavior;

            public void PerformFly()
            {
                _fLyBehavior.Fly();
            }

            public void PerformQuack()
            {
                _quackBehavior.Quack();
            }
        }

        public class MallardDuck : Duck
        {
            public MallardDuck()
            {
                _fLyBehavior = new FlyWithWings();
                _quackBehavior = new QuackOutLoud();
            }
        }

        public class SqueakyDuck : Duck
        {
            public SqueakyDuck()
            {
                _fLyBehavior = new FlyNoWay();
                _quackBehavior = new Squeak();
            }
        }

        public interface IFLyBehavior
        {
            void Fly();
        }

        public interface IQuackBehavior
        {
            void Quack();
        }

        public class FlyWithWings : IFLyBehavior
        {
            public void Fly()
            {
                Console.WriteLine("I am flapping my wings");
            }
        }

        public class FlyNoWay : IFLyBehavior
        {
            public void Fly()
            {
                Console.WriteLine("I do not fly uh uh no way");
            }
        }

        public class QuackOutLoud : IQuackBehavior
        {
            public void Quack()
            {
                Console.WriteLine("quacking away over here");
            }
        }

        public class Squeak : IQuackBehavior
        {
            public void Quack()
            {
                Console.WriteLine("Squeak squeak");
            }
        }
    }
}
