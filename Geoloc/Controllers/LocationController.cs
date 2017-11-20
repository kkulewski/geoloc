﻿using System.Collections.Generic;
using Geoloc.Models;
using Geoloc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Geoloc.Controllers
{
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private readonly ILocationRepository _repo;

        public LocationController(ILocationRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("[action]")]
        public void Send([FromBody]LocationModel model)
        {
            _repo.Add(model);
        }

        [HttpGet("[action]")]
        public IEnumerable<LocationModel> Get()
        {
            return _repo.Get();
        }

    }
}