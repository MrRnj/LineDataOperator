using System;
namespace Inter_face.Models
{
    public interface IDataModel
    {
        float LengthProperty { get; set; }
        float PositionProperty { get; set; }
        DataType Type { get; set; }
        bool SelectedProperty { get; set; }
        int SectionNumProperty { get; set; }
        int ScaleProperty { get; set; }
        string HatProperty { get; set; }
        string PathDataProperty { get; set; }
        IDataModel Self { get; }
        string ChangeData(float oriheight, float oriposition, float length, float angle);
    }
}
