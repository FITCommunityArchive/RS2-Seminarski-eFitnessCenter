﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Requests.Workout;
using RS2_Seminarski.Database;
using RS2_Seminarski.Security;
using RS2_Seminarski.Services;

namespace RS2_Seminarski.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutAdviceController : ControllerBase
    {
        private readonly FitnessCenterDbContext _context;
        private readonly WorkoutAdviceService _workoutAdviceService;
        private readonly AuthenticationService _authenticationService;

        public WorkoutAdviceController(FitnessCenterDbContext context)
        {
            _context = context;
            _workoutAdviceService = new WorkoutAdviceService(_context);
            _authenticationService = new AuthenticationService(_context);
        }

        [HttpGet]
        public List<Models.Workout.WorkoutAdvice> GetAll([FromQuery] WorkoutAdviceQueryParams queryParams)
        {
            UserInfo userInfo = _authenticationService.IsAuthorized(Request, new[] { "EMPLOYEE", "CLIENT", "TRAINER" }); 
           return _workoutAdviceService.GetAll(queryParams, userInfo);
        }

        [HttpGet("{id}")]
        public Models.Workout.WorkoutAdvice GetById(int id)
        {
            return _workoutAdviceService.GetById(id);
        }

        [HttpPost]
        public Models.Workout.WorkoutAdvice Create(Models.Requests.Workout.WorkoutAdviceCreate adviceCreate)
        {
            UserInfo userInfo = _authenticationService.IsAuthorized(Request, new[] { "EMPLOYEE", "TRAINER" });
            return _workoutAdviceService.Create(adviceCreate, userInfo);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Models.Requests.Workout.WorkoutAdviceCreate adviceCreate)
        {
            UserInfo userInfo = _authenticationService.IsAuthorized(Request, new[] { "EMPLOYEE", "TRAINER" });
            _workoutAdviceService.Update(id, adviceCreate, userInfo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            UserInfo userInfo = _authenticationService.IsAuthorized(Request, new[] { "EMPLOYEE", "TRAINER" });
            _workoutAdviceService.Delete(id, userInfo);
            return NoContent();
        }
    }
}