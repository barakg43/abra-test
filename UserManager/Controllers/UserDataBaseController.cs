
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;



namespace UserManager.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserDataBaseController
    {

        private readonly Dictionary<int,UserData> userNameDatabase;

        [HttpPost("add-user")]
        public void AddUser([FromBody] UserData userData) {

            if (userNameDatabase.ContainsKey(userData.Id))
            {

                throw new InvalidOperationException();

            }
            else
            {
                userNameDatabase.Add(userData.Id, userData);
            }


        }
        [HttpGet("get-user")]
        public UserData GetUser([FromQuery] int id)
        {

            return userNameDatabase[id];

        }
        [HttpGet("update-user")]
        public UserData UpdateUser([FromBody] UserData userData)
        {

            return userNameDatabase[userData.Id]=userData;

        }
    }
}
