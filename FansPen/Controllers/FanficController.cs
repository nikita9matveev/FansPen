using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using FansPen.Domain.Models;
using FansPen.Domain.Repository;
using FansPen.Web.Models.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;

namespace FansPen.Web.Controllers
{
    public class FanficController : Controller
    {
        public FanficRepository FanficRepository;
        public TagRepository TagRepository;
        public TopicRepository TopicRepository;

        private FileStream file;
        private PdfWriter wri;
        private Document pdfDoc;

        private FanficViewModel _fanficViewModel { get; set; }

        public FanficController(ApplicationContext context)
        {
            FanficRepository = new FanficRepository(context);
            TagRepository = new TagRepository(context);
            TopicRepository = new TopicRepository(context);
        }

        [HttpGet]
        [Route("Fanfic")]
        public IActionResult Fanfic(int id)
        {
            _fanficViewModel = Mapper.Map<FanficViewModel>(FanficRepository.GetById(id));
            if (_fanficViewModel == null) return LocalRedirect("/");
            _fanficViewModel.SetTags(Mapper.Map<List<TagViewModel>>(TagRepository.GetList()));
            return View("Index", _fanficViewModel);
        }

        public IActionResult ExportToPdf(string returnUrl, int id)
        {
            var fanfic = Mapper.Map<FanficViewModel>(FanficRepository.GetById(id));
            var topics = Mapper.Map<List<TopicViewModel>>(TopicRepository.GetTopicsByFanficId(id));
            try
            {
                pdfDoc = new Document(PageSize.LETTER, 40f, 40f, 60f, 60f);
                BaseFont baseFont = BaseFont.CreateFont(@"wwwroot/font/arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                string path = @"wwwroot/pdf/tester1.pdf";
                file = new FileStream(path, FileMode.Create);
                wri = PdfWriter.GetInstance(pdfDoc, file);
                pdfDoc.Open();
                var spacer = new Paragraph("\n");
                FileStream fileLogo = new FileStream(@"wwwroot/images/icons/logoPDF.png", FileMode.Open);
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
                pdfDoc.Add(spacer);
                pdfDoc.Add(spacer);
                Paragraph info = new Paragraph("Author: " + fanfic.ApplicationUser.UserName + "\n" + "Rating: " + fanfic.AverageRating + "\n" + "Date: " + fanfic.CreateDate.ToShortDateString(), new Font(baseFont, 11, 2));
                pdfDoc.Add(info);
                pdfDoc.Add(spacer);
                var avatarFanfic = fanfic.ImgUrl;
                avatarFanfic = avatarFanfic.Substring(0, 47) + "t_FanficPDF" + avatarFanfic.Substring(58, 22) + "jpg";
                Uri uri = new Uri(avatarFanfic);
                Jpeg img = new Jpeg(uri);
                pdfDoc.Add(img);
                pdfDoc.Add(spacer);
                Paragraph descLabel = new Paragraph("Description: ", new Font(baseFont, 16));
                pdfDoc.Add(descLabel);
                pdfDoc.Add(spacer);
                string arialuniTff = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
                FontFactory.Register(arialuniTff);
                StyleSheet ST = new StyleSheet();
                ST.LoadTagStyle(HtmlTags.BODY, HtmlTags.FACE, "Arial Unicode MS");
                ST.LoadTagStyle(HtmlTags.BODY, HtmlTags.ENCODING, BaseFont.IDENTITY_H);
                using (var htmlWorker = new HTMLWorker(pdfDoc))
                {
                    using (var sr = new StringReader(fanfic.Description))
                    {
                        htmlWorker.SetStyleSheet(ST);
                        htmlWorker.Parse(sr);
                    }
                }
                pdfDoc.Add(spacer);
                Chapter content = new Chapter(new Paragraph("Content: ", new Font(baseFont, 16)), 0);
                pdfDoc.Add(content);
                foreach (var item in topics)
                {
                    pdfDoc.Add(spacer);
                    Paragraph chapter = new Paragraph("Chapter " + item.Number + ". " + item.Name, new Font(baseFont, 12));
                    pdfDoc.Add(chapter);
                }
                foreach (var item in topics)
                {
                    Chapter chapter = new Chapter(new Paragraph(item.Name, new Font(baseFont, 18)), item.Number);
                    pdfDoc.Add(chapter);
                    pdfDoc.Add(spacer);
                    Paragraph infoTopic = new Paragraph("Rating: " + item.AverageRating, new Font(baseFont, 11));
                    pdfDoc.Add(infoTopic);
                    pdfDoc.Add(spacer);
                    if (item.ImgUrl != " ")
                    {
                        var avatarTopic = item.ImgUrl;
                        avatarTopic = avatarTopic.Substring(0, 47) + "t_FanficPDF" + avatarTopic.Substring(58, 22) + "jpg";
                        Uri uriTopic = new Uri(avatarTopic);
                        Jpeg imgTopic = new Jpeg(uriTopic);
                        pdfDoc.Add(imgTopic);
                        pdfDoc.Add(spacer);
                    }
                    using (var htmlWorker = new HTMLWorker(pdfDoc))
                    {
                        using (var sr = new StringReader(item.Text))
                        {
                            htmlWorker.SetStyleSheet(ST);
                            htmlWorker.Parse(sr);
                        }
                    }
                }
                pdfDoc.Close();
                wri.Close();
                WebClient User = new WebClient();
                Byte[] FileBuffer = User.DownloadData(@"wwwroot/pdf/tester1.pdf");
                if (FileBuffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.Headers.Add("content-length", FileBuffer.Length.ToString());
                    Response.Body.Write(FileBuffer, 0, FileBuffer.Length);
                }
            }
            catch (Exception ex)
            {
                pdfDoc.Close();
                wri.Close();
            }
            finally
            {                
                System.IO.File.Delete(@"wwwroot/pdf/tester1.pdf");
            }            
            return RedirectPermanent(returnUrl);
        }
    }
}
