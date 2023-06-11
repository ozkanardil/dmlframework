using DmlFramework.Application.Features.Category.Queries;
using DmlFramework.Infrastructure.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmlFramework.Application.Features.Category.Rules
{
    public static class GetCategoryQueryGuard
    {
        public static GetCategoryQueryClause Against(GetCategoryQuery request)
        {
            return new GetCategoryQueryClause(request);
        }
        public class GetCategoryQueryClause
        {
            private readonly GetCategoryQuery _category;
            public GetCategoryQueryClause(GetCategoryQuery category)
            {
                _category = category;
            }

            public GetCategoryQueryClause MustBePositive()
            {
                if (_category.categoryId < 1)
                    throw new BusinessException("Yahu ne biçim iş la bu");

                return this;
            }
        }
    }

}
