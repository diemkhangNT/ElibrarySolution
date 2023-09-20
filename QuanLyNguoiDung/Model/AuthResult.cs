namespace QuanLyNguoiDung.Model
{
    public class AuthResult
    {
        public string Message { get; set; }
        public bool Result { get; set; }
        public object data { get; set; } = new List<string>();
    }
}
