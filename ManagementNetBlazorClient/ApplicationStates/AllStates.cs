using BasicLibrary.Entites;

namespace ManagementNetBlazorClient.ApplicationStates
{
    public class AllStates

    {
        public Action? GeneralDepartmentAction { get; set; }
        public Action? DepartmentAction { get; set; }
        public Action? CityAction { get; set; }
        public Action? CountryAction { get; set; }
        public Action? EmployeeAction { get; set; }
        public Action? TownAction { get; set; }
        public Action? UserAction { get; set; }


        public Action? BranchAction { get; set; }

        public bool ShowGeneralDepartment { get; set; } = true;
        public void GeneralDepartmentClicked()
        {
          //  ResetAllDepartment();
            ShowGeneralDepartment = true;
            GeneralDepartmentAction?.Invoke();
        }
        //Department
        public bool ShowDepartment { get; set; }
        public void DepartmentClicked()
        {
           // ResetAllDepartment();
            ShowDepartment = true;
            DepartmentAction?.Invoke();
        }
        //Branch
        public bool ShowBranch { get; set; }
        public void BranchClicked()
        {
            ResetAllDepartment();
            ShowBranch = true;
            BranchAction?.Invoke();
        }

        //country
        public bool ShowCountry { get; set; }
        public void CountryClicked()
        {
            ResetAllDepartment();
            ShowCountry = true;
            CountryAction?.Invoke();
        }
        //city
        public bool ShowCity { get; set; }
        public void CityClicked()
        {
            ResetAllDepartment();
            ShowCity = true;
            CityAction?.Invoke();
        }
        //Town
        public bool ShowTown { get; set; }
        public void TownClicked()
        {
            ResetAllDepartment();
            ShowTown = true;
            TownAction?.Invoke();
        }
        //User
        public bool ShowUser { get; set; }
        public void UserClicked()
        {
            ResetAllDepartment();
            ShowUser = true;
            UserAction?.Invoke();
        }
        //Employee
        public bool ShowEmployee { get; set; }
        public void EmplyeeClicked()
        {
            ResetAllDepartment();
            ShowEmployee = true;
            EmployeeAction?.Invoke();
        }
        private void ResetAllDepartment()
        {
            ShowGeneralDepartment = false;
            ShowDepartment = false;
            ShowBranch = false;
            ShowCity = false;
            ShowCountry = false;
            ShowTown = false;
            ShowUser = false;
            ShowEmployee = false;
            
        }
    }
}
