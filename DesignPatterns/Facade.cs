using System;

namespace DesignPatterns.Facade
{
    public class HeroFacade
    {
        private Hero1 _hero1 = new Hero1();
        private Hero2 _hero2 = new Hero2();
        private Hero3 _hero3 = new Hero3();

        public void SuperCombinedTask()
        {
            _hero1.SuperTask1();
            _hero2.SuperTask1();

            _hero3.SuperTask1();
            _hero3.SuperTask2();
        }
    }

    public class Hero3
    {
        public void SuperTask1()
        {
            Console.WriteLine($"{nameof(Hero3)}_SuperTask1");
        }

        public void SuperTask2()
        {
            Console.WriteLine($"{nameof(Hero3)}_SuperTask2");
        }
    }

    public class Hero2
    {
        public void SuperTask1()
        {
            Console.WriteLine($"{nameof(Hero2)}_SuperTask2");
        }
    }

    public class Hero1
    {
        public void SuperTask1()
        {
            Console.WriteLine($"{nameof(Hero1)}_SuperTask2");
        }
    }

    public class FacadePatternCaller : IPatternCaller
    {
        public void Call()
        {
            var heroFacade = new HeroFacade();
            heroFacade.SuperCombinedTask();
        }
    }
}
