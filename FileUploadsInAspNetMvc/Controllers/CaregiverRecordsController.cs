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

using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;

namespace FileUploadsInAspNetMvc.Controllers
{
    public class CaregiverRecordsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: CaregiverRecords
        public ActionResult Index()
        {
            return View(db.CaregiverRecords.ToList());
        }

        public ActionResult BloodPressure()
        {
            Highcharts columnChart = new Highcharts("columnchart");

            columnChart.InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Column,
                BackgroundColor = new BackColorOrGradient(System.Drawing.Color.AliceBlue),
                Style = "fontWeight: 'bold', fontSize: '17px'",
                BorderColor = System.Drawing.Color.LightBlue,
                BorderRadius = 0,
                BorderWidth = 2

            });

            columnChart.SetTitle(new Title()
            {
                Text = "血壓紀錄"
            });

            columnChart.SetXAxis(new XAxis()
            {
                Type = AxisTypes.Category,
                Title = new XAxisTitle() { Text = "Date", Style = "fontWeight: 'bold', fontSize: '17px'" },
                Categories = new[] { "8/23", "8/24", "8/25" }
            });

            columnChart.SetYAxis(new YAxis()
            {
                Title = new YAxisTitle()
                {
                    Text = "mmHg",
                    Style = "fontWeight: 'bold', fontSize: '17px'"
                },
                ShowFirstLabel = true,
                ShowLastLabel = true,
                Min = 0
            });

            columnChart.SetLegend(new Legend
            {
                Enabled = true,
                BorderColor = System.Drawing.Color.CornflowerBlue,
                BorderRadius = 6
            });

            columnChart.SetSeries(new Series[]
            {
                new Series{

                    Name = "收縮壓",
                    Data = new Data(new object[] { 110, 115, 105 })
                },
                new Series()
                {
                    Name = "舒張壓",
                    Data = new Data(new object[] { 70, 75, 68 })
                }
            }
            );

            return View(columnChart);
        }

        // GET: CaregiverRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaregiverRecord caregiverRecord = db.CaregiverRecords.Find(id);
            if (caregiverRecord == null)
            {
                return HttpNotFound();
            }
            return View(caregiverRecord);
        }

        // GET: CaregiverRecords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CaregiverRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CareDateTime,CareTitle,CareText,CareFeedback")] CaregiverRecord caregiverRecord)
        {
            if (ModelState.IsValid)
            {
                db.CaregiverRecords.Add(caregiverRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(caregiverRecord);
        }

        // GET: CaregiverRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaregiverRecord caregiverRecord = db.CaregiverRecords.Find(id);
            if (caregiverRecord == null)
            {
                return HttpNotFound();
            }
            return View(caregiverRecord);
        }

        // POST: CaregiverRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CareDateTime,CareTitle,CareText,CareFeedback")] CaregiverRecord caregiverRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caregiverRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(caregiverRecord);
        }

        // GET: CaregiverRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaregiverRecord caregiverRecord = db.CaregiverRecords.Find(id);
            if (caregiverRecord == null)
            {
                return HttpNotFound();
            }
            return View(caregiverRecord);
        }

        // POST: CaregiverRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CaregiverRecord caregiverRecord = db.CaregiverRecords.Find(id);
            db.CaregiverRecords.Remove(caregiverRecord);
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
