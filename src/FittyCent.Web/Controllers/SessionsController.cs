using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FittyCent.Data;
using FittyCent.Domain;
using FittyCent.Web.Models;

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
        public ActionResult Create() {
            return View();
        }

        [HttpPost, Authorize(Roles = Constants.Roles.Trainer)]
        public ActionResult Create(SessionModel model) {
            if ( ModelState.IsValid ) {
                var session = new Session();

                Mapper.Map(model, session);
                var trainerClass = _unitOfWork.Repository.Attach(new TrainerClass { Id = model.TrainerClassId });

                session.Class = trainerClass;

                _unitOfWork.Repository.Add(session);
                _unitOfWork.Save();

                return RedirectToAction("Details", new { session.Id });
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
            return model;
        }
    }
}