using Shoe_restoration_project.Models;
using System.Runtime.InteropServices.ComTypes;

namespace Shoe_restoration_project.DTO
{
    public interface IDtoObject<TModel>
    {
        public TModel GetModel();
        public static abstract IDtoObject<TModel> FromModel(TModel model);
    }
}
