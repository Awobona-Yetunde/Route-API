using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using RouteAPi.Data;
using RouteAPi.Models.Domain;
using RouteAPi.Models.DTO;

namespace RouteAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly RouteDbContext dbContext;

        public RouteController(RouteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //GET DATA FRON DB - DOMAIN MODELS
            var routes = dbContext.Routes.ToList();

            //Map Domain models to DTOs
            var routeDto = new List<RouteDTO>();
            foreach (var route in routes)
            {
                routeDto.Add(new RouteDTO()
                {
                    Id = route.Id,
                    Origin = route.Origin,
                    Destination = route.Destination,
                    Distance = route.Distance,
                    Leadtime = route.Leadtime,
                });

            }

            //Return DTOs
            return Ok(routeDto);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            //Get route domain models from DB
            var route = dbContext.Routes.Find(id);
            if (route == null)
            {
                return NotFound();
            }
            //Map Route domain models tp route DTO
            var routeDto = new RouteDTO
            {
                Id = route.Id,
                Origin = route.Origin,
                Destination = route.Destination,
                Distance = route.Distance,
                Leadtime = route.Leadtime,
            };

            //Return DTO
            return Ok(routeDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRouteRequestDTO addRouteRequestDTO)
        {
            //Map DTO to domain model 
            var routeDomainModel = new Models.Domain.Route
            {
                Origin = addRouteRequestDTO.Origin,
                Destination = addRouteRequestDTO.Destination,
                Distance = addRouteRequestDTO.Distance,
                Leadtime = addRouteRequestDTO.Leadtime,
            };

            //Use domain models to create Route
            dbContext.Routes.Add(routeDomainModel);
            dbContext.SaveChanges();

            //Map domain model to DTO
            var routeDto = new RouteDTO
            {
                Id = routeDomainModel.Id,
                Origin = routeDomainModel.Origin,
                Destination = routeDomainModel.Destination,
                Distance = routeDomainModel.Distance,
                Leadtime = routeDomainModel.Leadtime,
            };
            return CreatedAtAction(nameof(GetById), new { id = routeDomainModel.Id }, routeDto);
        }


        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            var route = dbContext.Routes.Find(id);
            if (route == null)
            {
                return BadRequest();
            }

            dbContext.Routes.Remove(route);
            dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id,[FromBody] Models.Domain.Route route)
        {
            if (id != route.Id)
            {
                return BadRequest();
            }
            dbContext.Entry(route).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();
            dbContext.Routes.Add(route);

            return Ok(route);
        }
        
    }
}
