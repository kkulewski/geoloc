﻿using System;
using AutoMapper;
using Geoloc.Models;
using Geoloc.Models.WebModels;
using Geoloc.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Geoloc.Controllers
{
    [Route("api/[controller]")]
    public class MeetingController : Controller
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpGet("[action]/{meetingId}")]
        public MeetingWebModel Get(Guid meetingId)
        {
            var model = _meetingService.GetById(meetingId);
            var result = Mapper.Map<MeetingWebModel>(model);
            return result;
        }

        [HttpPost("[action]")]
        public IActionResult Add([FromBody]MeetingWebModel webModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = new MeetingModel
            {
                Name = webModel.Name,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now + TimeSpan.FromHours(1)
            };

            var isSuccess = _meetingService.AddMeeting(model);
            if (!isSuccess)
            {
                return BadRequest();
            }

            return new OkObjectResult(JsonConvert.SerializeObject("Meeting added"));
        }
    }
}
