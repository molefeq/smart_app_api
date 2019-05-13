using SmartData.Data.ViewModels;
using SmartData.DataAccess;
using SmartData.Payfast;
using SmartData.Payfast.Models;
using SmartData.Service.DataMappers;

using System;

namespace SmartData.Service.Payment
{
    public class PayFastPaymentService : IPaymentService
    {
        private IPayFastService payFastService;
        private OnceOffPaymentMapper onceOffPaymentMapper;
        private PaymentDetailMapper paymentDetailMapper;
        private IUnitOfWork unitOfWork;

        public PayFastPaymentService(IPayFastService payFastService,
                                     OnceOffPaymentMapper onceOffPaymentMapper,
                                     PaymentDetailMapper paymentDetailMapper,
                                     IUnitOfWork unitOfWork)
        {
            this.payFastService = payFastService;
            this.onceOffPaymentMapper = onceOffPaymentMapper;
            this.paymentDetailMapper = paymentDetailMapper;
            this.unitOfWork = unitOfWork;
        }

        public OnceOffPaymentResponse GetOnceOffPaymentUrl(BuyDataModel buyDataModel, PayFastSettings payFastSettings)
        {
            var paymentDetail = paymentDetailMapper.MapToPaymentDetail(buyDataModel);

            unitOfWork.PaymentDetail.Insert(paymentDetail);
            unitOfWork.Save();

            var onceOffPaymentModel = onceOffPaymentMapper.MapToOnceOffPaymentModel(paymentDetail, buyDataModel, payFastSettings);
            
            return new OnceOffPaymentResponse
            {
                Url = payFastService.CreateOncePaymentUrl(onceOffPaymentModel),
                PaymentId = paymentDetail.PaymentId
            };
        }

        public void FailOnceOffPayment(Guid paymentId)
        {
            var paymentDetail = unitOfWork.PaymentDetail.GetById(item => item.PaymentId == paymentId);

            paymentDetail.IsPaymentSuccessful = false;

            unitOfWork.PaymentDetail.Update(paymentDetail);
            unitOfWork.Save();
        }
    }
}
