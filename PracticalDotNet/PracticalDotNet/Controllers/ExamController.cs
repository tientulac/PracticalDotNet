﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PracticalDotNet.Context;
using PracticalDotNet.Models;

namespace PracticalDotNet.Controllers
{
    public class ExamController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Exam
        public ActionResult Index(string search, string sortOrder, string classroomId)
        {
            ViewBag.ClassroomId = 0;
            string sort = !String.IsNullOrEmpty(sortOrder) ? sortOrder : "asc";

            var exams = from p in db.Exams select p;
            if (!String.IsNullOrEmpty(search))
            {
                exams = exams.Where(p => p.Name.Contains(search));
            }
            switch (sort)
            {
                case "asc": exams = exams.OrderBy(p => p.Name); break;
                case "desc": exams = exams.OrderByDescending(p => p.Name); break;
            }
            // loc theo category
            if (!String.IsNullOrEmpty(classroomId))
            {
                var classId = Convert.ToInt32(classroomId);
                exams = exams.Where(p => p.ClassroomID == classId);
                ViewBag.ClassroomId = classId;
            }


            var classrooms = db.Classrooms.ToList();
            var faculties = db.Faculties.ToList();
            dynamic data = new ExpandoObject();
            data.Exams = exams;
            data.Classrooms = classrooms;
            data.Faculty = faculties;
            return View(data);
            //var exams = db.Exams.Include(e => e.Classroom).Include(e => e.Faculty);
            //return View(exams.ToList());
        }

        // GET: Exam/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // GET: Exam/Create
        public ActionResult Create()
        {
            ViewBag.ClassroomID = new SelectList(db.Classrooms, "Id", "Name");
            ViewBag.FacultyID = new SelectList(db.Faculties, "Id", "Name");
            return View();
        }

        // POST: Exam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Start_time,Exam_date,Exam_duration,Status,ClassroomID,FacultyID")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                exam.Start_time.ToString();
                db.Exams.Add(exam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassroomID = new SelectList(db.Classrooms, "Id", "Name", exam.ClassroomID);
            ViewBag.FacultyID = new SelectList(db.Faculties, "Id", "Name", exam.FacultyID);
            return View(exam);
        }

        // GET: Exam/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }

            ViewBag.ClassroomID = new SelectList(db.Classrooms, "Id", "Name", exam.ClassroomID);
            ViewBag.FacultyID = new SelectList(db.Faculties, "Id", "Name", exam.FacultyID);

            return View(exam);
        }

        // POST: Exam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Start_time,Exam_date,Exam_duration,Status,ClassroomID,FacultyID")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                exam.Start_time.ToString();
                db.Entry(exam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassroomID = new SelectList(db.Classrooms, "Id", "Name", exam.ClassroomID);
            ViewBag.FacultyID = new SelectList(db.Faculties, "Id", "Name", exam.FacultyID);
            return View(exam);
        }

        // GET: Exam/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.Exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // POST: Exam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exam exam = db.Exams.Find(id);
            db.Exams.Remove(exam);
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
