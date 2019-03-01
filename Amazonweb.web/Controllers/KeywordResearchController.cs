using Amazonweb.Models;
using LinqToExcel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amazonweb.web.Controllers
{
    public class KeywordResearchController : Controller
    {
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        List<KeywordResearchModel> ListKeyword;
        // GET: KeywordResearch
        public ActionResult KeywordList()
        {
            ListKeyword = GetKeyword("Doll", 1);
            var ProductCategoryList = GetListCategory();
            ViewBag.ProductCategoryList = new SelectList(ProductCategoryList, "Id", "Category");
            UserModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            ViewBag.UserName = MdUser.UserName;
            return View(ListKeyword);
        }

        public ActionResult KeywordAdd()
        {
            UserModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            ViewBag.UserName = MdUser.UserName;

            return View();
        }
        // GET: KeywordResearch/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: KeywordResearch/Create
        public ActionResult Create()
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
        // POST: KeywordResearch/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    KeywordResearchModel objModel = new KeywordResearchModel();
                    objModel.Id = "0";
                    objModel.KeyWord = collection["KeyWord"].ToString();
                    objModel.ExactMatchSearchVolume = long.Parse(collection["ExactMatchSearchVolume"].ToString());
                    objModel.BroadMatchSearchVolume = long.Parse(collection["BroadMatchSearchVolume"].ToString());
                    objModel.CategoryId = long.Parse(collection["CategoryId"].ToString());
                    objModel.RecommendedGiveaway = long.Parse(collection["RecommendedGiveaway"].ToString());
                    objModel.HSABid = long.Parse(collection["HSABid"].ToString());
                    objModel.ExactPPCBid = long.Parse(collection["ExactPPCBid"].ToString());
                    objModel.BroadPPCBid = long.Parse(collection["BroadPPCBid"].ToString());
                    objModel.EaseToRank = long.Parse(collection["EaseToRank"].ToString());
                    objModel.RelevancyScore = long.Parse(collection["RelevancyScore"].ToString());
                    objModel.Operation = "insert";
                    SaveKeyword(objModel);
                    ListKeyword = GetKeyword("", 0);
                    return RedirectToAction("KeywordList", ListKeyword);
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }
            return View();
        }
        private void SaveKeyword(KeywordResearchModel objModel)
        {
            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objModel));
            // TODO: Add insert logic here
            var _response = Services.GetApiResponse(Constant.ApiSaveKeyword, "POST", _request);
            if (_response == null) return;
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
        }
        // GET: KeywordResearch/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: KeywordResearch/Edit/5
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

        // GET: KeywordResearch/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: KeywordResearch/Delete/5
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
        [Route("KeywordResearch/SerchKeyword")]
        public ActionResult GetKeywordAjx(FormCollection collection)
        {
            var ProductCategoryList = GetListCategory();
            String searchText = collection["txtSearch"];
            long categoryid = long.Parse(collection["category"]);
            KeywordResearchModel objModel = new KeywordResearchModel()
            {
                KeyWord = searchText,
                CategoryId = categoryid
            };

            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objModel));
            // TODO: Add insert logic here
            var _response = Services.GetApiResponse(Constant.ApiGetkeyword, "POST", _request);
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
                        List<KeywordResearchModel> listKeyword = JsonConvert.DeserializeObject<List<KeywordResearchModel>>(_data["Response"].ToString());
                        ViewBag.ProductCategoryList = new SelectList(ProductCategoryList, "Id", "Category");

                        return View("KeywordList", listKeyword);
                    }
                }
                ViewBag.ProductCategoryList = new SelectList(ProductCategoryList, "Id", "Category");

                return View();
            }
        }
        public List<KeywordResearchModel> GetKeyword(string searchText, int categoryid)
        {
            KeywordResearchModel objModel = new KeywordResearchModel()
            {

                KeyWord = searchText,
                CategoryId = categoryid
            };

            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objModel));
            // TODO: Add insert logic here
            var _response = Services.GetApiResponse(Constant.ApiGetkeyword, "POST", _request);
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
                        return JsonConvert.DeserializeObject<List<KeywordResearchModel>>(_data["Response"].ToString());
                    }
                }
                return new List<KeywordResearchModel>();
            }
        }

        [HttpPost]
        [Route("KeywordUpload")]
        public ActionResult KeywordUpload(KeywordResearchModel objKeywordModle,FormCollection frmColl)
        {
            var postedFile = Request.Files["FileUploadKeyword"];
            HttpPostedFileBase FileUpload = postedFile ;
            //HttpPostAttribute file = frmColl["FileUploadKeyword"].All<KeywordResearchModel>;
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {


                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<KeywordResearchModel>(sheetName) select a;

                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            if (a.KeyWord != "" && a.ExactMatchSearchVolume != 0 && a.BroadMatchSearchVolume != 0)
                            {
                                KeywordResearchModel objKwdR = new KeywordResearchModel();
                                objKwdR.KeyWord = a.KeyWord;
                                objKwdR.ExactMatchSearchVolume = a.ExactMatchSearchVolume;
                                objKwdR.BroadMatchSearchVolume = a.BroadMatchSearchVolume;
                                objKwdR.CategoryId = a.BroadMatchSearchVolume;
                                objKwdR.RecommendedGiveaway = a.BroadMatchSearchVolume;
                                objKwdR.HSABid = a.BroadMatchSearchVolume;
                                objKwdR.ExactPPCBid = a.BroadMatchSearchVolume;
                                objKwdR.BroadPPCBid = a.BroadMatchSearchVolume;
                                objKwdR.EaseToRank = a.BroadMatchSearchVolume;
                                objKwdR.RelevancyScore = a.BroadMatchSearchVolume;
                                objKwdR.Operation = "insert";

                                SaveKeyword(objKwdR);
                                ViewBag.Message = "Keyword Upload";
                                //.Users.Add(TU);

                                //db.SaveChanges();



                            }
                            else
                            {
                                #region Comment
                                //data.Add("<ul>");
                                //if (a.Name == "" || a.Name == null) data.Add("<li> name is required</li>");
                                //if (a.Address == "" || a.Address == null) data.Add("<li> Address is required</li>");
                                //if (a.ContactNo == "" || a.ContactNo == null) data.Add("<li>ContactNo is required</li>");

                                //data.Add("</ul>");
                                //data.ToArray();
                                //return Json(data, JsonRequestBehavior.AllowGet);
                                #endregion
                                ViewBag.Message = "Excel File Columns Blank";
                            }
                        }

                        catch (DbEntityValidationException ex)
                        {
                            #region Comment EF Db Exception 
                            //foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            //{

                            //    foreach (var validationError in entityValidationErrors.ValidationErrors)
                            //    {

                            //        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                            //    }

                            //}
                            #endregion
                        }
                    }
                    return View("KeywordAdd");

                    // ListKeyword = GetKeyword("", 0);
                    //return RedirectToAction("KeywordAdd", "KeywordResearch");
                    ////deleting excel file from folder  
                    //if ((System.IO.File.Exists(pathToExcelFile)))
                    //{
                    //    System.IO.File.Delete(pathToExcelFile);
                    //}
                    //return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    #region comment Check File Formate 
                    //alert message for invalid file format  
                    //data.Add("<ul>");
                    //data.Add("<li>Only Excel file format is allowed</li>");
                    //data.Add("</ul>");
                    //data.ToArray();
                    //return Json(data, JsonRequestBehavior.AllowGet);
                    #endregion
                    ViewBag.Message = "Only Excel file format is allowed";
                    return View("KeywordAdd");
                }
            }
            else
            {
                #region Choose File Validation
                //data.Add("<ul>");
                //if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                //data.Add("</ul>");
                //data.ToArray();
                //return Json(data, JsonRequestBehavior.AllowGet);
                #endregion
                ViewBag.Message = "Please choose Excel file";
                return View("KeywordAdd");
            }

           // return View();
        }

        public FileResult DownloadExcel()
        {
            string path = "/FormateExcel/sampleKeyword.xlsx";
            return File(path, "application/vnd.ms-excel", "Sample.xlsx");
        }
    }
}
