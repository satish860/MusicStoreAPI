namespace MusicStore.Api
{
    public class CategoryCountViewModel
    {
        public string CategoryName { get; set; }

        public int Count { get; set; }

        public string ProductUrl { get; set; }


        public override bool Equals(object obj)
        {
            CategoryCountViewModel CompareModel = obj as CategoryCountViewModel;

            return string.Compare(this.CategoryName, CompareModel.CategoryName) == 0;
        }

        public override int GetHashCode()
        {
            return this.CategoryName.GetHashCode();
        }
    }
}