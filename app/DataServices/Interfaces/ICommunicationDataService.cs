﻿using scheapp.app.Models.Data.TableModels.Communication;

namespace scheapp.app.DataServices.Interfaces
{
    public interface ICommunicationDataService
    {
        Task<List<CustomerCall>> GetCustomerCalls();
        Task<List<ProfessionalCall>> GetProfessionalCalls();
    }
}