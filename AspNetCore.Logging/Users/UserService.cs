using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Logging.Users
{
    public class UserService
    {
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        private readonly List<string> _users = new();


        public void Register(string userName)
        {
            // Başlangıç log'u
            _logger.LogInformation(new EventId(1000, "StartRegister"), "Kayıt İşlemi Başladı");

            // Kayıt işlemi sürecini simüle etme (Thread.Sleep yerine asenkron işlem önerilir)
            SimulateProgress(30); // %30
            SimulateProgress(50); // %50
            SimulateProgress(80); // %80
            SimulateProgress(99); // %99

            // Kullanıcı zaten var mı kontrol et
            if (_users.Contains(userName))
            {
                _logger.LogWarning(new EventId(2000, "DuplicateUser"), "Kullanıcı zaten mevcut: {UserName}", userName);
                return;
            }

            try
            {
                // Yeni kullanıcı ekleme
                _users.Add(userName);
                _logger.LogInformation(new EventId(1001, "UserAdded"), "Kullanıcı başarıyla eklendi: {UserName}", userName);
            }
            catch (Exception ex)
            {
                // Hata log'u
                _logger.LogError(new EventId(3000, "RegisterError"), ex, "Kayıt esnasında bir hata oluştu: {UserName}", userName);
            }
        }

        // Kayıt işlemi sırasında ilerleme simülasyonu
        private void SimulateProgress(int progressPercentage)
        {
            _logger.LogInformation(new EventId(1000, "StartRegister"), "Kayıt İşlemi işleniyor %{ProgressPercentage}..", progressPercentage);
            Thread.Sleep(1000); 
        }



    }
}
