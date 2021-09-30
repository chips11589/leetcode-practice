using System;
using System.Collections.Generic;

namespace DesignPatterns.Adapter
{
    public interface IEmployee
    {
        void ShowHappiness();
    }

    public class Employee : IEmployee
    {
        private readonly string _name;

        public Employee(string name)
        {
            this._name = name;
        }

        public void ShowHappiness()
        {
            Console.WriteLine($"{_name} is 10 / 10");
        }
    }

    public class Hero
    {
        public void SolveProblems()
        {
            Console.WriteLine("Solved Problems...");
        }
    }

    public class HeroEmployeeAdapter : Hero, IEmployee
    {
        private readonly string _name;

        public HeroEmployeeAdapter(string name)
        {
            this._name = name;
        }

        public void ShowHappiness()
        {
            base.SolveProblems();
            Console.WriteLine($"{_name} is 100/10");
        }
    }

    public class AdapterPatternCaller : IPatternCaller
    {
        private List<IEmployee> _employees = new List<IEmployee>();

        public void Call()
        {
            _employees.Add(new Employee("Chips 1"));
            _employees.Add(new Employee("Chips 2"));
            _employees.Add(new HeroEmployeeAdapter("Super Chips"));

            foreach (var employee in _employees)
            {
                employee.ShowHappiness();
            }
        }
    }
}
