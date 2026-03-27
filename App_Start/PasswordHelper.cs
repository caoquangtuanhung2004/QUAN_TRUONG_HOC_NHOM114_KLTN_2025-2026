using System;
using System.Security.Cryptography;

namespace demomvc.App_Start
{
    public static class PasswordHelper
    {
        

        private const int SaltSize = 16;     // Salt 128-bit
        private const int KeySize = 32;      // Hash 256-bit
        private const int Iterations = 10000; // Số vòng PBKDF2

        /// HASH mật khẩu để lưu Database
   
        public static string HashPassword(string password)
        {
            // PBKDF2 + SHA256 (chuẩn Microsoft)
            using (var algorithm = new Rfc2898DeriveBytes(
                password,                   // mật khẩu gốc
                SaltSize,                   // sinh salt ngẫu nhiên
                Iterations,                 // số vòng lặp
                HashAlgorithmName.SHA256))  // thuật toán hash
            {
                // Sinh hash
                var key = Convert.ToBase64String(
                    algorithm.GetBytes(KeySize));

                // Lấy salt đã sinh
                var salt = Convert.ToBase64String(
                    algorithm.Salt);

                // Format lưu DB:
                // Iterations.Salt.Hash
                return $"{Iterations}.{salt}.{key}";
            }
        }

       
        /// KIỂM TRA mật khẩu nhập vào có đúng không
       
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Tách dữ liệu đã lưu
            var parts = hashedPassword.Split('.');

            if (parts.Length != 3)
                return false; // sai format → fail ngay

            int iterations = int.Parse(parts[0]);
            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] key = Convert.FromBase64String(parts[2]);

            // Hash lại mật khẩu người dùng nhập
            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256))
            {
                byte[] keyToCheck = algorithm.GetBytes(KeySize);

                // So sánh an toàn (constant-time)
                return SlowEquals(key, keyToCheck);
            }
        }

       
        /// So sánh byte[] KHÔNG bị timing attack
       
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            // XOR độ dài → khác là fail
            uint diff = (uint)a.Length ^ (uint)b.Length;

            // So từng byte (không break sớm)
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }

            // diff == 0 → giống nhau
            return diff == 0;
        }
    }
}
