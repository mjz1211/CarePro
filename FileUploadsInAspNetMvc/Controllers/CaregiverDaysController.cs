using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FileUploadsInAspNetMvc.DAL;
using FileUploadsInAspNetMvc.Models;

using Line.Messaging;
using System.Threading.Tasks;


namespace FileUploadsInAspNetMvc.Controllers
{
    public class CaregiverDaysController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: CaregiverDays
        public ActionResult Index()
        {
            return View(db.CaregiverDays.OrderBy(c=>c.CareDateTime).ToList());
        }

        public ActionResult CareIndex()
        {
            return View(db.CaregiverDays.Where(c=>c.CareTitle != "照護設定").OrderBy(c => c.CareDateTime).ToList());
        }


        public async Task<ActionResult> Send(int id)
        {
            ISendMessage replyMessage = null;

            var caregiverDay = db.CaregiverDays.Find(id);

            if (caregiverDay != null)
            {
                string title = caregiverDay.CareTitle;
                if (title.IndexOf("快樂照片") != -1)
                {
                    var imageUrl = "https://fileuploadsinmvc.azurewebsites.net";

                    List<ImageCarouselColumn> happyImageCarouselColumns = new List<ImageCarouselColumn>();

                    int i = 1;
                    var happyImages = db.CareElderImages.ToArray();
                    foreach (var item in happyImages)
                    {
                        if (i > 5)
                            break;
                        UriTemplateAction action = new UriTemplateAction("快樂照片" + i, imageUrl + item.ImageUrl);
                        happyImageCarouselColumns.Add(new ImageCarouselColumn(imageUrl + item.ImageUrl, action));
                        i++;

                    }

                    replyMessage = new TemplateMessage("ImageCarouselTemplate",
                        new ImageCarouselTemplate(happyImageCarouselColumns));
                    await PushLineMessage(replyMessage);
                }
                else if (title.IndexOf("照護紀錄") != -1)
                {
                    replyMessage = new TemplateMessage(title,
                                        new ButtonsTemplate(text: "您覺得最近一次照護活動如何？", title: title,
                                        actions: new List<ITemplateAction> {
                                                        new PostbackTemplateAction("很好", "good"),
                                                        new PostbackTemplateAction("一般", "normal"),
                                                        new PostbackTemplateAction("再改善", "improve")
                                        }));
                    await PushLineMessage(replyMessage);

                }
                else if (title.IndexOf("照護設定") != -1)
                {
                    replyMessage = new TemplateMessage(title,
                    new ButtonsTemplate(text: "修改設定", title: title,thumbnailImageUrl: "https://fileuploadsinmvc.azurewebsites.net/Content/Images/heart.png",
                    actions: new List<ITemplateAction> {
                                                        new UriTemplateAction("設定 configure", "https://fileuploadsinmvc.azurewebsites.net/CaregiverDays/CareIndex")
                    }));
                    await PushLineMessage(replyMessage);

                }
                else
                {
                    var imageUrl = "https://fileuploadsinmvc.azurewebsites.net/Content/Images";
                    List<CarouselItem> carouselColumns = new List<CarouselItem>();


                    if (title.IndexOf("散步地圖") != -1)
                    {
                        string title1 = caregiverDay.CareTitle;
                        string text1 = caregiverDay.CareDateTime + " " + caregiverDay.CareText;
                        string imageUrl1 = imageUrl + caregiverDay.CareImageUrl;

                        string url1 = caregiverDay.CareUrl;
                        if (string.IsNullOrEmpty(url1))
                            url1 = "https://fileuploadsinmvc.azurewebsites.net/uploads/5cfec41b-94f0-420a-a810-3cfdd9602593.jpeg";

                        string title2 = caregiverDay.CareTitle;
                        string text2 = caregiverDay.CareDateTime + " " + "仙岩公園";
                        string imageUrl2 = imageUrl + caregiverDay.CareImageUrl;
                        string url2 = "http://fileuploadsinmvc.azurewebsites.net/CaregiverDays/RouteMap?lat=24.998244&lng=121.549549";

                        List<ITemplateAction> actions1 = new List<ITemplateAction>();
                        actions1.Add(new UriTemplateAction("全長 2 公里", url1));
                        List<ITemplateAction> actions2 = new List<ITemplateAction>();
                        actions2.Add(new UriTemplateAction("全長 3.5 公里", url2));

                        carouselColumns.Add(new CarouselItem { Title = title1, ImageUrl = imageUrl1, Actions = actions1, Text = text1 });
                        carouselColumns.Add(new CarouselItem { Title = title2, ImageUrl = imageUrl2, Actions = actions2, Text = text2 });
                        replyMessage = new TemplateMessage("Caregiver", GetCarouselTemplate(carouselColumns));

                        await PushLineMessage(replyMessage);

                    }
                    else if (title.IndexOf("用藥安全") != -1)
                    {
                        string title1 = caregiverDay.CareTitle + "/提醒";
                        string text1 = caregiverDay.CareDateTime + " 現在請服用 " + caregiverDay.CareText;
                        string imageUrl1 = imageUrl + caregiverDay.CareImageUrl;

                        List<ITemplateAction> actions1 = new List<ITemplateAction>();
                        actions1.Add(new PostbackTemplateAction("有服用", "Yes"));
                        actions1.Add(new PostbackTemplateAction("未服用", "No"));

                        carouselColumns.Add(new CarouselItem { Title = title1, ImageUrl = imageUrl1, Actions = actions1, Text = text1 });
                        replyMessage = new TemplateMessage("Caregiver", GetCarouselTemplate(carouselColumns));

                        await PushLineMessage(replyMessage);

                    }
                    else /* 照護安全、檢視報告 */
                    {
                        string title1 = caregiverDay.CareTitle;
                        string text1 = caregiverDay.CareDateTime + " " + caregiverDay.CareText;
                        string imageUrl1 = imageUrl + caregiverDay.CareImageUrl;

                        string url1 = caregiverDay.CareUrl;
                        if (string.IsNullOrEmpty(url1))
                            url1 = "https://fileuploadsinmvc.azurewebsites.net/uploads/5cfec41b-94f0-420a-a810-3cfdd9602593.jpeg";

                        List<ITemplateAction> actions1 = new List<ITemplateAction>();
                        actions1.Add(new UriTemplateAction("步驟 steps", url1));

                        carouselColumns.Add(new CarouselItem { Title = title1, ImageUrl = imageUrl1, Actions = actions1, Text = text1 });
                        replyMessage = new TemplateMessage("Caregiver", GetCarouselTemplate(carouselColumns));

                        await PushLineMessage(replyMessage);

                        /* 檢視報告要再送 audio message */
                        if (title1.IndexOf("檢視報告") != -1)
                        {
                            string audioUrl = "https://fileuploadsinmvc.azurewebsites.net";
                            string aUrl = audioUrl + "/Content/Audio/report_audio.m4a";
                            replyMessage = new AudioMessage(originalContentUrl: aUrl, duration: 11000);
                            await PushLineMessage(replyMessage);
                        }

                    }

                }


                /* 送文字提醒 */
                if (title.IndexOf("用藥安全") != -1)
                {
                    
                    string note = "";
                    string medicine = "LOVASTATIN";
                    var careElderMedicines = db.CareElderMedicines2.Where(c => c.EName.StartsWith(medicine));
                    if (careElderMedicines.Count() > 0)
                    {
                        string pc = careElderMedicines.First().PackageCode;
                        if (!string.IsNullOrEmpty(pc))
                            note = "高血脂藥(Lovastatin)" + pc + "Hindari makanan grapefruit, itu akan menghambat metabolisme obat-obatan di hati, yang mengarah ke penurunan metabolisme obat.Dosis kumulatif akan meningkatkan kemanjuran dan bahkan menyebabkan efek samping atau toksisitas.";
                    }
                    await PushLineMessage(new TextMessage(note));
                }
                else if (title.IndexOf("照護安全") != -1)
                {
                    string note = "進行口腔清潔前後，請記得洗手喔！避免相互感染，保護自己也保護被看護人喔！Sebelum dan sesudah membersihkan rongga mulut ingat untuk mencuci tangan! Agar tidak saling menular, melindungi diri sendiri sama dengan melindungi pasien!";
                    await PushLineMessage(new TextMessage(note));
                }


            }


            return RedirectToAction("Index", "CaregiverDays", new { message = "OK" });
        }

        public async Task PushLineMessage(ISendMessage replyMessage) {
            LineMessagingClient lineMessagingClient = new LineMessagingClient(System.Configuration.ConfigurationManager.AppSettings["ChannelAccessToken"]);
            var debugUserId = System.Configuration.ConfigurationManager.AppSettings["DebugUser"];
            await lineMessagingClient.PushMessageAsync(debugUserId, new List<ISendMessage> { replyMessage });

        }


        private CarouselTemplate GetCarouselTemplate(List<CarouselItem> carouselItems)
        {
            CarouselTemplate carouselTemplate = null;
            List<CarouselColumn> carouselColumns = new List<CarouselColumn>();
            foreach (CarouselItem item in carouselItems)
            {
                //
                carouselColumns.Add(new CarouselColumn(
                    item.Text,
                    item.ImageUrl,
                    item.Title,
                    item.Actions));
            }
            carouselTemplate = new CarouselTemplate(carouselColumns);

            return carouselTemplate;
        }


        public ActionResult RouteMap(string lat, string lng)
        {
            ViewBag.Lat = lat;
            ViewBag.Lng = lng;
            return View();
        }

        // GET: CaregiverDays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaregiverDay caregiverDay = db.CaregiverDays.Find(id);
            if (caregiverDay == null)
            {
                return HttpNotFound();
            }
            return View(caregiverDay);
        }

        // GET: CaregiverDays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CaregiverDays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CareTitle,CareText,CareDateTime")] CaregiverDay caregiverDay)
        {
            if (ModelState.IsValid)
            {
                switch (caregiverDay.CareTitle) {
                    case "照護安全":
                        caregiverDay.CareImageUrl = "/denture.png";
                        break;
                    case "用藥安全":
                        caregiverDay.CareImageUrl = "/mediation.png";
                        break;
                    case "散步地圖":
                        caregiverDay.CareImageUrl = "/street-map.png";
                        break;
                    case "照護紀錄":
                        caregiverDay.CareImageUrl = "/heart.png";
                        break;
                    case "檢視報告":
                        caregiverDay.CareImageUrl = "/analytics.png";
                        break;
                    default:
                        caregiverDay.CareImageUrl = "/heart.png";
                        break;
                }


                db.CaregiverDays.Add(caregiverDay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(caregiverDay);
        }

        // GET: CaregiverDays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaregiverDay caregiverDay = db.CaregiverDays.Find(id);
            if (caregiverDay == null)
            {
                return HttpNotFound();
            }
            return View(caregiverDay);
        }

        // POST: CaregiverDays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CareTitle,CareText,CareImageUrl,CareDateTime,CareUrl")] CaregiverDay caregiverDay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caregiverDay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(caregiverDay);
        }

        // GET: CaregiverDays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaregiverDay caregiverDay = db.CaregiverDays.Find(id);
            if (caregiverDay == null)
            {
                return HttpNotFound();
            }
            return View(caregiverDay);
        }

        // POST: CaregiverDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CaregiverDay caregiverDay = db.CaregiverDays.Find(id);
            db.CaregiverDays.Remove(caregiverDay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
