using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreWeb.MVC6.DataContext;
using AspnetCoreWeb.MVC6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetCoreWeb.MVC6.Controllers
{
    public class NoteController : Controller
    {
        /// <summary>
        /// 게시판 리스트
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null) // 세션이 없으면
            {
                return RedirectToAction("Login", "Account"); // 로그인폼 이동 RedirecToAction("액션","컨트롤러")
            }
            using (var db = new DatabaseContext()) // DB를 사용해야하므로
            {
                var list = db.Notes.ToList(); // db의 Notes 테이블의 리스트를 모두 가져온다. (ToList())
                return View(list);
            }
        }

        /// <summary>
        /// 게시물 추가
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null) // 세션이 없으면
            {
                return RedirectToAction("Login", "Account"); // 로그인폼 이동 RedirecToAction("액션","컨트롤러")
            }
            return View();
        }

        [HttpPost]
        public IActionResult Add(Note model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null) // 세션이 없으면
            {
                return RedirectToAction("Login", "Account"); // 로그인폼 이동 RedirecToAction("액션","컨트롤러")
            }
            model.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString()); // null값인지도 모르는 값을 string으로 변환하고 int형으로 바꿔준다.

            if (ModelState.IsValid) // 사용자 정보 입력 부분에 모두 입력을 했는지 확인하기 위한 부분
            {
                using (var db = new DatabaseContext()) // using을 사용한 이유는 C# 전용이며 using문을 빠져나가면 자동으로 Close된다.
                {
                    db.Notes.Add(model); // Note의 정보의 데이터를 추가 (메모리상에만 올라감)

                    if (db.SaveChanges() > 0) // 실제 데이터베이스에 commit부분 데이터가 올라가면 1이므로 True
                    {
                        return Redirect("Index"); // 게시물을 올리면 다시 리스트로 돌아간다.
                        // 동일한 컨트롤러에서 처리는 이렇게 처리해준다. 그게아니면 Redirect("액션","컨트롤러")
                        // Redirect("Index","Note")와 같음
                    }
                }
                ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다."); // if문이 false면 에러 메세지 출력
            }
            return View(model);
        }

        /// <summary>
        /// 게시물 수정
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null) // 세션이 없으면
            {
                return RedirectToAction("Login", "Account"); // 로그인폼 이동 RedirecToAction("액션","컨트롤러")
            }
            return View();
        }

        /// <summary>
        /// 게시물 삭제
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null) // 세션이 없으면
            {
                return RedirectToAction("Login", "Account"); // 로그인폼 이동 RedirecToAction("액션","컨트롤러")
            }
            return View();
        }
    }
}
