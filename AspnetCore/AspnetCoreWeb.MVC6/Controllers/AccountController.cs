using AspnetCoreWeb.MVC6.DataContext;
using AspnetCoreWeb.MVC6.Models;
using AspnetCoreWeb.MVC6.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetCoreWeb.MVC6.Controllers
{
    public class AccountController : Controller
    {
        public const string SessionKey = "USER_LOGIN_KEY";
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
        /// 로그인 전송
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginView model)
        {
            if(ModelState.IsValid) // 값이 정상적으로 입력이 되었을 경우에만 실행
            {
                using (var db = new DatabaseContext()) // 데이터베이스를 열고 닫겠다 선언
                {
                    // Linq - 메서드 체이닝
                    // => : A Go to B
                    // Users.FirstOrDefault는 Users에서 첫번째 혹은 기본값을 출력하겠다라는 의미
                    // Id값과 Password를 확인 하기 위해서 난바식을 써서 다음과 같이 표현해준다. 
                    // ex) 어떤데이터 => 어떤데이터와 실제데이터 비교하고 일치 여부 확인

                    // var user = db.Users.FirstOrDefault(u => u.UserId == model.UserId && u.UserPassword == model.UserPassword);
                    var user = db.Users.FirstOrDefault(u => u.UserId.Equals(model.UserId) && 
                                                            u.UserPassword.Equals(model.UserPassword));
                    if(user != null) // user 데이터가 없을 경우 (로그인 실패)
                    {
                        HttpContext.Session.SetInt32(SessionKey, user.UserNo);
                        return RedirectToAction("LoginSuccess", "Home"); // HomeController에 LoginSuccess로 이동
                    }
                }
                ModelState.AddModelError(string.Empty, "사용자 정보가 일치하지 않습니다."); // user 데이터가 있을 경우 페이지 이동 (로그인 성공)
                                                                                          // 별도의 key값이 필요 없으므로 string.Empty해준다.
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKey); // 지정해준 세션 Key를 입력 (세션 삭제)
            //HttpContext.Session.Clear(); // 관리자가 메모리가 너무 많이차서 초기화 할때 사용한다. (관리자 명령어)
            return RedirectToAction("Index", "Home"); // 로그아웃하면 홈화면으로 이동
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

        /// <summary>
        /// 회원 가입 전송
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

