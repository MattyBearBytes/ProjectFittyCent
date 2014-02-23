using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FittyCent.Data;
using FittyCent.Domain;
using FittyCent.Web.Models;
using WebGrease.Css;

namespace FittyCent.Web.Controllers {
    public class SessionsController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public SessionsController() {
            _unitOfWork = Hacks.CreateUnitOfWork();
        }

        [HttpGet]
        public ActionResult Details(int id) {
            var model = GetSessionModel(id);

            if ( null == model ) {
                if ( User.IsInRole(Constants.Roles.Trainer) ) {
                    return RedirectToAction("Index", "Classes");

                } else {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        [HttpGet, Authorize(Roles = Constants.Roles.Trainer)]
        public ActionResult Create(int id) {
            var trainerClass = _unitOfWork.Repository.Find<TrainerClass>(id);

            if ( null == trainerClass ) {
                return RedirectToAction("Index", "Classes");
            }

            var defaultTime = DateTime.Now.Date.AddHours(12);
            if ( defaultTime < DateTime.Now ) {
                defaultTime = defaultTime.AddDays(1);
            }

            return View(new SessionModel { TrainerClassId = id, TrainerClassTitle = trainerClass.Title, DateTime = defaultTime });
        }

        [HttpPost, Authorize(Roles = Constants.Roles.Trainer)]
        public ActionResult Create(SessionModel model) {
            if ( ModelState.IsValid ) {
                var session = new Session();

                Mapper.Map(model, session);
                var trainerClass = _unitOfWork.Repository.Find<TrainerClass>(model.TrainerClassId);

                session.TrainerClassId = model.TrainerClassId;
                _unitOfWork.Repository.Add(session);
                _unitOfWork.Save();

                return RedirectToAction("Details", "Classes", new { id = trainerClass.Id });
            }

            return View(model);
        }

        [HttpGet, Authorize(Roles = Constants.Roles.Trainer)]
        public ActionResult Edit(int id) {
            var model = GetSessionModel(id);

            if ( null == model ) {
                return RedirectToAction("Index", "Classes");
            }

            return View(model);
        }

        [HttpPost, Authorize(Roles = Constants.Roles.Trainer)]
        public ActionResult Edit(SessionModel model) {
            if ( ModelState.IsValid ) {
                var trainerClass = _unitOfWork.Repository.Find<Session>(model.Id);
                Mapper.Map(model, trainerClass);
                _unitOfWork.Save();

                return RedirectToAction("Details", new { trainerClass.Id });
            }

            return View(model);
        }

        private SessionModel GetSessionModel(int id) {
            var query = _unitOfWork.Repository.Query<Session>();

            var model = ( from p in query
                          where p.Id == id
                          select p ).Project().To<SessionModel>().SingleOrDefault();

            var trainerClass = _unitOfWork.Repository.Find<TrainerClass>(model.TrainerClassId);

            model.TrainerClassTitle = trainerClass.Title;
            return model;
        }
    }
}