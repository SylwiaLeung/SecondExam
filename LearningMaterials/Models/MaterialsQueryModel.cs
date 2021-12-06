namespace LearningMaterials.Models
{
    public class MaterialsQueryModel
    {
        public string SearchPhrase { get; set; }
        public bool SortByDate { get; set; } = false;
    }
}
