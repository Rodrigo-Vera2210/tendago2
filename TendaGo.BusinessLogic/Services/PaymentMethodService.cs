using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;

namespace TendaGo.BusinessLogic.Services
{
    public class PaymentMethodService
    {
        public List<PaymentMethodDto> GetPaymentMethods()
        {
            var medioPagoFindParameter = new ER.BE.MedioPagoFindParameterEntity { IdEstado = 1 };
            var paymentMethods = ER.BA.MedioPagoCollectionBussinesAction.FindByAll(medioPagoFindParameter);
            var paymentMethodsDto = paymentMethods.Select(py => py.GlobalMapperConverter<ER.BE.MedioPagoEntity, PaymentMethodDto>()).ToList();
            return paymentMethodsDto;
        }
    }
}
