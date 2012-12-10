using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using muagicungban.Entities;

namespace muagicungban.Abstract
{
    public interface IShipCountryRepository
    {
        IQueryable<ShipCountry> ShipCountries { get; }
        void SaveCountry(ShipCountry shipCountry);
        ShipCountry FetchByID(string ID);
        void DeleteCountry(ShipCountry shipCountry);
    }
}
