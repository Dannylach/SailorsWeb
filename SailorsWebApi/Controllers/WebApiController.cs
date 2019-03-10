using System;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using SailorsWebApi.BLL_Interfaces;
using SailorsWebApi.Models;

namespace SailorsWebApi.Controllers
{
    public class WebApiController : ApiController
    {
        private readonly IUsersServices usersServices;
        private readonly IRentingServices rentingServices;
        

        public WebApiController(IUsersServices userServices, IRentingServices rentingService)
        {
            usersServices = userServices;
            rentingServices = rentingService;
        }

        #region Auth
        /*
        [HttpPost]
        public IHttpActionResult Login(Users contact)
        {
            bool validEmail = db.Contacts.Any(x => x.Email == contact.Email);

            if (!validEmail)
            {
                return RedirectToAction("Login");
            }

            string password = db.Contacts.Where(x => x.Email == loginForm.Email)
                .Select(x => x.Password)
                .Single();

            bool passwordMatches = Crypto.VerifyHashedPassword(password, loginForm.Password);

            if (!passwordMatches)
            {
                return RedirectToAction("Login");
            }

            string authId = Guid.NewGuid().ToString();

            Session["AuthID"] = authId;

            var cookie = new HttpCookie("AuthID");
            cookie.Value = authId;
            Response.Cookies.Add(cookie);

            return RedirectToAction("Private");
        }
        */
        #endregion

        #region Users

        [Route("api/webapi/getallusers")]
        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            var response = usersServices.GetAllUserNames();
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [Route("api/webapi/getallusersdata")]
        [HttpGet]
        public IHttpActionResult GetAllUsersData()
        {
                var response = usersServices.GetAllUsersData();
                if(response.ResultType) return Ok(response);
                return Content(HttpStatusCode.BadRequest, response);
        }
        
        //TODO It is duplicated function
        [Route("api/webapi/getusersdatabyname")]
        [HttpGet]
        public IHttpActionResult GetUserDataByName(string userName)
        {
            var response = usersServices.GetUserDataByName(userName);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [Route("api/webapi/getallusersbyfunction")]
        [HttpGet]
        public IHttpActionResult GetAllUsersByFunction(int functionId)
        {
            var response = usersServices.GetAllByFunction(functionId);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/getdataforusername")]
        public IHttpActionResult getDataForUserName(string name)
        {
                var response = usersServices.GetUserDataByName(name);
                if(response.ResultType) return Ok(response);
                return Content(HttpStatusCode.BadRequest, response);
        }

        //TODO Change logic
        [HttpGet]
        [Route("api/webapi/getdataforuserid")]
        public IHttpActionResult getAllDataForUserId(int userId)
        {
            var response = usersServices.GetUserDataById(userId);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/getuserbyid")]
        public IHttpActionResult getUserById(int userId)
        {
            var response = usersServices.GetUserById(userId);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/adduser")]
        public IHttpActionResult AddUser(string userName, string userPassword, string userEmail, string phoneNumber, string name, string surname, int functionId = 1)
        {
            var response = usersServices.AddUser(userName, userPassword, userEmail, phoneNumber, functionId, name, surname);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/updateuser")]
        public IHttpActionResult UpdateUser(int userId, string userName, string userEmail, string phoneNumber, int functionId, string name, string surname)
        {
            var response = usersServices.UpdateUser(userId, userName, userEmail, phoneNumber, functionId, name, surname);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/deleteuser")]
        public IHttpActionResult DeleteUser(string userName)
        {
            var response = usersServices.DeleteUser(userName);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        #endregion

        #region Functions

        [HttpGet]
        [Route("api/webapi/getallfunctions")]
        public IHttpActionResult GetAllFunctions()
        {
            var response = usersServices.GetAllFunctions();
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/addfunction")]
        public IHttpActionResult AddFunction(string functionName)
        {
            var response = usersServices.AddFunction(functionName);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/deletefunction")]
        public IHttpActionResult DeleteFunction(string functionName)
        {
            var response = usersServices.DeleteFunction(functionName);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        //TODO Change for respective fields
        [HttpGet]
        [Route("api/webapi/updatefunction")]
        public IHttpActionResult UpdateFunction(int functionId, string functionName)
        {
            var response = usersServices.UpdateFunction(functionId, functionName);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        #endregion

        #region Reservations


        [HttpGet]
        [Route("api/webapi/getAllEvents")]
        public IHttpActionResult Get()
        {
            ResponseWrapper<object> response = rentingServices.GetAllRentings();
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        #region Harbour

        [HttpGet]
        [Route("api/webapi/getallharbourreservations")]
        public IHttpActionResult GetAllHarbourReservations()
        {
            ResponseWrapper<object> response = rentingServices.GetAllHarbourRentings();
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/getharbourrentingbyid")]
        public IHttpActionResult GetHarbourRentingById(int rentId)
        {
            ResponseWrapper<object> response = rentingServices.GetHarbourRentingById(rentId);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/getharbourrentingsforuser")]
        public IHttpActionResult GetHarbourRentingsForUser(string userName)
        {
            ResponseWrapper<object> response = rentingServices.GetHarbourRentingsForUser(userName);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/rentharbour")]
        public IHttpActionResult RentHarbour(string userName, string startDate, string endDate, string rentName, string rentDescription)
        {
            ResponseWrapper<object> response = rentingServices.RentHarbour(userName, startDate, endDate, rentName, rentDescription);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }
        
        [HttpGet]
        [Route("api/webapi/updateharbourrent")]
        public IHttpActionResult UpdateHarbourRent(int rentId, string userName, string startDate, string endDate, string rentName, string rentDescription)
        {
            ResponseWrapper<object> response = rentingServices.UpdateHarbourRent(rentId, userName, startDate, endDate, rentName, rentDescription);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/deleteharbourrent")]
        public IHttpActionResult DeleteHarbourRent(int rentId)
        {
            ResponseWrapper<object> response = rentingServices.DeleteHarbourRent(rentId);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        #endregion

        #region Equipment

        [HttpGet]
        [Route("api/webapi/getallequipmentreservations")]
        public IHttpActionResult GetAllEquipmentReservations()
        {
            ResponseWrapper<object> response = rentingServices.GetAllEquipmentRentings();
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/getequipmentrentingbyid")]
        public IHttpActionResult GetEuipmentRentingById(int rentId)
        {
            ResponseWrapper<object> response = rentingServices.GetEquipmentRentingById(rentId);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/getequipmentrentingsforuser")]
        public IHttpActionResult GetEquipmentRentingsForUser(string userName)
        {
            ResponseWrapper<object> response = rentingServices.GetEquipmentRentingsForUser(userName);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/rentequipment")]
        public IHttpActionResult RentEquipment(string userName, string startDate, string endDate, int equipmentId)
        {
            ResponseWrapper<object> response = rentingServices.RentEquipment(userName, startDate, endDate, equipmentId);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/updateequipmentrent")]
        public IHttpActionResult UpdateEquipmentRent(int rentId, string userName, string startDate, string endDate, int equipmentId)
        {
            ResponseWrapper<object> response = rentingServices.UpdateEquipmentRent(rentId, userName, startDate, endDate, equipmentId);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/deleteequipmentrent")]
        public IHttpActionResult DeleteEquipmentRent(int rentId)
        {
            ResponseWrapper<object> response = rentingServices.DeleteEquipmentRent(rentId);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        #endregion

        #endregion

        #region Equipment

        #region Instances

        [HttpGet]
        [Route("api/webapi/getallequipment")]
        public IHttpActionResult GetAllEquipment()
        {
            var response = rentingServices.GetAllEquipment();
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/addequipment")]
        public IHttpActionResult AddEquipment(string equipmentName, int typeId)
        {
            var response = rentingServices.AddEquipment(equipmentName, typeId);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/updateequipment")]
        public IHttpActionResult UpdateEquipment(int id, string equipmentName, int typeId)
        {
            var response = rentingServices.UpdateEquipment(id, equipmentName, typeId);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/deleteequipment")]
        public IHttpActionResult DeleteEquipment(int id)
        {
            var response = rentingServices.DeleteEquipment(id);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        #endregion

        #region Types

        [HttpGet]
        [Route("api/webapi/getallequipmenttypes")]
        public IHttpActionResult GetAllEquipmentTypes()
        {
            var response = rentingServices.GetAllEquipmentTypes();
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/addequipmenttype")]
        public IHttpActionResult AddEquipmentType(string equipmentName)
        {
            var response = rentingServices.AddEquipmentType(equipmentName);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/updateequipmenttype")]
        public IHttpActionResult UpdateEquipmentType(int id, string equipmentName)
        {
            var response = rentingServices.UpdateEquipmentType(id, equipmentName);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        [HttpGet]
        [Route("api/webapi/deleteequipmenttype")]
        public IHttpActionResult DeleteEquipmentType(int id)
        {
            var response = rentingServices.DeleteEquipmentType(id);
            if (response.ResultType) return Ok(response);
            return Content(HttpStatusCode.BadRequest, response);
        }

        #endregion

        #endregion
    }
}
