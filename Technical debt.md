# Технический долг
## Анализ признаков технического долга
| Признак технического долга | Способ решения |
| --------------------------:| --------------:|
| Нечитабельный код          | Рефакторинг кода |
| Дублирующийся код          | Рефакторинг кода |
| Запутанная архитектура     | Создание интерфейсов для упрощения архитектуры и расширяемости |
| Незакомиченный код / Долгоживущие ветки | Удаление всех завершенных веток |
| Отсутствие / Несоответствие технической документации      | Соответствует | - |

## Нечитабельный код ДО
```C#
 public IActionResult ExportToPdf(string returnUrl, int id)
        {
            var fanfic = Mapper.Map<FanficViewModel>(FanficRepository.GetById(id));
            var topics = Mapper.Map<List<TopicViewModel>>(TopicRepository.GetTopicsByFanficId(id));
            try
            {
                pdfDoc = new Document(PageSize.LETTER, 40f, 40f, 60f, 60f);
                BaseFont baseFont = BaseFont.CreateFont(@"D:/home/site/repository/FansPen/wwwroot/font/arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                string path = @"D:/home/site/repository/FansPen/wwwroot/pdf/tester1.pdf";
                file = new FileStream(path, FileMode.Create);
                wri = PdfWriter.GetInstance(pdfDoc, file);
                pdfDoc.Open();
                var spacer = new Paragraph("\n");
                FileStream fileLogo = new FileStream(@"D:/home/site/repository/FansPen/wwwroot/images/icons/logoPDF.png", FileMode.Open);
                var logo = Image.GetInstance(fileLogo);
                logo.SetAbsolutePosition(pdfDoc.Left, pdfDoc.Top);
                pdfDoc.Add(logo);
                fileLogo.Close();
                var helvetica = new Font(baseFont, 20);
                var helveticaBase = helvetica.GetCalculatedBaseFont(false);
                wri.DirectContent.BeginText();
                wri.DirectContent.SetFontAndSize(helveticaBase, 20f);
                wri.DirectContent.ShowTextAligned(Element.ALIGN_CENTER, fanfic.Name, 305, 705, 0);
                wri.DirectContent.EndText();
                ...
```
## Нечитабельный код ПОСЛЕ
```C#
 public IActionResult ExportToPdf(string returnUrl, int id)
        {
            var fanfic = Mapper.Map<FanficViewModel>(FanficRepository.GetById(id));
            var topics = Mapper.Map<List<TopicViewModel>>(TopicRepository.GetTopicsByFanficId(id));
            try
            {
                pdfDoc = new Document(PageSize.LETTER, 40f, 40f, 60f, 60f);
                BaseFont baseFont = BaseFont.CreateFont(Resource.PathToFont);
                string path = Resource.PathToSavePDF;
                file = new FileStream(path, FileMode.Create);
                wri = PdfWriter.GetInstance(pdfDoc, file);
                pdfDoc.Open();
                addLogo();
                setFont();
                ...
```
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
* Рефакторинг кода -  устранение дублирующегося кода, вынесение кириллицы и константных строк в файл ресурсов, дробление больших функций на маленькие.
* Создание интерфейсов для устранения жесткой зависимости между основными классами, расширяемости кода.
## Оценка плана мероприятий
* Рефакторинг кода - 3 часа
* Создание интерфейсов - 2 часа 
## Вывод

