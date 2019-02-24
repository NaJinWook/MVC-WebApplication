using System.ComponentModel.DataAnnotations;

namespace AspnetCoreWeb.MVC6.ViewModel
{
    public class LoginView // 로그인 할때만 사용하는 Model
    {
        [Required(ErrorMessage = "사용자 ID를 입력하세요.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "사용자 비밀번호를 입력하세요.")]
        public string UserPassword { get; set; }
    }
}

