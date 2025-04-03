using wsmcbl.src.model.academy;

namespace wsmcbl.src.controller.service.sheet;

public class EvaluationStatisticsByTeacherSheetBuilder : SheetBuilder
{
    private int partial { get; set; }
    private string schoolyear { get; set; } = null!;
    private string enrollmentLabel { get; set; } = null!;
    private string userAlias { get; set; } = null!;
    private TeacherEntity teacher { get; set; } = null!;

    private List<model.secretary.SubjectEntity> subjectList { get; set; } = null!;
    private List<SubjectPartialEntity> subjectPartialList { get; set; } = null!;
    
    protected override void setColumnQuantity()
    {
        throw new NotImplementedException();
    }

    public override byte[] getSpreadSheet()
    {
        throw new NotImplementedException();
    }
    

    public class Builder
    {
        private readonly EvaluationStatisticsByTeacherSheetBuilder sheetBuilder;

        public Builder()
        {
            sheetBuilder = new EvaluationStatisticsByTeacherSheetBuilder();
        }

        public EvaluationStatisticsByTeacherSheetBuilder build() => sheetBuilder;
        
        public Builder withPartial(int parameter)
        {
            sheetBuilder.partial = parameter;
            return this;
        }
        
        public Builder withSchoolyear(string parameter)
        {
            sheetBuilder.schoolyear = parameter;
            return this;
        }
        
        public Builder withTeacher(TeacherEntity parameter)
        {
            sheetBuilder.teacher = parameter;
            return this;
        }
        
        public Builder withUserAlias(string parameter)
        {
            sheetBuilder.userAlias = parameter;
            return this;
        }
        
        public Builder withEnrollment(string parameter)
        {
            sheetBuilder.enrollmentLabel = parameter;
            return this;
        }
        
        public Builder withSubjectList(List<SubjectEntity> parameter)
        {
            sheetBuilder.subjectList = parameter.Select(e => e.secretarySubject!).ToList();
            sheetBuilder.setColumnQuantity();
            return this;
        }
        
        public Builder withSubjectPartialList(List<SubjectPartialEntity> parameter)
        {
            sheetBuilder.subjectPartialList = parameter;
            return this;
        }
    }
}