using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pp_AbstactFactory
{
    // Интерфейс Абстрактной Фабрики объявляет набор методов, которые
    // возвращают различные абстрактные продукты.  Эти продукты называются
    // семейством и связаны темой или концепцией высокого уровня. Продукты
    // одного семейства обычно могут взаимодействовать между собой. Семейство
    // продуктов может иметь несколько вариаций,  но продукты одной вариации
    // несовместимы с продуктами другой.
    public interface ICarFactory
    {
        IProcessor CreateProcessor();

        IMainboard CreateMainboard();
    }

    // Конкретная Фабрика производит семейство продуктов одной вариации.
    // Фабрика гарантирует совместимость полученных продуктов.  Обратите
    // внимание, что сигнатуры методов Конкретной Фабрики возвращают абстрактный
    // продукт, в то время как внутри метода создается экземпляр  конкретного
    // продукта.
    class Dell : ICarFactory
    {
        public IProcessor CreateProcessor()
        {
            return new DellProcessor();
        }

        public IMainboard CreateMainboard()
        {
            return new DellEngine();
        }
    }

    // Каждая Конкретная Фабрика имеет соответствующую вариацию продукта.
    class Sony : ICarFactory
    {
        public IProcessor CreateProcessor()
        {
            return new SonyProcessor();
        }

        public IMainboard CreateMainboard()
        {
            return new SonyEngine();
        }
    }

    // Каждый отдельный продукт семейства продуктов должен иметь базовый
    // интерфейс. Все вариации продукта должны реализовывать этот интерфейс.
    public interface IProcessor
    {
        string ShowMessage();
    }

    // Конкретные продукты создаются соответствующими Конкретными Фабриками.
    class DellProcessor : IProcessor
    {
        public string ShowMessage()
        {
            return "Процессор Dell";
        }
    }

    class SonyProcessor : IProcessor
    {
        public string ShowMessage()
        {
            return "Процессор Sony";
        }
    }

    // Базовый интерфейс другого продукта. Все продукты могут
    // взаимодействовать друг с другом, но правильное взаимодействие возможно
    // только между продуктами одной и той же конкретной вариации.
    public interface IMainboard
    {
        // Продукт B способен работать самостоятельно...
        string ShowBatteryVolume();

        // ...а также взаимодействовать с Продуктами Б той же вариации.
        //
        // Абстрактная Фабрика гарантирует, что все продукты, которые она
        // создает, имеют одинаковую вариацию и, следовательно, совместимы.
        string ShowProcessor(IProcessor collaborator);
    }

    // Конкретные Продукты создаются соответствующими Конкретными Фабриками.
    class DellEngine : IMainboard
    {
        public string ShowBatteryVolume()
        {
            return "Материнская плата Dell";
        }

        // Продукт B1 может корректно работать только с Продуктом A1. Тем не
        // менее, он принимает любой экземпляр Абстрактного Продукта А в
        // качестве аргумента.
        public string ShowProcessor(IProcessor processor)
        {
            var result = processor.ShowMessage();

            return $"({result}): процессор";
        }
    }

    class SonyEngine : IMainboard
    {
        public string ShowBatteryVolume()
        {
            return "Материнская плата Sony";
        }

        // Продукт B2 может корректно работать только с Продуктом A2. Тем не
        // менее, он принимает любой экземпляр Абстрактного Продукта А в качестве
        // аргумента.
        public string ShowProcessor(IProcessor processor)
        {
            var result = processor.ShowMessage();

            return $"({result}): процессор";
        }
    }

    // Клиентский код работает с фабриками и продуктами только через
    // абстрактные типы: Абстрактная Фабрика и Абстрактный Продукт. Это
    // позволяет передавать любой подкласс фабрики или продукта клиентскому
    // коду, не нарушая его.
    class Client
    {
        public void Main()
        {
            // Клиентский код может работать с любым конкретным классом
            // фабрики.
            Console.WriteLine("Клиент: тестирование первой фабрики...");
            ClientMethod(new Dell());
            Console.WriteLine();

            Console.WriteLine("Клиент: тестирование второй фабрики...");
            ClientMethod(new Sony());
        }

        public void ClientMethod(ICarFactory factory)
        {
            IProcessor processor = factory.CreateProcessor();
            IMainboard mainboard = factory.CreateMainboard();

            Console.WriteLine(mainboard.ShowBatteryVolume());
            Console.WriteLine(mainboard.ShowProcessor(processor));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new Client().Main();
            Console.ReadLine();
        }
    }

}
