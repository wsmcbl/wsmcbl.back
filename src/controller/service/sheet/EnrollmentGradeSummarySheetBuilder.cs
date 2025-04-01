using wsmcbl.src.model.academy;
using wsmcbl.src.model.config;

namespace wsmcbl.src.controller.service.sheet;

public class EnrollmentGradeSummarySheetBuilder
{
    private UserEntity user { get; set; } = null!;
    private EnrollmentEntity enrollment { get; set; } = null!;
    private List<StudentEntity> studentList { get; set; } = null!;

    public byte[] getSpreadSheet()
    {
        return [];
    }
    
    public class Builder
    {
        private readonly EnrollmentGradeSummarySheetBuilder sheetBuilder;

        public Builder()
        {
            sheetBuilder = new EnrollmentGradeSummarySheetBuilder();
        }

        public EnrollmentGradeSummarySheetBuilder build() => sheetBuilder;
        
        public Builder withUser(UserEntity parameter)
        {
            sheetBuilder.user = parameter;
            return this;
        }
        
        public Builder withEnrollment(EnrollmentEntity parameter)
        {
            sheetBuilder.enrollment = parameter;
            return this;
        }
        
        public Builder withStudentList(List<StudentEntity> parameter)
        {
            sheetBuilder.studentList = parameter;
            return this;
        }
    }
}