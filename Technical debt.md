# Технический долг
## Анализ признаков технического долга
| Признак технического долга | Способ решения |
| --------------------------:| --------------:|
| Нечитабельный код          | Рефакторинг кода |
| Дублирующийся код          | Рефакторинг кода |
| Запутанная архитектура     | Создание интерфейсов для упрощения архитектуры и расширяемости |
| Незакомиченный код / Долгоживущие ветки | Удаление всех завершенных веток |
| Отсутствие / Несоответствие технической документации      | Соответствует | - |
## Запутанная архитектура ДО
```C#
public class FanficController : Controller
    {
        public FanficRepository FanficRepository;
        public TagRepository TagRepository;
        public TopicRepository TopicRepository;

        private FileStream _file;
        private PdfWriter _wri;
        private Document _pdfDoc;

        private FanficViewModel _fanficViewModel { get; set; }

        public FanficController(ApplicationContext context)
        {
            FanficRepository = new FanficRepository(context);
            TagRepository = new TagRepository(context);
            TopicRepository = new TopicRepository(context);
        }
     }
```
## Запутанная архитектура ПОСЛЕ
```C#
public interface IRepository 
{
  ...
}

public class FanficController : Controller
    {
        public IRepository Repository;

        private FileStream _file;
        private PdfWriter _wri;
        private Document _pdfDoc;

        private FanficViewModel _fanficViewModel { get; set; }

        public FanficController(IRepository icontext)
        {
            Repository = icontext;
        }
     }
```
## Мероприятия по устранению технического долга
* Рефакторинг кода -  устранение дублирующегося кода, вынесение кириллицы и константных строк в файл ресурсов.
* Вынесение бизнес-логики в отдельные классы - создание отдельного класса с методами обработки данных, выгруженных из бд, для представления конечному пользователю.
## Оценка плана мероприятий
* Рефакторинг кода - 3 часа
* Создание интерфейсов - 2 часа 
## Вывод

