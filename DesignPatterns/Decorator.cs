using System;

namespace DesignPatterns.Decorator
{
    public interface ISuperHero
    {
        void Move();
        void Fight();
    }

    public class SuperHero : ISuperHero
    {
        public void Fight()
        {
            Console.WriteLine($"{nameof(Fight)} with bare hands");
        }

        public void Move()
        {
            Console.WriteLine($"{nameof(Move)} by feet");
        }
    }

    public class FlyingHeroDecorator : ISuperHero
    {
        private ISuperHero _wrappee;

        public FlyingHeroDecorator(ISuperHero wrappee)
        {
            _wrappee = wrappee;
        }

        public void Fight()
        {
            _wrappee.Fight();
            Console.Write(" in the air");
        }

        public void Move()
        {
            _wrappee.Move();
            Console.Write(" in the air");
        }
    }

    public class DecoratorPatternCaller : IPatternCaller
    {
        public void Call()
        {
            ISuperHero superHero = new FlyingHeroDecorator(new SuperHero());

            superHero.Move();
            superHero.Fight();
        }
    }
}
