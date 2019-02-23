using AspnetCoreWeb.MVC6.DataContext;
using AspnetCoreWeb.MVC6.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetCoreWeb.MVC6.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 로그인
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 회원 가입
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if(ModelState.IsValid) // 사용자 정보 입력 부분에 모두 입력을 했는지 확인하기 위한 부분
            {                      // Required부분 not null인지 null인지 여부 확인
                using (var db = new DatabaseContext()) // using을 사용한 이유는 C# 전용이며 using문을 빠져나가면
                {                                      // 자동으로 Close된다.
                    db.Users.Add(model); // User의 정보의 데이터를 추가 (메모리상에만 올라감)
                    db.SaveChanges(); // 실제로 데이터베이스에 올리는 부분
                }
                return RedirectToAction("Index", "Home"); // 다시 돌아가는 부분으로 (액션,컨트롤러)
            }
            return View(model);
        }
    }
}

