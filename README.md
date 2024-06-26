# CollisiumProblem
Задача: Илон Маск и Марк Цукерберг в римском Колизее
Илон Маск и Марк Цукерберг решили сразиться в поединке в римском Колизее. Боги разгневались на них, схватили и заперли в двух разных комнатах. Они согласны освободить Марка и Илона, если они решат одну задачку.

Есть колода из 36 карт. Боги перемешивают колоду, делят её напополам и отдают одну стопку Илону Маску, а вторую стопку Марку Цукербергу. Затем оба смотрят на свои карты не перемешивая их. Каждый из них должны назвать номер карты в стопке у партнера. Если цвета выбранных карт совпадают, им разрешают сразится иначе, не разрешают.

Илон и Марк, как придвинутые парни, заранее предвидели все эти обстоятельства и имели возможность договориться о стратегиях своего поведения (они могут быть как одинаковы, так и различны для Илона и Марка).

Цель: найти стратегию(и) позволяющие выйти на арену с как можно большей вероятностью.


Задача 1. Консольное приложение
Приложение должно провести 1 000 000 экспериментов, с каким-то стратегиями (например, проверять всегда 1-ю карту). Колода каждый раз перемешивается случайным образом. В консоль выводится отношение количества успехов к общему числу экспериментов.

Стратегия должна размещаться в отдельной сборке типа Class Library.

 

Задача 2. Подключаем .Net Generic Host
Поместить песочницу в .Net Generic Host:

Класс, реализующий проведение единичного экспериментов, выделить в отдельную зависимость;

Класс реализующий перемешивание колоды карт выделить в отдельную зависимость;

Также в качестве зависимостей реализовать партнеров и их соперников.


class Program

{

    public static void Main(string[] args)

    {

        CreateHostBuilder(args).Build().Run();

    }

    

    public static IHostBuilder CreateHostBuilder(string[] args)        	

    {

        return Host.CreateDefaultBuilder(args)

            .ConfigureServices((hostContext, services) =>

            {

                services.AddHostedService<CollisiumExperimentWorker>();

                services.AddScoped<CollisiumSandbox>();

                services.AddScoped<IDeckShufller, DeckShufller>();

                // Зарегистрировать партнеров и их стратегии

            });

    }

}

 

Задача 3. Unit тесты
Реализовать следующие тесты:

Тест колоды карт. Проверить, что колода карт должна иметь по 18 черных и 18 красных карт;

Тесты на стратегию. Проверить, что стратегия должна давать определённый результат на определенным образом перемешанную половину колоды;

Тесты на проведение эксперимента:

Колода должна быть перемешана. Т.е. метод перемешивания должен быть вызван один раз;

Корректность результата эксперимента. При определённом (заранее известном) перемешивании колоды, при определенных стратегиях должен получаться однозначный результат.

 

Задача 4. Взаимодействие с базой данных
Приложение должно поддерживать следующие возможности.

Условие эксперимента, это то порядок карт в колоде:

Сгенерировать 100 экспериментов. Сохранить условия экспериментов в базе данных с помощью EF;

Считать условия 100 эксперимента с (помощью EF), из базы данных, и провести эксперименты в этих условиях;

Реализовать тесты на сохранение и считывание условий экспериментов, с использованием SQLite в режиме выполнения в памяти.

 

Задача 5. Web-API
Комнаты, в которых сидят Илон и Марк, должны быть реализованы в виде отдельных веб служб.

Боги, должны быть реализованы в качестве консольного приложения, которое должно поддерживать возможно проведения заранее записанных экспериментов из предыдущей задачи.

Боги перемешивают колоду и по http, отправляют части колоды партнерам. В ответ получают номер карты, которую нужно проверить. Зная, что они отправили определяют результат эксперимента.

 

Задача 6. MassTransit over RabbitMQ
Илон, Марк и боги должны быть выполнены в виде 3-х различных приложений.

Боги отправляют половины колоды партнерам используя направленную отправку (Send), через MassTransit в ответ, на это партнеры публикуют номер карты, которую нужно проверить у другого.

Боги по http, запрашивают, цвет получившейся у Илона и Марка, после чего, определяют результат эксперимента.
