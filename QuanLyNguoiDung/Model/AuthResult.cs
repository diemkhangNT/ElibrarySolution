namespace QuanLyNguoiDung.Model
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool Result { get; set; }
        public List<string> Message { get; set; } = new List<string>();
    }
}
