//using Application.Users.Delete;
//using Application.Users.Get;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly IMediator sender;
//        public UserController(IMediator sender)
//        {
//            this.sender = sender;
//        }

//        // GET: api/<UserController>
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }

//        // GET api/<UserController>/5
//        [HttpGet("{id:guid}")]
//        public async Task<ActionResult<UserResponse>> Get(Guid id)
//        {
//            var user = await sender.Send(new GetUserQuery(id));
//            return user;
//        }

//        // POST api/<UserController>
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }

//        // PUT api/<UserController>/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/<UserController>/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}
