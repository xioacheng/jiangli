using jiangli.Models.Constants;
using jiangli.Respository.Wx;
using jiangli.Util;
using jiangli.UI.Models;
using jiangli.UI.Models.WX;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
namespace jiangli.Controllers
{
    public class CaseAPIController : ApiController
    {
        private ILog Ilog = LogHelper.GetInstance();
        private Rcase cases = Rcase.Current;

        // GET: api/CaseAPI
        public IEnumerable<string> Get()
        {
            string url = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            return new string[] { "value1", url };

        }


        /// <summary>
        /// 通过页数和状态获取案件
        /// </summary>
        /// <param name="page"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WxCaseListItemViewModel> GetPieceByPageAndState(int page, CaseState state)
        {
            return cases.GetPieceByPagesAndState(page, state);
        }
        /// <summary>
        /// 通过案件Id获取案件的一系列信息
        /// </summary>
        /// <param name="caseId"></param>
        /// <returns></returns>
        [HttpGet]
        public WxCaseDetailViewModel GetCaseDetailById(int caseId)
        {
            return cases.GetCaseDetailById(caseId);
        }
        [HttpGet]
        public IEnumerable<BasicCaseViewModel> GetPieceBySuggest(string q, CaseState state)
        {
            return cases.GetPieceBySuggest(q, state);
        }
        /// <summary>
        /// 通过关键词查找案件列表
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="q"></param>
        /// <param name="page"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WxCaseListItemViewModel> GetPieceByKeyWord(int limit, string q, int page, CaseState state)
        {
            return cases.GetPieceByPageAndWordAndState(q, state, page);
        }
        [HttpGet]
        public bool CreateCaseGet()
        {
            WxPostCaseViewModel newcase = new WxPostCaseViewModel
            {
                id = 1,
                accuser = "achang",
                accuserid = 1,
                title = "achangis title",
                description = "sdfasfdasdfasdf",
                start_at = DateTime.Now.ToString(),
                respondent = "haibin",
                originalpay = 1234.4,
                respondentid = 2,
            };
            this.CreateCase(newcase);
            return true;
        }
        /// <summary>
        /// 创建一个案件
        /// </summary>
        /// <param name="newCase"></param>
        /// <returns></returns>
        [HttpPost]
        public bool CreateCase(WxPostCaseViewModel newCase)
        {
            cases.Create(newCase);
            return true;
        }
        /// <summary>
        /// 修改一个案件
        /// </summary>
        /// <param name="newCase"></param>
        /// <returns></returns>
        public HttpResponseMessage ModifiedCase(WxPostCaseViewModel newCase)
        {
            if (newCase.id == 0)
            {
                Ilog.Debug("新建案件：案件id不可等于0");
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            cases.Modified(newCase);
            return new HttpResponseMessage(HttpStatusCode.OK);

        }
        /// <summary>
        /// 获取某用户发起的所有案件
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WxCaseListItemViewModel> GetCaseListOfOwner(int userid)
        {
            return cases.getCaseListOfOwner(userid);
        }
        /// <summary>
        /// 修改案件--带有图片的
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> ModifiedCaseWithImage()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            /* root是服务器的根目录 */
            string root = HttpContext.Current.Server.MapPath(GlobalConfig.imgPath);
            /* serverUrl是网站的根Url */
            string serverUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            /* 服务器的物理存储位置 */
            string realpath = "";
            /* 文件的名称 */
            string realfilename = "";
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                var model = _getPostCaseFromFormdata(provider.FormData);
                /* 将原图片重置 */
                if (File.Exists(model.imageurl))
                {
                    File.Delete(model.imageurl);
                }
                /* 获取得到的图片文件--只能一个 */
                foreach (MultipartFileData file in provider.FileData)
                {
                    realfilename = DateTime.Now.ToFileTime().ToString() + Util.UtilFileType.switchType(file.Headers.ContentType.ToString());
                    realpath = root + "\\" + realfilename;

                    Ilog.Debug("图片位置的实际路径:" + realpath);
                    if (File.Exists(realpath))
                    {
                        File.Delete(realpath);
                    }

                    File.Move(file.LocalFileName, realpath);
                }

                string urlpath = serverUrl + GlobalConfig.imgPath + "/" + realfilename;
                /* 修改新图片的路径 */
                model.imageurl = urlpath;
                /* 修改数据库中的案件 */
                cases.Modified(model);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Ilog.Error(e.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        /// <summary>
        /// 上传案件信息带有图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> CreateCaseWithImage()
        {
            // 检查该请求是否含有multipart/form-data
            if (!Request.Content.IsMimeMultipartContent())
            {
                Ilog.Error("不符合Multipartcontent格式");
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath(GlobalConfig.imgPath);
            /* serverUrl是网站的根Url */
            string serverUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            /* 服务器的物理存储位置 */
            string realpath = "";
            /* 文件的名称 */
            string realfilename = "";
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                // 以下描述如何获取文件名
                foreach (MultipartFileData file in provider.FileData)
                {
                    realfilename = DateTime.Now.ToFileTime().ToString() + Util.UtilFileType.switchType(file.Headers.ContentType.ToString());
                    realpath = root + "\\" + realfilename;

                    if (File.Exists(realpath))
                    {
                        File.Delete(realpath);
                    }

                    File.Move(file.LocalFileName, realpath);
                }
                var content = provider.FormData;
                /* 获取formdata中的数据 并创建新的案件*/
                var model = _getPostCaseFromFormdata(content);

                /* 获取图片的url路径 */
                string urlpath =  serverUrl + GlobalConfig.imgPath + "/" + realfilename;

                model.imageurl = urlpath;
                cases.Create(model);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                Ilog.Error("信息失误:" + e.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// 创建一个评论
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public WxCommentViewModel CreateComment(WxCommentViewModel model)
        {
            return Rcomment.Current.Create(model);
        }
        /// <summary>
        /// 通过案件Id获取该案件下所有的评论
        ///         -- 分页处理
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WxCommentViewModel> GetCommentsByCaseId(int cid, int page)
        {
            return Rcomment.Current.GetCommentsByCaseId(cid, page);
        }
        /// <summary>
        /// 修改案件的申诉过程信息
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpGet]
        public bool SetAppeal(int cid, string msg)
        {
            cases.setAppealMsg(cid, msg);
            return true;
        }
        /// <summary>
        /// 修改案件的状态 - 针对微信不开放
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="state"></param>
        [HttpGet]
        private bool SetState(int cid, CaseState state)
        {
            cases.ModifiedState(cid, state);
            return true;
        }
        /// <summary>
        /// 创建一个裁决
        /// </summary>
        [HttpPost]
        public bool CreateInvolve(WxPostInvolveViewModel model)
        {
            Rpart.Current.CreateInvolve(model);
            return true;
        }
        /// <summary>
        /// 修改一个裁决
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public bool ModifiedInvolve(WxPostInvolveViewModel model)
        {
            model.id = Rpart.Current.GetPartIdByCaseAndUser(model.caseid, model.userid);
            Rpart.Current.ModifiedInvolve(model);
            return true;
        }


        #region 非对外接口方法
        /// <summary>
        /// 在Formdata中获取PostCaseViewmodel
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [NonAction]
        private WxPostCaseViewModel _getPostCaseFromFormdata(System.Collections.Specialized.NameValueCollection content)
        {
            return new WxPostCaseViewModel()
            {
                id = Int32.Parse(content.Get("id") != null ? content.Get("id") : "0"),
                accuser = content.Get("accuser"),
                accuserid = Int32.Parse(content.Get("accuserid")),
                respondent = content.Get("respondent"),
                respondentid = Int32.Parse(content.Get("respondentid")),
                description = content.Get("description"),
                issuer = content.Get("accuser"),
                originalpay = Double.Parse(content.Get("originalpay")),
                title = content.Get("title"),
                state = (CaseState)Enum.Parse(typeof(CaseState), content.Get("state")),
            };
        }
        #endregion
    }
}
