using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI
{
    public enum ErrorCodes
    {
        [Description("Нет ошибки")]
        NoError = 0,

        [Description("Неверный авторизационный токен")]
        IncorrectToken = 1,

        [Description("Истёк срок действия токена")]
        TokenExpired = 2,

        [Description("Авторизационный токен не предоставлен")]
        NoAuthInfo = 3,

        [Description("Общая ошибка выполнения на сервере")]
        ServerError = 4,

        [Description("Недостаточно прав на выполнение")]
        NoRights = 4,
    }
}
