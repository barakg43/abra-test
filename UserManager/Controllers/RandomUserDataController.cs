
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;

namespace UserManager
{

    

    [ApiController]
    [Route("api/[controller]")]
    public class RandomUserDataController : ControllerBase
    {

       

        HttpClient httpClient = new HttpClient();


        [HttpGet("get-user-data")]
        public  string Get10UserData([FromQuery] string gender)
        {

            string randomUserApiUrl = string.Format(@"https://randomuser.me/api?results=10&gender={0}", gender);

            HttpRequestMessage requestMassage = new HttpRequestMessage(HttpMethod.Get, randomUserApiUrl);
            HttpResponseMessage response =  httpClient.Send(requestMassage);
            response.EnsureSuccessStatusCode();
           

            StreamReader reader = new StreamReader(response.Content.ReadAsStream());
            return  reader.ReadToEnd();

            
           
        }

        [HttpGet("get-most-pupalar-country")]
        public string GetTheMostPupalarCountry()
        {

          
            HttpRequestMessage requestMassage = new HttpRequestMessage(HttpMethod.Get, "https://randomuser.me/api?results=5000");
            HttpResponseMessage response = httpClient.Send(requestMassage);
            response.EnsureSuccessStatusCode();


            StreamReader reader = new StreamReader(response.Content.ReadAsStream());
            string bodyRespone= reader.ReadToEnd();

            RandomUsersObject randomUsersObject = JsonSerializer.Deserialize<RandomUsersObject>(bodyRespone);
            return getTheMostPupalarCountryFromList(randomUsersObject.Results);


        }


        private string getTheMostPupalarCountryFromList(List<RandomUserDataDTO> userDataDTOs)
        {
            string countryName = "";
            Dictionary<string, int> countryCountMap = new Dictionary<string, int>();
            string currentContryName = "";
            if (userDataDTOs != null && userDataDTOs.Count > 0)
            {


                foreach (RandomUserDataDTO userData in userDataDTOs)
                {
                    incrementCountryCounter(countryCountMap, userData.Location.Country);
                }
                countryName = findTheMostCountryAppearance(countryCountMap);

            }

            return countryName;

        }

        private string findTheMostCountryAppearance(Dictionary<string, int> countryCountMap)
        {

            int maxCountryCount = countryCountMap.Values.Max();
            string countryMostAppearance = countryCountMap.First(item => item.Value == maxCountryCount).Key;

            return countryMostAppearance;
        }

        private void incrementCountryCounter(Dictionary<string, int> countryCountMap, string contry)
        {

            if (countryCountMap.ContainsKey(contry))
            {
                countryCountMap[contry]++;
            }
            else
            {
                countryCountMap.Add(contry, 1);
            }
        }


        [HttpGet("get-random-30-email-list")]
        public List<string> GetEmailList()
        {

            List<string> emailList = new List<string>();
            string bodyRequset;
            HttpRequestMessage requestMassage = new HttpRequestMessage(HttpMethod.Get, "https://randomuser.me/api?results=30");
            HttpResponseMessage response = httpClient.Send(requestMassage);
            response.EnsureSuccessStatusCode();
            StreamReader reader = new StreamReader(response.Content.ReadAsStream());
            string bodyRespone = reader.ReadToEnd();
            return new List<string> { bodyRespone };

            RandomUsersObject randomUsersObject = JsonSerializer.Deserialize<RandomUsersObject>(bodyRespone);

            Console.WriteLine(randomUsersObject);
            if (randomUsersObject?.Results != null)
            {
                foreach (RandomUserDataDTO userData in randomUsersObject.Results)
                {
                    Console.WriteLine(userData.Name.First);
                    emailList.Add(userData.Email);
                }
            }

            return emailList;
        }

        [HttpGet("get-oldest-user-from-random-list")]
        public OldestUserName? GetTheOldestRandomUser()
        {


            HttpRequestMessage requestMassage = new HttpRequestMessage(HttpMethod.Get, "https://randomuser.me/api?results=100");
            HttpResponseMessage response = httpClient.Send(requestMassage);
            response.EnsureSuccessStatusCode();
            StreamReader reader = new StreamReader(response.Content.ReadAsStream());
            string bodyRespone = reader.ReadToEnd();
            RandomUsersObject randomUsersObject = JsonSerializer.Deserialize<RandomUsersObject>(bodyRespone);
            List<RandomUserDataDTO> userDataDTOs = JsonSerializer.Deserialize<List<RandomUserDataDTO>>(response.Content.ReadAsStream());
            
            return getOldestUser(userDataDTOs);


        }

        private OldestUserName? getOldestUser(List<RandomUserDataDTO> userDataDTOs)
        {
            OldestUserName? oldestUser =null;
            if (userDataDTOs != null && userDataDTOs.Count > 0)
            {
                oldestUser = GetOldestUserOject(userDataDTOs[0]);

                foreach (RandomUserDataDTO userData in userDataDTOs)
                {
                    if (userData.Registered.Age < oldestUser?.Age)
                    {
                        oldestUser = GetOldestUserOject(userData);
                    }
                }
            
            }
            return oldestUser;
        }  

        private OldestUserName GetOldestUserOject(RandomUserDataDTO userDataDTO) {
  

            return new OldestUserName(userDataDTO.Id.Name, userDataDTO.Registered.Age);
        }

    }
        


}