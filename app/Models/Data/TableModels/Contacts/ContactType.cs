﻿using System.ComponentModel.DataAnnotations;

namespace scheapp.app.Models.Data.TableModels.Contacts
{
    public class ContactType
    {

        public int Id { get; set; }
        public string Type { get; set; } = "";
    }
}
