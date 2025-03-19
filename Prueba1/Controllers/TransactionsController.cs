using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba1.Models;

namespace Prueba1.Controllers
{
    public class TransactionsController : Controller
    {
       
        // GET: TransactionsController
        public UserModel GetSessionInfo()
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("userSession")))
                {
                    UserModel? user = JsonConvert.DeserializeObject<UserModel>(HttpContext.Session.GetString("userSession"));

                    return user;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public ActionResult Index()
        {
            UserModel? user = GetSessionInfo();

            if (user != null)
            {
                ViewBag.User = user;
                List<TransactionsModel> transactionsList = TransactionsHelper.GetTransactions().Result;
                ViewBag.Transactions = transactionsList;
                HttpContext.Session.SetString("transactionList", JsonConvert.SerializeObject(transactionsList));
                return View();
            }

            return RedirectToAction("Index", "Error");
        }

        public ActionResult Create()
        {
            UserModel? user = GetSessionInfo();

            if (user != null)
            {
                ViewBag.User = user;
                return View();
            }

            return RedirectToAction("Index", "Error");
        }

        public ActionResult Balance()
        {
            UserModel? user = GetSessionInfo();

            if (user != null)
            {
                ViewBag.User = user;
                return View();
            }

            return RedirectToAction("Index", "Error");
        }

        public ActionResult createTransaction(string emailToUser, int txtAmount)
        {
            UserModel? user = GetSessionInfo();
            UserHelper userHelper = new UserHelper();
            if (user != null)
            {
                if (txtAmount < user.Money)
                {
                    List<UserModel> usersList = userHelper.getAllUsers().Result;
                    foreach (var userData in usersList)
                    {
                        if (emailToUser.Equals(userData.Email))
                        {
                            if (emailToUser.Equals(user.Email))
                            {
                                return RedirectToAction("Index", "Error");
                            }
                            else
                            {
                                string email = user.Email;
                                string name = user.Name;
                                int newAmount = user.Money - txtAmount;
                                int QuantitySent = userData.Money + txtAmount;

                                TransactionsHelper transactionsHelper = new TransactionsHelper();

                                
                                bool result = transactionsHelper.RegisterTransaction(new TransactionsModel
                                {
                                    FromUser = user.Email,
                                    ToUser = emailToUser,
                                    Amount = txtAmount,
                                    Date = DateTime.Now.ToString(),
                                }).Result;

                                bool result_2 = userHelper.UpdateUserInfo(new UserModel
                                {
                                    Id = user.Id,
                                    Email = email,
                                    Name = name,
                                    Money = newAmount,
                                }).Result;

                                
                                bool result_3 = userHelper.MakeTransaction(new UserModel
                                {
                                    Id = userData.Id,
                                    Email = emailToUser,
                                    Name = userData.Name,
                                    Money = QuantitySent,
                                }).Result;

                                return RedirectToAction("Index");


                            }

                        }
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Error");
                }
            }

            return RedirectToAction("Index", "Error");
        }


        // GET: TransactionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransactionsController/Create
     

        // POST: TransactionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransactionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransactionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
