using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADUser_TreeList.Models
{
    public class ADUserDummyData
    {
        public IEnumerable<EmployeeDTO> GetDummyADUsers()
        {
            return GetUsers();
        }

        public IList<EmployeeDTO> GetUsers()
        {

            var empDTO = new List<EmployeeDTO>();

            empDTO.Add(new EmployeeDTO { Id = 1, cn = "CEO", ManagerCn = "CEO", ManagerId = null, Email = "xyz@abc.com", Buerau = "sdfsd" });
            empDTO.Add(new EmployeeDTO { Id = 2, cn = "EMP1", ManagerCn = "CEO", ManagerId = 1, Email = "xyz@abc.com", Buerau = "sdfsdsd" });
            empDTO.Add(new EmployeeDTO { Id = 3, cn = "EMP2", ManagerCn = "CEO", ManagerId = 1, Email = "xyz@abc.com", Buerau = "sdxcvfsdsd" });
            empDTO.Add(new EmployeeDTO { Id = 4, cn = "EMP3", ManagerCn = "EMP1", ManagerId = 2, Email = "xyz@abc.com", Buerau = "sdxcvfsd" });
            empDTO.Add(new EmployeeDTO { Id = 5, cn = "EMP4", ManagerCn = "EMP1", ManagerId = 2, Email = "xyz@abc.com", Buerau = "sdsdfsd" });
            empDTO.Add(new EmployeeDTO { Id = 6, cn = "EMP5", ManagerCn = "EMP3", ManagerId = 4, Email = "xyz@abc.com", Buerau = "sdsdfsd" });
            empDTO.Add(new EmployeeDTO { Id = 7, cn = "EMP6", ManagerCn = "EMP5", ManagerId = 6, Email = "xyz@abc.com", Buerau = "sdfsd" });
            empDTO.Add(new EmployeeDTO { Id = 8, cn = "EMP7", ManagerCn = "EMP5", ManagerId = 6, Email = "xyz@abc.com", Buerau = "sdfvxcsd" });
            return empDTO;
        }

        public IList<EmployeeDTO> GetDummyADUsers_Linq()
        {
            var empDTO = new List<EmployeeDTO>();

            empDTO.Add(new EmployeeDTO { Id = 1, cn = "CEO", ManagerCn = "CEO" });
            empDTO.Add(new EmployeeDTO { Id = 2, cn = "EMP1", ManagerCn = "CEO" });
            empDTO.Add(new EmployeeDTO { Id = 3, cn = "EMP2", ManagerCn = "CEO" });
            empDTO.Add(new EmployeeDTO { Id = 4, cn = "EMP3", ManagerCn = "EMP1" });
            empDTO.Add(new EmployeeDTO { Id = 5, cn = "EMP4", ManagerCn = "EMP1" });
            empDTO.Add(new EmployeeDTO { Id = 6, cn = "EMP5", ManagerCn = "EMP3" });
            empDTO.Add(new EmployeeDTO { Id = 7, cn = "EMP6", ManagerCn = "EMP5" });
            empDTO.Add(new EmployeeDTO { Id = 8, cn = "EMP7", ManagerCn = "EMP5" });

            return GetManagerIds(empDTO);

        }

        public IList<EmployeeDTO> GetManagerIds(IEnumerable<EmployeeDTO> empDTO)
        {
            //var empMgrDTO = new List<EmployeeDTO>();
            var MgrData = from r in empDTO
                          join l in empDTO on r.cn equals l.ManagerCn
                          select new EmployeeDTO
                          {
                              Id = l.Id,
                              cn = l.cn,
                              ManagerCn = l.ManagerCn,
                              ManagerId = r.Id
                          };

            var Modeldata =  MgrData.ToList().Select(emp => new EmployeeDTO
            {
                Id = emp.Id,
                ManagerId = emp.ManagerId == emp.Id ? null : emp.ManagerId,
                Buerau = emp.Buerau,
                Email = emp.Email,
                cn = emp.cn,
                Manager = emp.Manager,
                ManagerCn = emp.ManagerCn,
                TeamMember = emp.TeamMember}

            );


            return Modeldata.ToList();
        }


        //public static EmployeeDTOModel ToEmployeeDirectoryModel(this EmployeeDTO employee)
        //{
        //    return new EmployeeDTOModel
        //    {
        //        Id= employee.Id,
        //        ManagerId = employee.ManagerId,
        //        Buerau = employee.Buerau,
        //        cn = employee.cn,
        //        Email = employee.Email,
        //        Manager = employee.Manager,
        //        ManagerCn = employee.ManagerCn,
        //        TeamMember = employee.TeamMember

        //    };
        //}
    }
}