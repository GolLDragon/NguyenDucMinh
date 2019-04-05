using BigSchoolS.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchoolS.Controllers
{
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbContext;

        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend([FromBody] int courseId)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.Attendance.Any(a => a.AttendeeId == userId && a.CourseId == courseId))
                return BadRequest("The Attendance already exists!");
            var attendance = new Attendance
            {
                CourseId = courseId,
                AttendeeId = userId
            };
            _dbContext.Attendance.Add(attendance);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
