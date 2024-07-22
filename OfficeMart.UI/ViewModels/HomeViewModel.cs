using System;
using System.Collections.Generic;
using OfficeMart.Domain.Models.Entities;
using OfficeMart.UI.Models.API;

namespace OfficeMart.UI
{
	public class HomeViewModel
	{
		public List<Models.API.Category> categories { get; set; }
        public List<Slider> sliders { get; set; }
	}
}

