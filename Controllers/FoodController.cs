using ClassLibrary18._9._19;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace WebApplication2.Controllers
{
    public class FoodController : ApiController
    {
       
        List<Food> foods = new List<Food>();
        ClassManager classManager = new ClassManager();


        // GET api/food
        public HttpResponseMessage Get()
        {
            foods = classManager.Foods();
            if (foods.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, foods);
            return msg;
        }

        // GET api/food/5
        [HttpGet]
        public HttpResponseMessage Get([FromUri] int id)
        {
            foods = classManager.Foods();
            Food result = foods.FirstOrDefault(m => m.ID == id);
            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, result);
            return msg;
        }


        [Route("api/food/byname/{name}")]
        [HttpGet]
        public HttpResponseMessage GetFoodByName([FromUri] string name)
        {
            foods = classManager.Foods();
            IEnumerable<Food> result = foods.Where(m => m.Name.ToUpper().Contains(name.ToUpper()));
            if(result==null)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, result);
            return msg;
        }
       
        [HttpPost]
        // POST api/food
        public HttpResponseMessage Post([FromBody]Food food)
        {
            if(food==null)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            
            classManager.AddToDB(food);
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.Created);
            return msg;
        }

        [HttpPut]
        // PUT api/food/5
        public HttpResponseMessage Put(int id, [FromBody]Food food)
        {
            foods = classManager.Foods();
            Food result = foods.FirstOrDefault(m => m.ID == id);
            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            classManager.UpdateDB(food, id);
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.Accepted);
            return msg;
        }

        [HttpDelete]
        // DELETE api/food/5
        public HttpResponseMessage Delete(int id)
        {
            foods = classManager.Foods();
            Food result = foods.FirstOrDefault(m => m.ID == id);
            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            classManager.RemoveFromDB(id);
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK);
            return msg;
        }

        // query string
        // ...api/food/search ? name = ""& grade = 5 & mincal = 60& maxcal=6000
        // ...api/food/search ? name = f & grade = 0 & mincal = 0 & maxcal=6000
        [Route("api/food/search")] 
        [HttpGet]
        public HttpResponseMessage GetFoodsByFilter(string name = "", int grade = 0, int mincal=0, int maxcal=int.MaxValue)
        {
            List<Food> foodsByFilter = classManager.GetFoodsByFilter(mincal,maxcal,name,grade);
            if (foodsByFilter == null || foodsByFilter.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, foodsByFilter);
            return msg;
        }

        [Route("api/food/biggerthancalory/{mincal}")]
        [HttpGet]
        public HttpResponseMessage BiggerThanCalory(int mincal)
        {
            List<Food> foodsBiggerCalories = classManager.GetFoodBiggerThanCalory(mincal);

            if(foodsBiggerCalories == null || foodsBiggerCalories.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, foodsBiggerCalories);
            return msg;
        }
    }
}
