﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FittyCent.Data;
using FittyCent.Domain;
using FittyCent.Web.Models;
using Microsoft.AspNet.Identity;

namespace FittyCent.Web.Controllers {
    [Authorize]
    public class ClassesController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public ClassesController() {
            _unitOfWork = Hacks.CreateUnitOfWork();
        }

        [HttpGet]
        public ActionResult Index() {
            var query = _unitOfWork.Repository.Query<TrainerClass>();
            var userId = User.Identity.GetUserId();

            var models = ( from c in query
                           where c.User.Id == userId
                           select c ).Project().To<TrainerClassModel>().ToArray();

            return View(models);
        }

        [HttpGet]
        public ActionResult Details(int id) {
            var model = GetTrainerClassModel(id);

            if ( null == model ) {
                return RedirectToAction("Index");
            }

            var sessions = GetSessionModels(id);

            model.Sessions = sessions.ToArray();

            return View(model);
        }

        private IEnumerable<SessionModel> GetSessionModels(int id) {
            var query = _unitOfWork.Repository.Query<Session>();

            var models = ( from s in query
                           where s.TrainerClassId == id
                           select s ).Project().To<SessionModel>();

            return models;
        }

        [HttpGet, Authorize(Roles = Constants.Roles.Trainer)]
        public ActionResult Create() {
            return View();
        }

        [HttpPost, Authorize(Roles = Constants.Roles.Trainer)]
        public ActionResult Create(TrainerClassModel model) {
            if ( ModelState.IsValid ) {
                var trainerClass = new TrainerClass();

                Mapper.Map(model, trainerClass);
                var userAccount = _unitOfWork.Repository.Attach(new UserAccount { Id = User.Identity.GetUserId() });

                trainerClass.User = userAccount;

                _unitOfWork.Repository.Add(trainerClass);
                _unitOfWork.Save();

                return RedirectToAction("Details", new { trainerClass.Id });
            }

            return View(model);
        }

        [HttpGet, Authorize(Roles = Constants.Roles.Trainer)]
        public ActionResult Edit(int id) {
            var model = GetTrainerClassModel(id);

            if ( null == model ) {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost, Authorize(Roles = Constants.Roles.Trainer)]
        public ActionResult Edit(TrainerClassModel model) {
            if ( ModelState.IsValid ) {
                var trainerClass = _unitOfWork.Repository.Find<TrainerClass>(model.Id);
                Mapper.Map(model, trainerClass);
                _unitOfWork.Save();

                return RedirectToAction("Details", new { trainerClass.Id });
            }

            return View(model);
        }

        private TrainerClassModel GetTrainerClassModel(int id) {
            var query = _unitOfWork.Repository.Query<TrainerClass>();

            var model = ( from p in query
                          where p.Id == id
                          select p ).Project().To<TrainerClassModel>().SingleOrDefault();
            return model;
        }
    }
}