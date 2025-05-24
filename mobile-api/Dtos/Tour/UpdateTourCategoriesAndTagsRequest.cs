using System.Collections.Generic;

namespace mobile_api.Dtos.Tour
{
    public class UpdateTourCategoriesAndTagsRequest
    {
        public List<int> CategoryIds { get; set; }
        public List<int> TagIds { get; set; }
    }
}
