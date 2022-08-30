using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetApp.Models;
using TweetApp.Services;

namespace TweetApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[Action]")]
    public class TweetsController : ControllerBase
    {
        private readonly UserDataService _userDataService;

        public TweetsController(UserDataService userDataService) =>
            _userDataService = userDataService;


        /// <summary>
        /// Allows user to register by taking details required to create account
        /// </summary>
        /// <param name="newUserData">User data liek first name, lastname are taken</param>
        /// <returns>Returns a result action method</returns>

        [HttpPost, ActionName("Register")]
        public async Task<IActionResult> Post(UserData newUserData)
        {
            await _userDataService.CreateAsync(newUserData);

            return CreatedAtAction(nameof(Get), new { id = newUserData.Id }, newUserData);
        }


        /// <summary>
        /// aAllows registered users to login 
        /// </summary>
        /// <param name="loginDetails">Uasername and password are given</param>
        /// <returns>IActionResult</returns>
        [HttpPost, ActionName("Login")]
        //[Route("Login")]
        public async Task<IActionResult> Login([FromBody] Login loginDetails)
        {
            var _logindetails = await _userDataService.Find(loginDetails);
            Console.WriteLine(_logindetails);
            if (_logindetails is null)
            {
                return NotFound();
            }
            //return CreatedAtAction(nameof(Login),_logindetails);
            return Content("SuccessfulLogin");
        }

 
        /// <summary>
        /// This method retrieves all the tweets available in the database
        /// </summary>
        /// <returns>A list containing all the tweets with id and username</returns>

        [HttpGet, ActionName("all")]
        public async Task<List<Tweet>> GetAllTweets() =>
            await _userDataService.GetTweetAsync();

        /// <summary>
        /// This method returns all the users containing handle name
        /// </summary>
        /// <returns>List containing handle names</returns>

        [HttpGet, ActionName("Users/All")]
        public async Task<List<string>> GetAllUsers()
        {
            List<Tweet> userList = new List<Tweet>();
            List<string> userName = new List<string>();
            //Tweet tweet = new Tweet();

            userList = await _userDataService.GetTweetAsync();
            foreach(Tweet tweet  in userList)
            {
                userName.Add(tweet.HandleName);
            }
            return userName;
        }

        /// <summary>
        /// This method searches all the tweets based on handle name
        /// </summary>
        /// <param name="userName">Handle name of the user</param>
        /// <returns>Tweet details containing id and handlename</returns>

        [HttpGet, ActionName("User/Search")]
        public async Task<ActionResult<Tweet>> GetTweetByUserName(string userName)
        {
            var userData = await _userDataService.GetTweetAsyncByUserName(userName);

            if (userData is null)
            {
                return NotFound();
            }

            return userData;
        }


        /// <summary>
        /// This method retruns all the tweets based on user name
        /// </summary>
        /// <param name="userName">Handle name of the user</param>
        /// <returns>Gets all the tweets of a particular user</returns>
        [HttpGet,ActionName("GetAllTweetsOfUser")]
        public async Task<List<string>> GetAllTweetsOfUser(string userName)
        {
            List <TweetMessages> tweetMessages= new List<TweetMessages>();
            List<string> tweetMsg = new List<string>();
            var userData = await _userDataService.GetTweetAsyncByUserName(userName);
            var tweetId = userData.TweetId;
            tweetMessages = await _userDataService.GetTweetMessagesbyId(tweetId);
            foreach(TweetMessages tweetMessage in tweetMessages)
            {
                tweetMsg.Add(tweetMessage.TweetMessage);
            }
            return tweetMsg;
        }

        [HttpGet("{id:length(24)}"), ActionName("GetUserWithId")]
        public async Task<ActionResult<TweetMessages>> Get(string id)
        {
            var tweetMessagesData = await _userDataService.GetTweetMessagesAsync(id);

            if (tweetMessagesData is null)
            {
                return NotFound();
            }

            return tweetMessagesData;
        }

        
        /// <summary>
        /// This method allows to post a new tweet by a user
        /// </summary>
        /// <param name="tweetMessages">Tweetmessage containing message, id, time</param>
        /// <returns>Returns actionresult</returns>

        [HttpPost, ActionName("PostNewTweet")]
        public async Task<IActionResult> PostNewTweet([FromBody] TweetMessages tweetMessages)
        {
            await _userDataService.CreateTweetAsync(tweetMessages);

            return CreatedAtAction(nameof(Get), new { id = tweetMessages.TweetId }, tweetMessages);
        }


        /// <summary>
        /// This allows the user to update a particular tweet
        /// </summary>
        /// <param name="id">Tweet Message id of type string</param>
        /// <param name="updatedTweetMessages">Tweet to update the tweet with</param>
        /// <returns>IACtionresult</returns>

        [HttpPut,ActionName("UpdateTweet")]
        public async Task<IActionResult> UpdateTweet(string id, TweetMessages updatedTweetMessages)
        {
            var userData = await _userDataService.GetTweetMessagesAsync(id);

            if (userData is null)
            {
                return NotFound();
            }

            updatedTweetMessages.Id = userData.Id;

            await _userDataService.UpdateTweetAsync(id, updatedTweetMessages);

            return NoContent();
        }

        /// <summary>
        /// Delete the required tweet by the user
        /// </summary>
        /// <param name="id">Object id of type string of particular tweet message</param>
        /// <returns>Returns action result</returns>

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteTweet(string id)
        {
            var tweet = await _userDataService.GetTweetMessagesAsync(id);

            if (tweet is null)
            {
                return NotFound();
            }

            await _userDataService.RemoveTweetAsync(id);

            return NoContent();
        }

        /// <summary>
        /// This method is used to like a particular tweet
        /// </summary>
        /// <param name="id">Object id of type string of particular tweet message</param>
        /// <returns>Returns IActionResult</returns>

        [HttpPut,ActionName("LikeTweet")]
        public async Task<IActionResult> LikeTweet(string id)
        {
            TweetMessages tweetMessages = new TweetMessages();

            TweetMessages userData = await _userDataService.GetTweetMessagesAsync(id);
            if(userData.Id==id)
            {
                userData.Like = userData.Like + 1;
            }

            if (userData is null)
            {
                return NotFound();
            }

            tweetMessages.Id = userData.Id;
            tweetMessages.TweetMessage = userData.TweetMessage;
            tweetMessages.Like = userData.Like;
            tweetMessages.Time = userData.Time;
            tweetMessages.Id = userData.Id;

            await _userDataService.UpdateLikeAsync(id, tweetMessages);

            return NoContent();
        }

        
    }
}
