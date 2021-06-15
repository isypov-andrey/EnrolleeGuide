﻿using Entities;
using Prism.Mvvm;

namespace EnrolleeGuide.Models
{
    public class AddressModel : BindableBase
    {
        public int Id { get; set; }

        private string _fullAddress;

        public string FullAddress
        {
            get
            {
                return _fullAddress;
            }
            set
            {
                SetProperty(ref _fullAddress, value);
            }
        }

        private string _phone;

        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                SetProperty(ref _phone, value);
            }
        }

        public static AddressModel GetFromDomain(Address address)
        {
            if (address == null)
            {
                return null;
            }

            return new AddressModel
            {
                Id = address.Id,
                FullAddress = address.FullAddress,
                Phone = address.Phone
            };
        }

        public Address ToDomain()
        {
            return new Address
            {
                Id = Id,
                FullAddress = FullAddress,
                Phone = Phone
            };
        }
    }
}
