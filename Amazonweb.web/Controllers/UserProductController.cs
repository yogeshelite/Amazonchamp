using Amazonweb.Controllers;
using Amazonweb.Models;
using AmzonWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amazonweb.web.Controllers
{
    public class UserProductController : Controller
    {
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        List<GetProductModel> ProductsList;
        public UserProductController()
        {




        }
        // GET: ProductSave
        public ActionResult Products()
        {

            ProductsList = GetProducts();
            return View(ProductsList);
        }

        private List<GetProductModel> GetProducts()
        {
            GetProductModel productSaveModel = new GetProductModel()
            {
                UserId = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id
            };
            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(productSaveModel));
            // TODO: Add insert logic here
            var _response = Services.GetApiResponse(Constant.ApiGetProduct, "POST", _request);
            //if (_response == null) return View();
            //---------- Get Api response stream
            using (var _result = new StreamReader(_response.GetResponseStream()))
            {

                //----------REturn response
                dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                var jsonData = JsonConvert.SerializeObject(_data);
                //var jsonUniqueName = jsonData.unique_name;
                if (json.ContainsKey("unique_name"))
                {

                    // if (!login.Success) { ViewBag.Message = login.Response; return View(); }
                    //var jsonString = new { Id = login.Id, UserName = login.Response }; \\ not cretae a correct Formate json
                    _data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json["unique_name"].ToString());
                    if (_data.ContainsKey("Response"))
                    {
                        return JsonConvert.DeserializeObject<List<GetProductModel>>(_data["Response"].ToString());
                    }
                }
                return new List<GetProductModel>();
            }
        }
        public JsonResult GetProductASIN(String UserId)
        {
            GetProductModel productSaveModel = new GetProductModel();
            productSaveModel.UserId = Guid.Parse(UserId);//Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id
            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(productSaveModel));
            // TODO: Add insert logic here
            var _response = Services.GetApiResponse(Constant.ApiGetProduct, "POST", _request);
            //if (_response == null) return View();
            //---------- Get Api response stream
            using (var _result = new StreamReader(_response.GetResponseStream()))
            {

                //----------REturn response
                dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                var jsonData = JsonConvert.SerializeObject(_data);
                //var jsonUniqueName = jsonData.unique_name;
                if (json.ContainsKey("unique_name"))
                {

                    // if (!login.Success) { ViewBag.Message = login.Response; return View(); }
                    //var jsonString = new { Id = login.Id, UserName = login.Response }; \\ not cretae a correct Formate json
                    _data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json["unique_name"].ToString());
                    if (_data.ContainsKey("Response"))
                    {
                        var ListProducts = JsonConvert.DeserializeObject<List<GetProductModel>>(_data["Response"].ToString());
                        var jSonValue = JsonConvert.SerializeObject(ListProducts, Formatting.Indented);
                        //  var jSonValue= json(strserialize, JsonRequestBehavior.AllowGet);
                        return Json(jSonValue, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json("data:Record Not Found");
            }
        }
        // GET: ProductSave/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductSave/Create
        public ActionResult AddProduct()
        {
            var ProductCategoryList = GetListCategory();
            ViewBag.ProductCategoryList = new SelectList(ProductCategoryList, "Id", "Category");

            return View();
        }
        public List<CateoryModel> GetListCategory()
        {
            var ProductCategoryList = new List<CateoryModel>();
            var _request = "";//_JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(loginModel));

            var _response = Services.GetApiResponse(Constant.ApiCategory, "POST", _request);
            //  if (_response == null) return View();
            //---------- Get Api response stream
            using (var _result = new StreamReader(_response.GetResponseStream()))
            {

                //----------REturn response
                dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                var jsonData = JsonConvert.SerializeObject(_data);
                //var jsonUniqueName = jsonData.unique_name;
                if (json.ContainsKey("unique_name"))
                {
                    ProductCategoryList = JsonConvert.DeserializeObject<List<CateoryModel>>(json["unique_name"].ToString());
                    // if (!login.Success) { ViewBag.Message = login.Response; return View(); }
                    //var jsonString = new { Id = login.Id, UserName = login.Response }; \\ not cretae a correct Formate json

                }

            }
            return ProductCategoryList;

        }
        // POST: ProductSave/Create
        [HttpPost]
        public ActionResult Create(ProductSaveModel productSaveModel, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    String VarIsFeacture = collection.GetValue("isFeatured").AttemptedValue;
                    productSaveModel.UserId = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id;
                    productSaveModel.Operation = "insert";
                    //ProductSaveModel productSaveModel = new ProductSaveModel()
                    //{
                    //    ASIN = collection["ASIN"].ToString(),
                    //    categoryId = long.Parse(collection["categoryId"].ToString()),
                    //    UserId = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id,
                    //    isFeatured = Convert.ToBoolean(VarIsFeacture),
                    //    Operation = "insert"
                    //};
                    var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(productSaveModel));
                    // TODO: Add insert logic here
                    var _response = Services.GetApiResponse(Constant.ApiSaveProduct, "POST", _request);
                    if (_response == null) return View();
                    //---------- Get Api response stream
                    using (var _result = new StreamReader(_response.GetResponseStream()))
                    {

                        //----------REturn response
                        dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                        var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                        var jsonData = JsonConvert.SerializeObject(_data);
                        //var jsonUniqueName = jsonData.unique_name;
                        if (json.ContainsKey("unique_name"))
                        {
                            // ProductCategoryList = JsonConvert.DeserializeObject<List<CateoryModel>>(json["unique_name"].ToString());
                            // if (!login.Success) { ViewBag.Message = login.Response; return View(); }
                            //var jsonString = new { Id = login.Id, UserName = login.Response }; \\ not cretae a correct Formate json
                            return RedirectToAction("Products");
                        }

                    }
                }
            }
            catch
            {

            }
            return View();
        }

        // GET: ProductSave/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductSave/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductSave/Delete/5
        public ActionResult Delete(string Asin)
        {
            ProductSaveModel productSaveModel = new ProductSaveModel()
            {
                ASIN = Asin,
                UserId = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id,
                Operation = "delete"
            };
            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(productSaveModel));
            // TODO: Add insert logic here
            var _response = Services.GetApiResponse(Constant.ApiSaveProduct, "POST", _request);
            if (_response == null) return View();
            //---------- Get Api response stream
            using (var _result = new StreamReader(_response.GetResponseStream()))
            {

                //----------REturn response
                dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                var jsonData = JsonConvert.SerializeObject(_data);
                //var jsonUniqueName = jsonData.unique_name;
                if (json.ContainsKey("unique_name"))
                {
                    // ProductCategoryList = JsonConvert.DeserializeObject<List<CateoryModel>>(json["unique_name"].ToString());
                    // if (!login.Success) { ViewBag.Message = login.Response; return View(); }
                    //var jsonString = new { Id = login.Id, UserName = login.Response }; \\ not cretae a correct Formate json


                }

            }
            ProductsList = GetProducts();
            return View("Products", ProductsList);
        }

        // POST: ProductSave/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult SearchProduct()
        {
            ProductsList = GetProducts();
            var ProductCategoryList = GetListCategory();
            ViewBag.ProductCategoryList = new SelectList(ProductCategoryList, "Id", "Category");

            return View(ProductsList);
        }
        [HttpPost]
        public ActionResult SearchProduct(FormCollection collection)
        {

            String ASIN = collection["ASIN"].ToString();
            long categoryId = long.Parse(collection["categoryId"].ToString());

            //ProductSaveModel productSaveModel = new ProductSaveModel()
            //{
            //    ASIN = collection["ASIN"].ToString(),
            //    categoryId = long.Parse(collection["categoryId"].ToString()),
            //    UserId = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id
            //};
            ProductsList = GetProducts();
            var ProductCategoryList = GetListCategory();
            ViewBag.ProductCategoryList = new SelectList(ProductCategoryList, "Id", "Category");

            return View(ProductsList);
        }

        public ActionResult AddFeatured(string ASIN, bool IsFeatured)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    ProductSaveModel productSaveModel = new ProductSaveModel()
                    {
                        ASIN = ASIN,
                        UserId = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id,
                        isFeatured = IsFeatured,
                        Operation = "insert"
                    };
                    var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(productSaveModel));
                    // TODO: Add insert logic here
                    var _response = Services.GetApiResponse(Constant.ApiSaveProduct, "POST", _request);
                    if (_response == null) return View();
                    //---------- Get Api response stream
                    using (var _result = new StreamReader(_response.GetResponseStream()))
                    {

                        //----------REturn response
                        dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                        var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                        var jsonData = JsonConvert.SerializeObject(_data);
                        //var jsonUniqueName = jsonData.unique_name;
                        if (json.ContainsKey("unique_name"))
                        {
                            // ProductCategoryList = JsonConvert.DeserializeObject<List<CateoryModel>>(json["unique_name"].ToString());
                            // if (!login.Success) { ViewBag.Message = login.Response; return View(); }
                            //var jsonString = new { Id = login.Id, UserName = login.Response }; \\ not cretae a correct Formate json

                            //ProductsList = GetProducts();
                            //return View("Products", ProductsList);
                        }

                    }
                }
            }
            catch
            {

            }




            ProductsList = GetProducts();
            return View("Products", ProductsList);
        }
    }
}
