﻿using Application.Exceptions;
using Core;
using System.Text;

namespace Application
{
	public class FundaRelativeUrlBuilder : IFundaRelativeUrlBuilder
    {
        private Tuin? tuin;
        private string? status;

        public IFundaRelativeUrlBuilder WithTuin(Tuin? tuin)
        {
            this.tuin = tuin;
            return this;
        }

		public IFundaRelativeUrlBuilder WithStatus(string? status)
		{
			this.status = status;
			return this;
		}

		public string Build(string key, string aanbodType, List<string> locations, int page, int pageSize)
        {
            if(locations == null || !locations.Any() || string.IsNullOrWhiteSpace(aanbodType) || page < 0 || pageSize < 0 || string.IsNullOrWhiteSpace(key))
            {
                throw new RequiredDataMissing();
            }

            var urlStringBuilder = new StringBuilder();
			urlStringBuilder.Append($"/json");
			urlStringBuilder.Append($"/{key}");
			urlStringBuilder.Append($"?type={aanbodType}");
			urlStringBuilder.Append($"&zo=");
			urlStringBuilder.Append($"/{string.Join(",", locations)}");

			if (!string.IsNullOrWhiteSpace(status))
			{
				urlStringBuilder.Append($"/{status}");
			}

			if (tuin != null)
            {
                urlStringBuilder.Append($"/tuin");
            }

			urlStringBuilder.Append("/");
			urlStringBuilder.Append($"&page={page}");
			urlStringBuilder.Append($"&pagesize={pageSize}");
            return urlStringBuilder.ToString();
        }
    }
}
